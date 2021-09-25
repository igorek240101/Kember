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
    public partial class WMC : BaseVisualModule
    {
        Label label;

        public WMC()
        {
            label = new Label();
            label.ForeColor = Color.White;
            label.AutoSize = true;
            Controls.Add(label);
            SizeChanged += new EventHandler(ControlSizeChenged);
        }
        
        public override string NameOfModule { get { return "WMC"; } }

        public override string SystemNameOfModule { get { return "WMC"; } }

        public override object DataCollection()
        {
            return null;
        }

        public override void Reader(string s)
        {
            throw new System.NotImplementedException();
        }

        public override void VisualizeResults(object results)
        {
            label.Text = "";
            object[] result = results as object[];
            (string, int)[] res = result[0] as (string, int)[];
            foreach(var value in res)
            {
                label.Text += value.Item1 + ": " + value.Item2 + "\r\n";
            }
            ControlSizeChenged(null, null);
        }

        public void ControlSizeChenged(object sender, EventArgs e)
        {
            label.Location = new Point(Width/2-label.Width/2, 30);
        }
    }
}
