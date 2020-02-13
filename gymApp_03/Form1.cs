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
        public static string info; 
        int nr_cartele_vandute;   
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
                    MessageBox.Show("Cartela a fost activata.");
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

        private void button1_Click(object sender, EventArgs e)
        {
            int i, gasit; string nume_cli = textBox1.Text.Trim();
            // Nume client  
            string cartele_sel;
            try             {
                if (comboBox2_Cartele.SelectedIndex == -1)
                    // Nu a fost selectat nimic in comboBox2_Cartele
                    throw new Exception("Va rugam selectati un Codul Cartelei");
                if (nume_cli == "")
                    // Nu a fost tastat nimic in TextBox Nume  
                    throw new Exception("Va rugam mentionati numele Instructorului sau o mentiune");
                // Daca ajunge executia in acest punct inseamna ca   
                
                cartele_sel = comboBox2_Cartele.SelectedItem.ToString();
                    
                gasit = 0; 
                for (i = 0; i < codCartele.Length && gasit == 0; i++ )                 {     
                    if (codCartele[i] == cartele_sel)                     { 
                        inregAbonati[i] = nume_cli; 
                        gasit = 1;
                    } 
                } 
                // Calculam numarul de zile pana la data inchirierii si il afisam in label_mesaj
                int nrzile = dateTimePicker1.Value.Subtract(DateTime.Now).Days + 1;      
                label_Mesaj.Text =  " Abonamentul cu seria : " + cartele_sel + " -- " + " este reinoit pentru" + nrzile + " zile";
                  
                reseteaza_Cartele(); 
                //     
                incarca_Combo_Cartele();    
            }    
            catch (Exception ex)             {
                MessageBox.Show(ex.Message, "Eroare!"); 
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1_ListaNume.SelectedIndex = 0;  
            // Deselectam domeniul selectat anterior, revenim la toate domeniile         
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            // Setam Data corespunzatoare zilei de "maine"        
            textBox1.Text = "";  // Stergem numele persoanei       
            label_Mesaj.Text = "";  // Stergem mesajul prin care am anuntat inchirierea unui film       
            reseteaza_Cartele();  // Stergem filmul selectat si pe cele care erau in lista      
            incarca_Combo_Cartele();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void disponibileToolStripMenuItem_Click(object sender, EventArgs e)
        
        {
            info = "Cartele disponibile:\n\n";
            int gasit = 0;
            for (int i = 0; i < codCartele.Length; i++) 
                if (inregAbonati[i] == "") { 
                    info += codCartele[i] + " - " + abonati[i] + " \n"; gasit++; }
            if (gasit == 0) info = "Nu sunt Cartele disponibile\n   va rugam generati noi coduri";
            Afisare codCarteleDisp = new Afisare(); 
            codCarteleDisp.Text = "Disponibile";
            codCarteleDisp.ShowDialog();   
        }

        private void totalVanduteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info = ""; nr_cartele_vandute = 0; 
            for (int i = 0; i < codCartele.Length; i++)
                if (inregAbonati[i] != "") nr_cartele_vandute++;
            info = "Numarul de cartele vandute = " + nr_cartele_vandute;
            Afisare carteleVandute = new Afisare();
            carteleVandute.Text = "Vandute";
            carteleVandute.ShowDialog();
        }

        private void listeClientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info = "";
            for (int i = 0; i < codCartele.Length; i++)
                if (inregAbonati[i] != "") info += inregAbonati[i] + " - (cartela - " + codCartele[i] + ")\n"; 
            if (info == "") info = "Nu sunt cartele vandute/active";
            Afisare clienti = new Afisare();
            clienti.Text = "Clienti"; 
            clienti.ShowDialog();   
        }
        

        
    }
}
