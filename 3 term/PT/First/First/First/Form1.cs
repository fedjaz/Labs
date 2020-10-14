using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace First
{
    public partial class Form1 : FlatForm
    {
        string OpenedPath = "";
        List<(string, string)> cutList = new List<(string, string)>();
        List<(string, string)> copyList = new List<(string, string)>();
        public Form1()
        {
            InitializeComponent();

            LoadDrives();
        }

        void LoadDrives()
        {
            OpenedPath = "";
            listView1.Items.Clear();
            listView1.LabelEdit = false;
            textBox1.Text = "";
            DriveInfo[] Drives = DriveInfo.GetDrives();

            foreach(DriveInfo drive in Drives)
            {
                ListViewItem item = new ListViewItem(drive.Name);
                item.SubItems.Add("");
                item.SubItems.Add("Drive");
                item.SubItems.Add($"{((float)drive.TotalSize - drive.AvailableFreeSpace) / (1024 * 1024 * 1024)} / {(float)drive.TotalSize / (1024 * 1024 * 1024)} GB");
                item.SubItems.Add(drive.RootDirectory.FullName);
                listView1.Items.Add(item);
            }
        }
        void LoadDirectory(string directory)
        {
            listView1.LabelEdit = true;
            if(directory == "")
            {
                LoadDrives();
                OpenedPath = "";
                return;
            }
            if(!Directory.Exists(directory))
            {
                Form2 f2 = new Form2();
                f2.Start(directory, this);
                LoadDirectory(OpenedPath);
                return;
            }
            
            DirectoryInfo dir = new DirectoryInfo(directory);
            try
            {
                dir.GetDirectories();
            }
            catch
            {
                MessageBox.Show("Access denied!");
                return;
            }
            listView1.Items.Clear();
            textBox1.Text = directory;
            ListViewItem item = new ListViewItem("..");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            string parent = Path.GetFullPath(Path.Combine(directory, @"..\"));
            if(parent == directory)
            {
                parent = "";
            }
            item.SubItems.Add(parent);
            OpenedPath = directory;
            listView1.Items.Add(item);
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);
            imageList.Images.Add(Properties.Resources.folder_icon_big_256);
            
            foreach(DirectoryInfo dir1 in dir.GetDirectories())
            {
                item = new ListViewItem(dir1.Name);
                item.SubItems.Add(dir1.LastWriteTime.ToString("dd.mm.yyyy H:mm"));
                item.SubItems.Add("Folder");
                item.SubItems.Add("");
                item.SubItems.Add(dir1.FullName);
                item.ImageIndex = 0;
                listView1.Items.Add(item);
            }
 
            
            foreach(FileInfo file in dir.GetFiles())
            {
                item = new ListViewItem(file.Name);
                item.SubItems.Add(file.LastWriteTime.ToString("dd.mm.yyyy H:mm"));
                item.SubItems.Add("File");
                item.SubItems.Add($"{(float)file.Length / (1024 * 1024) : 0.00} MB");
                item.SubItems.Add(file.FullName);
                imageList.Images.Add(file.Extension, Icon.ExtractAssociatedIcon(file.FullName));
                item.ImageIndex = imageList.Images.Count - 1;
                listView1.Items.Add(item);
            }
            listView1.SmallImageList = imageList;
            listView1.Invalidate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = (sender as ListView).HitTest(e.Location).Item;
            if(item != null)
            {
                LoadDirectory(item.SubItems[item.SubItems.Count - 1].Text);
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if(e.Label != null)
            {
                if(e.Label == "")
                {
                    MessageBox.Show("Name can't be empty");
                    e.CancelEdit = true;
                    return;
                }
                string path1 = listView1.Items[e.Item].SubItems[listView1.Items[e.Item].SubItems.Count - 1].Text;
                string path2 = Path.GetFullPath(Path.Combine(path1, @"..\")) + e.Label;
                if(listView1.Items[e.Item].SubItems[2].Text == "Folder")
                {
                    if(Directory.Exists(path2))
                    {
                        MessageBox.Show("Directory with this name already exists");
                        e.CancelEdit = true;
                        return;
                    }
                    Directory.Move(path1, path2);
                }
                else if(listView1.Items[e.Item].SubItems[2].Text == "File")
                {
                    if(File.Exists(path2))
                    {
                        MessageBox.Show("File with this name already exists");
                        e.CancelEdit = true;
                        return;
                    }
                    File.Move(path1, path2);
                }
            }
            LoadDirectory(OpenedPath);
        }

        private void newFileButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't create files in a drive viever");
                return;
            }

            string fileName = $"{OpenedPath}/New File.txt";


            if(!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
            else
            {
                int i = 1;
                while(File.Exists($"{OpenedPath}/New File({i}).txt"))
                {
                    i++;
                }
                File.Create($"{OpenedPath}/New File({i}).txt").Close();
            }

            LoadDirectory(OpenedPath);

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't delete something in a drive viever");
                return;
            }
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                if(item.SubItems[2].Text == "Folder")
                {
                    Directory.Delete(item.SubItems[item.SubItems.Count - 1].Text, true);
                }
                else if(item.SubItems[2].Text == "File")
                {
                    File.Delete(item.SubItems[item.SubItems.Count - 1].Text);
                }
            }
            LoadDirectory(OpenedPath);
        }

        private void newFolderButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't create folders in a drive viever");
                return;
            }

            string folderName = $"{OpenedPath}/New Folder/";


            if(!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            else
            {
                int i = 1;
                while(Directory.Exists($"{OpenedPath}/New Folder({i})/"))
                {
                    i++;
                }
                Directory.CreateDirectory($"{OpenedPath}/New Folder({i})/");
            }
            LoadDirectory(OpenedPath);
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't cut from a drive viever");
                return;
            }
            cutList.Clear();
            copyList.Clear();
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                if(item.SubItems[2].Text == "Folder" || item.SubItems[2].Text == "File")
                {
                    cutList.Add((item.SubItems[item.SubItems.Count - 1].Text, item.SubItems[2].Text));
                }
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't copy from a drive viever");
                return;
            }
            cutList.Clear();
            copyList.Clear();
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                if(item.SubItems[2].Text == "Folder" || item.SubItems[2].Text == "File")
                {
                    copyList.Add((item.SubItems[item.SubItems.Count - 1].Text, item.SubItems[2].Text));
                }
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            if(OpenedPath == "")
            {
                MessageBox.Show("You can't paste in a drive viever");
                return;
            }
            bool toCut = cutList.Count > 0;
            List<(string, string)> procList = new List<(string, string)>();
            procList.AddRange(copyList);
            procList.AddRange(cutList);
            foreach((string, string) path in procList)
            {
                if(path.Item2 == "Folder")
                {
                    string folderName = OpenedPath + "\\" + path.Item1.Trim('\\').Split('\\').Last();
                    while(Directory.Exists(folderName))
                    {
                        folderName += " - copy";
                    }
                    if(toCut)
                    {
                        Directory.Move(path.Item1, folderName);
                    }
                    else
                    {
                        DirectoryInfo source = new DirectoryInfo(path.Item1);
                        DirectoryInfo target = new DirectoryInfo(folderName);
                        Directory.CreateDirectory(folderName);
                        CopyFilesRecursively(source, target);
                    }
                }
                else if(path.Item2 == "File")
                {
                    string fileName = OpenedPath + "\\" + path.Item1.Split('\\').Last();
                    string extension = "." + fileName.Split('.').Last();
                    fileName = fileName.Substring(0, fileName.Length - extension.Length);
                    while(File.Exists(fileName + extension))
                    {
                        fileName += " - copy";
                    }
                    if(toCut)
                    {
                        File.Move(path.Item1, fileName + extension);
                    }
                    else
                    {
                        File.Copy(path.Item1, fileName + extension);
                    }
                }
            }
            cutList.Clear();
            LoadDirectory(OpenedPath);
        }

        void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach(DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach(FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        private void archiveButton_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                if(item.SubItems[2].Text == "Folder")
                {
                    string archivePath = OpenedPath + "\\" + item.Text;
                    ZipFile.CreateFromDirectory(archivePath, archivePath + ".zip");
                }
            }
            LoadDirectory(OpenedPath);
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                if(item.SubItems[2].Text == "File" && item.Text.Split('.').Last() == "zip")
                {
                    string archivePath = OpenedPath + "\\" + item.Text;
                    string folderPath = archivePath.Substring(0, archivePath.Length - 4);
                    try
                    {
                        ZipFile.ExtractToDirectory(archivePath, folderPath + " - extracted\\");
                    }
                    catch
                    {
                        MessageBox.Show("This archive is damaged");
                    }
                }
            }
            LoadDirectory(OpenedPath);
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(Directory.Exists(textBox1.Text))
                {
                    OpenedPath = textBox1.Text;
                    LoadDirectory(OpenedPath);
                }
                else
                {
                    textBox1.Text = OpenedPath;
                }
            }
        }
    }
}
