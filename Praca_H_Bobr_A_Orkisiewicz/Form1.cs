using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Praca_H_Bobr_A_Orkisiewicz
{
    public partial class Form1 : Form
    {
        private MySqlConnection con = new MySqlConnection();
        
        public Form1()
        {
            InitializeComponent();
            con.ConnectionString = @"server=localhost;database=user_info;userid=root;password=;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MessageBox.Show("Witamy w Kawiarni Ce Kratka");
                con.Close();
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.BlueViolet;
            radioButton2.ForeColor = System.Drawing.Color.RosyBrown;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Czarna Kawa");
            comboBox1.Items.Add("Expresso");
            comboBox1.Items.Add("Latte");
            comboBox1.Items.Add("Gorąca Czekolada");
            comboBox1.Items.Add("Kawa Mrożona");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.RosyBrown;
            radioButton2.ForeColor = System.Drawing.Color.BlueViolet;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Szarlotka");
            comboBox1.Items.Add("Sernik");
            comboBox1.Items.Add("Kremówka");
            comboBox1.Items.Add("Tarta Owocowa");
            comboBox1.Items.Add("Ptyś");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Czarna Kawa":
                    textBox1.Text = "50";
                    break;

                case "Expresso":
                    textBox1.Text = "100";
                    break;

                case "Latte":
                    textBox1.Text = "150";
                    break;

                case "Gorąca Czekolada":
                    textBox1.Text = "180";
                    break;

                case "Kawa Mrożona":
                    textBox1.Text = "190";
                    break;

                case "Szarlotka":
                    textBox1.Text = "100";
                    break;

                case "Sernik":
                    textBox1.Text = "175";
                    break;

                case "Kremówka":
                    textBox1.Text = "250";
                    break;

                case "Tarta Owocowa":
                    textBox1.Text = "280";
                    break;

                case "Ptyś":
                    textBox1.Text = "300";
                    break;

                default:
                    textBox1.Text = "0";
                    textBox3.Text = "";
                    textBox2.Text = "";
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length > 0)
            {
                textBox2.Text = (Convert.ToInt16(textBox1.Text) * Convert.ToInt16(textBox3.Text)).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Add(comboBox1.Text, textBox1.Text, textBox3.Text, textBox2.Text, dateTimePicker1.Text);
            textBox4.Text = (Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox2.Text)).ToString();
            //int n = dataGridView1.Rows.Add();
            //dataGridView1.Rows[n].Cells[0].Value = (n + 1).ToString();
            //dataGridView1.Rows[n].Cells[1].Value = dateTimePicker1.Value.ToString();

            //comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
             if (textBox5.Text.Length > 0)
            {
                textBox6.Text = (Convert.ToInt16(textBox5.Text) - Convert.ToInt16(textBox4.Text)).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //index = dataGridView1.CurrentCell.RowIndex;
            //dataGridView1.Rows.RemoveAt(index);


            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        textBox4.Text = (Convert.ToInt16(textBox4.Text) - Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value)).ToString();
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }


            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int reszta = Int32.Parse(textBox6.Text);
            if (reszta >= 0)
            { 
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO item_tbl Values ('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "')", con);
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO item_tbl (Date) Values ('" + dateTimePicker1.Value.ToString() + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Zamówienie zostało przyjęte :)");
                    con.Close();
                }
                dataGridView1.Rows.Clear();

                textBox4.Text = "0";
                textBox5.Text = "";
                textBox6.Text = "";
            } else {MessageBox.Show("Proszę uregulować rachunek"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox4.Text = "0";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        //Bitmap bmp;


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            //dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            //e.Graphics.DrawImage(bmp, 100, 150);

            //e.Graphics.DrawString("COFFEE SHOP", new Font("Arial", 20, FontStyle.Bold), Brushes.Red, new Point(185, 10));
            //e.Graphics.DrawString("Sale Recipt", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(240, 40));


        }
        private void losowanie(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int losik = rnd.Next(1, 10);
            switch (losik)
            {
                case 1:
                    this.Polecane.Text = "Kawa czarna";
                    break;
                case 2:
                    this.Polecane.Text = "Expresso";
                    break;
                case 3:
                    this.Polecane.Text = "Latte";
                    break;
                case 4:
                    this.Polecane.Text = "Gorąca Czekolada";
                    break;
                case 5:
                    this.Polecane.Text = "Kawa Mrożona";
                    break;
                case 6:
                    this.Polecane.Text = "Szarlotka";
                    break;
                case 7:
                    this.Polecane.Text = "Sernik";
                    break;
                case 8:
                    this.Polecane.Text = "Kremówka";
                    break;
                case 9:
                    this.Polecane.Text = "Tarta Owocowa";
                    break;
                case 10:
                    this.Polecane.Text = "Ptyś";
                    break;
                default:
                    this.Polecane.Text = "";
                    break;

            }
        } 

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
            f3 = null;
            this.Show();

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void dockLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dockLabel3_Click(object sender, EventArgs e)
        {

        }

        private void dockLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
