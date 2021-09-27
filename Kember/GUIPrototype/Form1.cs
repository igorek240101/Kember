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
using System.IO;
using Kember;

namespace GUIPrototype
{
    public partial class Form1 : Form
    {

        Dictionary<string, Assembly> assemlies = new Dictionary<string, Assembly>();

        Dictionary<string, BaseVisualModule> visualModules = new Dictionary<string, BaseVisualModule>();

        BaseVisualModule currentModule = null;

        public Form1()
        {
            InitializeComponent();
            ModuleLoad();
            Text = Environment.UserName;
            splitContainer1_Panel2_SizeChanged(null, null);
            splitContainer1_Panel1_SizeChanged(null, null);
        }

        private void ModuleLoad()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(Form1)).CodeBase).LocalPath;
            path = path.Substring(0, path.LastIndexOf("\\")) + @"\Modules";
            string[] dlls = Directory.GetFiles(path, "*.dll");
            for (int i = 0; i < dlls.Length; i++)
            {
                Type[] types = null;
                Assembly assembly = Assembly.LoadFile(dlls[i]);
                try
                {
                    types = assembly.GetTypes();
                }
                catch { types = new Type[0]; }
                for (int j = 0; j < types.Length; j++)
                {
                    if (!types[j].IsAbstract && !types[j].IsEnum && types[j].IsClass && types[j].IsSubclassOf(typeof(BaseVisualModule)))
                    {
                        BaseVisualModule loaded = types[j].GetConstructor(new Type[0]).Invoke(new object[0]) as BaseVisualModule;
                        visualModules.Add(loaded.NameOfModule, loaded);
                        menuStrip1.Items.Insert(0, new ToolStripMenuItem());
                        menuStrip1.Items[0].Text = loaded.NameOfModule;
                        menuStrip1.Items[0].ForeColor = SystemColors.Control;
                        splitContainer1.Panel1.Controls.Add(loaded);
                        loaded.Visible = false;
                    }
                }
            }
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            listBox1.Size = new Size(splitContainer1.Panel2.Width - 100,(int)(splitContainer1.Panel2.Height * 0.9));
            listBox1.Location = new Point(15, 10);
            button1.Location = new Point(splitContainer1.Panel2.Width - 75, 25);
            button2.Location = new Point(splitContainer1.Panel2.Width - 75, 100);
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            button3.Location = new Point(splitContainer1.Panel1.Width - 65, splitContainer1.Panel1.Height - 65);
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(currentModule != null)
            {
                currentModule.Visible = false;
                currentModule = null;
            }
            if (e.ClickedItem == menuStrip1.Items[^1])
            {
                currentModule = null;
                button3.Visible = false;
            }
            else
            {
                
                ToolStripItem toolStrip = e.ClickedItem;
                toolStrip.Enabled = false;
                currentModule = visualModules[toolStrip.Text];
                currentModule.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentModule != null)
            {
                if(assemlies.Count == 0)
                {
                    MessageBox.Show("Не выбраны сборки для анализа", "Нет сборок для анализа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Assembly[] objectsOfAnalization = assemlies.Values.ToArray();
                object[] results = KemberBackModule.Invoke(objectsOfAnalization, currentModule.DataCollection(), currentModule.SystemNameOfModule);
                currentModule.VisualizeResults(results);
            }
        }
    }
}
