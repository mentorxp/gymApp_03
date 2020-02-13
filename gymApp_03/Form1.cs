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
    public partial class Form1 : Form
    {
        string [] abonati = { "Moldovan Nicoleta", "Marc Alexandru", "Popovici Selina", "Iurcut Anca" };
        string [] codCartele ={ "SassyFit0001", "SassyFit0002", "SassyFit0003", "SassyFit0004" };
        string [] imagini = { "mnicoleta.jpg", "malexandru.jpg", "pselina.jpg", "ianca.jpg" };
        string[] inregAbonati = { "", "", "", "", "", "" };

        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Now.AddDays(1);
            comboBox_ListaNume.SelectedIndex = 0;
        }
    }
}
