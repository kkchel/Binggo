using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1to50Game_Client
{
    public partial class Setting_Form : Form
    {
        Waiting_Form wf;
        public Boolean OK = false;
        public int x;
        public int y;
        public int num;


        public Setting_Form()
        {
            InitializeComponent();
            wf = (Waiting_Form)this.Owner;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x = int.Parse(row.Text);
            y = int.Parse(col.Text);
            num = int.Parse(size.Text);
            if (x > 0 && y > 0 && num >= x * y)
            {
                OK = true;
                this.Close();
            }
        }
    }
}
