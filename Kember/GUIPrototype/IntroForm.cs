using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kember;
using System.Security.Cryptography;

namespace GUIPrototype
{
    public partial class IntroForm : Form
    {
        int count = 0;

        public static bool goodLoad = false;

        public IntroForm()
        {
            User user = AppDbContext.db.Users.FirstOrDefault(t => t.Name == "Igor");
            InitializeComponent();
            label2.Text = Environment.UserName;
            if (user == null)
            {
                label1.Text = "Добро пожаловать!";
                label3.Text = "Придумайте ключ безопастности (число)";
                progressBar1.Visible = false;
            }
            else
            {
                label1.Text = "C возвращением!";
                label3.Text = "Подождите";
                button1.Visible = false;
                textBox1.Visible = false;
                timer1.Start();
            }

            label1.Location = new Point(Width / 2 - label1.Width / 2, 20);
            label2.Location = new Point(Width / 2 - label2.Width / 2, 70);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int key;
            if (int.TryParse(textBox1.Text, out key))
            {
                KemberBackModule.Registration(Environment.UserName, Convert.ToString(key));
                goodLoad = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ввдеите число", "Ошибка!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            progressBar1.Value = count * 10;
            if (count == 10)
            { 
                goodLoad = true; 
                Close(); 
            }
        }
    }
}
