using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIPrototype
{
    public abstract partial class BaseVisualModule : UserControl
    {
        public BaseVisualModule()
        {
            InitializeComponent();
            AutoSize = true;
        }

        public abstract string NameOfModule { get; }

        public abstract string SystemNameOfModule { get; }

        public abstract void VisualizeResults(object[] results);

        public abstract void Reader(string s);

        public abstract object DataCollection();

    }
}
