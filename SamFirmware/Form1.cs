using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamFirmware
{
    public partial class Form1 : Form
    {
        /*private string adb = Path.Combine(Path.GetTempPath(), "adb.exe");
        private string AdbWinApi = Path.Combine(Path.GetTempPath(), "AdbWinApi.dll");
        private string AdbWinUsbApi = Path.Combine(Path.GetTempPath(), "AdbWinUsbApi.dll");*/
        public Form1()
        {


            InitializeComponent();
            if (Directory.Exists("tmp"))
            {
                foreach (var file in Directory.GetFiles("tmp"))
                {
                    File.Delete(file);
                }

                foreach (var directory in Directory.GetDirectories("tmp"))
                {
                    Directory.Delete(directory, true);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                listBox1.Items.Clear();

                if (openFileDialog1.FileName.Contains("img"))
                    listBox1.Items.Add(openFileDialog1.FileName);


                if (Directory.Exists("tmp"))
                {
                    foreach (var file in Directory.GetFiles("tmp"))
                    {
                        File.Delete(file);
                    }

                    foreach (var directory in Directory.GetDirectories("tmp"))
                    {
                        Directory.Delete(directory, true);
                    }

                }
                else
                {
                    Directory.CreateDirectory("tmp");
                }

                Process process = new Process();
                process.StartInfo.FileName = "tar.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                process.EnableRaisingEvents = false;

                process.StartInfo.Arguments = @" --force-local -C tmp -xvf " + "\"" + openFileDialog1.FileName + "\"";
                process.Start();
                process.WaitForExit();

                string folder = "tmp";
                string[] files = Directory.GetFiles(folder);


                foreach (string i in files)
                    listBox1.Items.Add(i.Remove(0, 4));



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int intCount = listBox1.SelectedItems.Count - 1; intCount >= 0; intCount--)
            {
                if (!listBox2.Items.Contains(listBox1.SelectedItems[intCount]))
                    listBox2.Items.Add(listBox1.SelectedItems[intCount]);
                listBox1.Items.Remove(listBox1.SelectedItems[intCount]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int intCount = listBox2.SelectedItems.Count - 1; intCount >= 0; intCount--)
            {
                listBox1.Items.Add(listBox2.SelectedItems[intCount]);
                listBox2.Items.Remove(listBox2.SelectedItems[intCount]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Directory.Exists("tmp"))
                {

                    Process process = new Process();
                    process.StartInfo.FileName = "tar.exe";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    process.EnableRaisingEvents = false;

                    process.StartInfo.Arguments = @" --force-local -C tmp -xvf " + "\"" + openFileDialog1.FileName + "\"";
                    process.Start();
                    process.WaitForExit();

                    string folder = "tmp";
                    string[] files = Directory.GetFiles(folder);



                    foreach (string i in files)
                    {
                        if (!listBox1.Items.Contains(i.Remove(0, 4)) && !listBox2.Items.Contains(i.Remove(0, 4)))
                            listBox1.Items.Add(i.Remove(0, 4));
                    }
                }
                else
                {
                    MessageBox.Show("Open Firmware First");
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name.Length == 0)
                name = "firmware";
            name = name + ".tar";
            string folder = "tmp";
            string[] files = Directory.GetFiles(folder);
            Process process = new Process();
            process.StartInfo.FileName = "tar.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            process.EnableRaisingEvents = false;

            if (File.Exists(name))
            {
                File.Delete(name);
            }

            foreach (var item in listBox2.Items)
            {
                process.StartInfo.Arguments = @" --transform 's/.*\///g' -rf " + name + " tmp/" + item.ToString();
                process.Start();
                process.WaitForExit();
            }

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.Move(name, folderBrowserDialog1.SelectedPath + "//" + name);
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
