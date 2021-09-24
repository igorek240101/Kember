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
using Microsoft.EntityFrameworkCore;

namespace GUIPrototype
{
    public partial class IntroForm : Form
    {
        public IntroForm()
        {
            User user = AppDbContext.db.Users.FirstOrDefault(t => t.Name == Environment.UserName);
            InitializeComponent();
            if (true)
            {
                label1.Text = "Добро пожаловть!";
                label2.Text = Environment.UserName;
                progressBar1.Visible = false;
            }
            else
            {

            }
        }
    }
}
