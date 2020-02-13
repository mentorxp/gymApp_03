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
        string[] abonati = { "Moldovan Nicoleta", "Marc Alexandru", "Popovici Selina", "Iurcut Anca" };
        string[] codCartele = { "SassyFit0001", "SassyFit0002", "SassyFit0003", "SassyFit0004" };
        string[] imagini = { "mnicoleta.jpg", "malexandru.jpg", "pselina.jpg", "ianca.jpg" };
        string[] inregAbonati = { "", "", "", "", "", "" };

        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Now.AddDays(1);
            comboBox1_ListaNume.SelectedIndex = 0;
        }
        private void incarca_Combo_Cartele()
        {
            int i; try
            {
                string dom;
                // Verificam daca am selectat un “numele abonatului”             
                if (comboBox1_ListaNume.SelectedIndex == 0)
                    dom = "";  // SelectedIndex == 0  este pentru -Toate numele-          
                else
                    dom = comboBox1_ListaNume.SelectedItem.ToString();

                if (dom == "")  // Nu am selectat nici un nume                
                    for (i = 0; i < abonati.Length; i++)
                    {
                        if (inregAbonati[i] == "")
                            comboBox2_Cartele.Items.Add(codCartele[i]);
                    }
                else  //  Am selectat numele                       
                    for (i = 0; i < codCartele.Length; i++)
                    {
                        if ((dom == abonati[i]) && (inregAbonati[i] == ""))
                            comboBox2_Cartele.Items.Add(codCartele[i]);
                    }
                if (comboBox2_Cartele.Items.Count == 0)  // comboBox2_Cartele nu contine nici un item                
                    MessageBox.Show("Cartele Epuizate", "Atentie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reseteaza_Cartele()
        {
            // Resetam valoarea controalelor care tin de un codCartela selectat          
            comboBox2_Cartele.Items.Clear();  //  Golim lista cartelelor incarcate in comboBox_Filmul       
            comboBox2_Cartele.SelectedIndex = -1;
            pictureBox1.Image = null;  // Stergem imaginea din pictureBox1         } 

        }

        private void comboBox1_ListaNume_SelectedIndexChanged(object sender, EventArgs e)
        {
            reseteaza_Cartele();
            incarca_Combo_Cartele();
        }

        private void comboBox2_Cartele_SelectedIndexChanged(object sender, EventArgs e)
        {
         try             { 
                // Cautam numele fisierului in care avem imaginea corespunzatoare Abonatului
             int i;            
             for (i = 0; i < codCartele.Length && comboBox2_Cartele.SelectedItem.ToString() != codCartele[i]; i++);
             pictureBox1.Image = Image.FromFile("..\\..\\..\\poze\\" + imagini[i]);
             // aceasta "..\\..\\..\\poze\\" este calea relativa spre folderul in care sunt pastrate imaginile. Reperul este fisierul .exe al aplicatiei care se afla in \bin\Debug
             pictureBox1.Visible = true;
         }
         catch (Exception ex)             {
             MessageBox.Show(ex.Message);
         } 
        }
    }
}
