using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Afisare : Form
    {
        public Afisare()
        {
            InitializeComponent();
            label_Info.Text = Form1.info; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}
