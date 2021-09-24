using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace GUIPrototype
{
    public partial class Form1 : Form
    {

        Dictionary<string, Assembly> assemlies = new Dictionary<string, Assembly>();

        public Form1()
        {
            InitializeComponent();
            Text = Environment.UserName;
            splitContainer1_Panel2_SizeChanged(null, null);
            splitContainer1_Panel1_SizeChanged(null, null);
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            listBox1.Size = new Size((int)(splitContainer1.Panel2.Width - 100),(int)(splitContainer1.Panel2.Height * 0.9));
            listBox1.Location = new Point(15, 10);
            button1.Location = new Point(splitContainer1.Panel2.Width - 75, 25);
            button2.Location = new Point(splitContainer1.Panel2.Width - 75, 100);
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            button3.Location = new Point(splitContainer1.Panel1.Width - 135, splitContainer1.Panel1.Height - 65);
            button4.Location = new Point(splitContainer1.Panel1.Width - 75, splitContainer1.Panel1.Height - 65);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "DLL|*.dll";
            fileDialog.Multiselect = true;
            fileDialog.FileOk += new CancelEventHandler(DLLLoader);
            fileDialog.ShowDialog();
        }

        private void DLLLoader(object sender, CancelEventArgs e)
        {
            FileDialog fileDialog = sender as FileDialog;
            for(int i = 0; i < fileDialog.FileNames.Length; i++)
            {
                if(fileDialog.FileNames[i].Split('.')[^1] != "dll")
                {
                    MessageBox.Show("Выбранный файл не является dll", "Расширение не dll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        assemlies.Add(fileDialog.FileNames[i], Assembly.LoadFile(fileDialog.FileNames[i]));
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Сборка по этому пути уже загружена", "Сборка-дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    listBox1.Items.Add(fileDialog.FileNames[i]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while(listBox1.SelectedItems.Count > 0)
            {
                assemlies.Remove(listBox1.SelectedItems[0] as string);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            listBox1.SelectedIndex = -1;
        }
    }
}
