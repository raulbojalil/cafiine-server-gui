using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafiineServerUI
{
    public partial class MainForm : Form
    {
        public const byte BYTE_NORMAL = 0xff;
        public const byte BYTE_SPECIAL = 0xfe;
        public const byte BYTE_OPEN = 0x00;
        public const byte BYTE_READ = 0x01;
        public const byte BYTE_CLOSE = 0x02;
        public const byte BYTE_OK = 0x03;
        public const byte BYTE_SETPOS = 0x04;
        public const byte BYTE_STATFILE = 0x05;
        public const byte BYTE_EOF = 0x06;
        public const byte BYTE_GETPOS = 0x07;
        public const byte BYTE_REQUEST = 0x08;
        public const byte BYTE_REQUEST_SLOW = 0x09;
        public const byte BYTE_HANDLE = 0x0A;
        public const byte BYTE_DUMP = 0x0B;
        public const byte BYTE_PING = 0x0C;

        [Flags]
        public enum FSStatFlag : uint
        {
            None = 0,
            unk_14_present = 0x01000000,
            mtime_present = 0x04000000,
            ctime_present = 0x08000000,
            entid_present = 0x10000000,
            directory = 0x80000000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FSStat
        {
            public FSStatFlag flags;
            public uint permission;
            public uint owner;
            public uint group;
            public uint file_size;
            public uint unk_14_nonzero;
            public uint unk_18_zero;
            public uint unk_1c_zero;
            public uint ent_id;
            public uint ctime_u;
            public uint ctime_l;
            public uint mtime_u;
            public uint mtime_l;
            public uint unk_34_zero;
            public uint unk_38_zero;
            public uint unk_3c_zero;
            public uint unk_40_zero;
            public uint unk_44_zero;
            public uint unk_48_zero;
            public uint unk_4c_zero;
            public uint unk_50_zero;
            public uint unk_54_zero;
            public uint unk_58_zero;
            public uint unk_5c_zero;
            public uint unk_60_zero;
        }


        public enum LogType
        {
            Info = 0,
            Error = 1,
            File = 2,
            ReplacedFile = 3,
            RequestedFile = 4,
            TitleID = 5
        }

        public static string root = "cafiine_root";
        public static Dictionary<string, string> requestedFiles = new Dictionary<string, string>();
        public static string lastTitleID = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ReplaceFile(string path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the file to replace " + path;
            ofd.Multiselect = false;
            ofd.Filter = "|*" + Path.GetExtension(path);

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if(!path.StartsWith("\\"))
                    path = "\\" + path;
                path = root + path;
                var dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.Copy(ofd.FileName, path);
            }
        }

        private void RequestFile(string remoteFile)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select the local file to save " + remoteFile + " to";
            sfd.FileName = Path.GetFileName(remoteFile);
            sfd.Filter = "|*" + Path.GetExtension(remoteFile);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (!requestedFiles.ContainsKey(remoteFile))
                    requestedFiles.Add(remoteFile, sfd.FileName);
                requestedFiles[remoteFile] = sfd.FileName;
            }
        }

        private void Log(string message)
        {
            bwkMain.ReportProgress((int)LogType.Info, message);
        }

        private void LogError(string message)
        {
            bwkMain.ReportProgress((int)LogType.Error, message);
        }

        private void LogFile(string file)
        {
            bwkMain.ReportProgress((int)LogType.File, file);
        }

        private void LogReplacedFile(string file)
        {
            bwkMain.ReportProgress((int)LogType.ReplacedFile, file);
        }

        private void LogRequestedFile(string file)
        {
            bwkMain.ReportProgress((int)LogType.RequestedFile, file);
        }

        private void SetTitleID(string id)
        {
            bwkMain.ReportProgress((int)LogType.TitleID, id);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            bwkMain.RunWorkerAsync();
        }

        private void bwkMain_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 7332);
                listener.Start();
                Log("Listening on 7332");

                int index = 0;
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Log("Connected");
                    Thread thread = new Thread(HandleClient);
                    thread.Name = "[" + index.ToString() + "]";
                    thread.Start(client);
                    index++;
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void HandleClient(object client_obj)
        {
            string name = Thread.CurrentThread.Name;
            FileStream[] files = new FileStream[256];
            Dictionary<int, FileStream> files_request = new Dictionary<int, FileStream>();

            try
            {
                TcpClient client = (TcpClient)client_obj;
                using (NetworkStream stream = client.GetStream())
                {
                    EndianBinaryReader reader = new EndianBinaryReader(stream);
                    EndianBinaryWriter writer = new EndianBinaryWriter(stream);

                    uint[] ids = reader.ReadUInt32s(4);

                    var titleId = ids[0].ToString("X8") + "-" + ids[1].ToString("X8");

                    SetTitleID(titleId);

                    if (!Directory.Exists(root + "\\" + titleId))
                        Directory.CreateDirectory(root + "\\" + titleId);

                    Log(name + " Accepted connection from client " + client.Client.RemoteEndPoint.ToString());
                    Log(name + " TitleID: " + titleId);
                    
                    string LocalRoot = root + "\\" + titleId;
                    writer.Write(BYTE_SPECIAL);

                    while (true)
                    {
                        byte cmd_byte = reader.ReadByte();
                        switch (cmd_byte)
                        {
                            case BYTE_OPEN:
                                {
                                    bool request_slow = false;

                                    int len_path = reader.ReadInt32();
                                    int len_mode = reader.ReadInt32();
                                    string path = reader.ReadString(Encoding.ASCII, len_path - 1);
                                    if (reader.ReadByte() != 0) throw new InvalidDataException();
                                    string mode = reader.ReadString(Encoding.ASCII, len_mode - 1);
                                    if (reader.ReadByte() != 0) throw new InvalidDataException();
                                    LogFile(path);
                                    if (File.Exists(LocalRoot + path))
                                    {
                                        int handle = -1;
                                        for (int i = 0; i < files.Length; i++)
                                        {
                                            if (files[i] == null)
                                            {
                                                handle = i;
                                                break;
                                            }
                                        }
                                        if (handle == -1)
                                        {
                                            Log(name + " Out of file handles!");
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-19);
                                            writer.Write(0);
                                            break;
                                        }

                                        
                                        Log(name + " -> fopen(\"" + path + "\", \"" + mode + "\") = " + handle.ToString());
                                        LogReplacedFile(path);

                                        files[handle] = new FileStream(LocalRoot + path, FileMode.Open, FileAccess.Read, FileShare.Read);

                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(0);
                                        writer.Write(0x0fff00ff | (handle << 8));

                                    }
                                    // Check for file request : filename + "-request" or + "-request_slow"
                                    //else if (File.Exists(LocalRoot + path + "-request") || (request_slow = File.Exists(LocalRoot + path + "-request_slow")))
                                    else if(requestedFiles.ContainsKey(path))
                                    {
                                        // Check if dump has already been done
                                        //if (!File.Exists(LocalRoot + path + "-dump"))
                                        //{
                                            // Inform cafiine that we request file to be sent
                                            writer.Write(!request_slow ? BYTE_REQUEST : BYTE_REQUEST_SLOW);
                                        //}
                                        //else
                                        //{
                                            // Nothing to do
                                            //writer.Write(BYTE_NORMAL);
                                        //}
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_HANDLE:
                                {
                                    // Read buffer params : fd, path length, path string
                                    int fd = reader.ReadInt32();
                                    int len_path = reader.ReadInt32();
                                    string path = reader.ReadString(Encoding.ASCII, len_path - 1);
                                    if (reader.ReadByte() != 0) throw new InvalidDataException();

                                    // Add new file for incoming data
                                    //files_request.Add(fd, new FileStream(LocalRoot + path + "-dump", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write));
                                    files_request.Add(fd, new FileStream(requestedFiles[path], FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write));
                                    

                                    // Send response
                                    writer.Write(BYTE_SPECIAL);
                                    break;
                                }
                            case BYTE_DUMP:
                                {
                                    // Read buffer params : fd, size, file data
                                    int fd = reader.ReadInt32();
                                    int sz = reader.ReadInt32();
                                    byte[] buffer = new byte[sz];
                                    buffer = reader.ReadBytes(sz);

                                    // Look for file descriptor
                                    foreach (var item in files_request)
                                    {
                                        if (item.Key == fd)
                                        {
                                            FileStream dump_file = item.Value;
                                            if (dump_file == null)
                                                break;

                                            Log(name + " -> dump(\"" + Path.GetFileName(dump_file.Name) + "\") " + (sz / 1024).ToString() + "kB");

                                            // Write to file
                                            dump_file.Write(buffer, 0, sz);

                                            break;
                                        }
                                    }

                                    // Send response
                                    writer.Write(BYTE_SPECIAL);
                                    break;
                                }
                            case BYTE_READ:
                                {
                                    int size = reader.ReadInt32();
                                    int count = reader.ReadInt32();
                                    int fd = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-19);
                                            writer.Write(0);
                                            break;
                                        }
                                        FileStream f = files[handle];

                                        byte[] buffer = new byte[size * count];
                                        int sz = f.Read(buffer, 0, buffer.Length);
                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(sz / size);
                                        writer.Write(sz);
                                        writer.Write(buffer, 0, sz);
                                        if (reader.ReadByte() != BYTE_OK)
                                            throw new InvalidDataException();
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_CLOSE:
                                {
                                    int fd = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-38);
                                            break;
                                        }

                                        
                                        Log(name + " close(" + handle.ToString() + ")");
                                        FileStream f = files[handle];

                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(0);
                                        f.Close();
                                        files[handle] = null;
                                    }
                                    else
                                    {
                                        // Check if it is a file to dump
                                        foreach (var item in files_request)
                                        {
                                            if (item.Key == fd)
                                            {
                                                FileStream dump_file = item.Value;
                                                if (dump_file == null)
                                                    break;

                                                Log(name + " -> dump complete(\"" + Path.GetFileName(dump_file.Name) + "\")");
                                                LogRequestedFile(dump_file.Name);

                                                // Close file and remove from request list
                                                dump_file.Close();
                                                files_request.Remove(fd);
                                                

                                                break;
                                            }
                                        }

                                        // Send response
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_SETPOS:
                                {
                                    int fd = reader.ReadInt32();
                                    int pos = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-38);
                                            break;
                                        }
                                        FileStream f = files[handle];

                                        f.Position = pos;
                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(0);
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_STATFILE:
                                {
                                    int fd = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-38);
                                            writer.Write(0);
                                            break;
                                        }
                                        FileStream f = files[handle];

                                        FSStat stat = new FSStat();

                                        stat.flags = FSStatFlag.None;
                                        stat.permission = 0x400;
                                        stat.owner = ids[1];
                                        stat.group = 0x101e;
                                        stat.file_size = (uint)f.Length;

                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(0);
                                        writer.Write(Marshal.SizeOf(stat));
                                        writer.Write(stat);
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_EOF:
                                {
                                    int fd = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-38);
                                            break;
                                        }
                                        FileStream f = files[handle];

                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(f.Position == f.Length ? -5 : 0);
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_GETPOS:
                                {
                                    int fd = reader.ReadInt32();
                                    if ((fd & 0x0fff00ff) == 0x0fff00ff)
                                    {
                                        int handle = (fd >> 8) & 0xff;
                                        if (files[handle] == null)
                                        {
                                            writer.Write(BYTE_SPECIAL);
                                            writer.Write(-38);
                                            writer.Write(0);
                                            break;
                                        }
                                        FileStream f = files[handle];

                                        writer.Write(BYTE_SPECIAL);
                                        writer.Write(0);
                                        writer.Write((int)f.Position);
                                    }
                                    else
                                    {
                                        writer.Write(BYTE_NORMAL);
                                    }
                                    break;
                                }
                            case BYTE_PING:
                                {
                                    int val1 = reader.ReadInt32();
                                    int val2 = reader.ReadInt32();

                                    Log(name + " PING RECEIVED with values : " + val1.ToString() + " - " + val2.ToString());
                                    break;
                                }
                            default:
                                throw new InvalidDataException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(name + " " + ex.Message);
            }
            finally
            {
                foreach (var item in files)
                {
                    if (item != null)
                        item.Close();
                }
                foreach (var item in files_request)
                {
                    if (item.Value != null)
                        item.Value.Close();
                }
            }
            Log(name + " Exit");
        }

        private void bwkMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var message = e.UserState.ToString();
            var messageType = (LogType)e.ProgressPercentage;

            switch (messageType)
            {
                case LogType.Info:
                    lsvLog.Items.Add(message, 0);
                    lsvLog.Items[lsvLog.Items.Count - 1].EnsureVisible();
                    break;
                case LogType.Error:
                    lsvLog.Items.Add(message, 1);
                    lsvLog.Items[lsvLog.Items.Count - 1].EnsureVisible();
                    break;
                case LogType.File:
                    lsvGameFiles.Items.Add(message);
                    lsvGameFiles.Items[lsvGameFiles.Items.Count - 1].EnsureVisible();
                    break;
                case LogType.ReplacedFile:
                    var replacedFileItem = new ListViewItem();
                    replacedFileItem.Tag = Application.StartupPath + "\\" + root + "\\" + lastTitleID + message;
                    replacedFileItem.ImageIndex = 2;
                    replacedFileItem.Text = message;
                    lsvGameFiles.Items.Add(replacedFileItem);
                    lsvGameFiles.Items[lsvGameFiles.Items.Count - 1].EnsureVisible();
                    break;
                case LogType.RequestedFile:
                    var origPath = requestedFiles.FirstOrDefault(kvp => kvp.Value == message);

                    if (origPath.Key != null)
                    {
                        var requestedFileItem = new ListViewItem();
                        requestedFileItem.Tag = message;
                        requestedFileItem.ImageIndex = 3;
                        requestedFileItem.Text = origPath.Key;
                        requestedFiles.Remove(origPath.Key);

                        lsvGameFiles.Items.Add(requestedFileItem);
                    }
                    else
                        lsvGameFiles.Items.Add(message, 3);

                    lsvGameFiles.Items[lsvGameFiles.Items.Count - 1].EnsureVisible();
                    break;
                case LogType.TitleID:

                    if (lastTitleID != message)
                        lsvGameFiles.Items.Clear();

                    lastTitleID = message;

                    grbGameFiles.Text = string.Format("Game files ({0})", message);
                    break;
            }
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lsvGameFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var replaceAndRequestEnabled = lsvGameFiles.SelectedItems.Count > 0 && lsvGameFiles.SelectedItems[0].Tag == null;
            var openEnabled = lsvGameFiles.SelectedItems.Count > 0 && lsvGameFiles.SelectedItems[0].Tag != null;

            replaceSelectedFileToolStripMenuItem.Enabled = replaceAndRequestEnabled;
            replaceSelectedFileToolStripButton.Enabled = replaceAndRequestEnabled;
            replaceFileToolStripMenuItem.Enabled = replaceAndRequestEnabled;
            
            requestFileToolStripMenuItem.Enabled = replaceAndRequestEnabled;
            requestSelectedFileToolStripMenuItem.Enabled = replaceAndRequestEnabled;
            requestSelectedFiletoolStripButton.Enabled = replaceAndRequestEnabled;

            openSelectedFileToolStripMenuItem.Enabled = openEnabled;
            openToolStripMenuItem.Enabled = openEnabled;
            openToolStripButton.Enabled = openEnabled;
        }

        private void replaceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lsvGameFiles.SelectedItems.Count > 0)
                ReplaceFile(lastTitleID + lsvGameFiles.SelectedItems[0].Text);
        }

        private void requestFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsvGameFiles.SelectedItems.Count > 0)
                RequestFile(lsvGameFiles.SelectedItems[0].Text);
        }


        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (lsvGameFiles.SelectedItems.Count > 0 && lsvGameFiles.SelectedItems[0].Tag != null)
            {
                try { Process.Start(lsvGameFiles.SelectedItems[0].Tag.ToString()); }
                catch { }
            }
        }
        
    }
}
