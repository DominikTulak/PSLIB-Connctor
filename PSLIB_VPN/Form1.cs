using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotRas;
using System.Diagnostics;

namespace PSLIB_VPN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            radioButton2.Checked = true;
            if (radioButton1.Checked == false)
            {
                textBox3.Enabled = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {

                try
                {
                    this.rasPhoneBook1.Open();
                }
                catch
                {
                    MessageBox.Show("Spusťte aplikaci jako správce!", "Chyba!");
                    this.Close();
                }

                if (this.rasPhoneBook1.Entries.Count == 0)
                {
                    RasEntry entry = RasEntry.CreateVpnEntry("PSLIB_VPN", "vpn.pslib.cz", RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn, false));
                    this.rasPhoneBook1.Entries.Add(entry);
                }

                System.Diagnostics.Process.Start("rasdial.exe", "PSLIB_VPN " + textBox1.Text + "@pslib.cz " + textBox2.Text);
                //System.Diagnostics.Process.Start("rasdial.exe", "PSLIB_VPN " + textBox1.Text + "@pslib.cz " + textBox2.Text);
                this.Close();
            }
            if (radioButton1.Checked == true)
            {
                try
                {
                    if (radioButton1.Checked == true) System.Threading.Thread.Sleep(10000);
                    System.Diagnostics.Process.Start("CMD.exe", "/C net use S: \\\\10.2.1.2\\public " + textBox3.Text + " /USER:AD\\" + textBox1.Text + " /persistent:yes");
                    System.Diagnostics.Process.Start("CMD.exe", "/C net use X: \\\\10.2.1.2\\" + textBox1.Text + " " + textBox3.Text + " /USER:AD\\" + textBox1.Text);

                }
                catch
                {
                    MessageBox.Show("Vyskytla se chyba s připojením disků", "Chyba!");
                }
                this.Close();
            }
        }

        

        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                System.Diagnostics.Process.Start("CMD.exe", "/C net use S: /delete");
                System.Diagnostics.Process.Start("CMD.exe", "/C net use X: /delete");

            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba s odpojením disků", "Chyba!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("rasdial.exe", "PSLIB_VPN /d");
            }
            catch
            {
                MessageBox.Show("VPN není připojeno!", "Chyba!");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox3.Enabled = true;
                textBox2.Enabled = false;
            }
            else
            {
                textBox3.Enabled = false;
                textBox2.Enabled = true;
            }
                

            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                textBox3.Enabled = false;
                textBox2.Enabled = true;
            }
            else
            {
                textBox3.Enabled = true;
                textBox2.Enabled = false;
            }
        }


    }

}
