using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
    public partial class saal : Form
    {
        cinema cinema = new cinema();
        Label message = new Label();
        TableLayoutPanel tlp = new TableLayoutPanel();
        Button btn_tabel;
        static List<Pilet> piletid;
        static string[] read_kohad;
        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artem Kabilov\OneDrive\Рабочий стол\MneNadoelo_kinozal-master\AppData\Database1.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;
        SqlDataAdapter adapter;

        public saal(int read, int kohad)
        {
            this.tlp.ColumnCount = kohad;
            this.tlp.RowCount = read;
            this.tlp.ColumnStyles.Clear();
            this.tlp.RowStyles.Clear();
            piletid = new List<Pilet> { };
            int i, j;
            read_kohad = Ostetud_piletid();

            for (i = 0; i < read; i++)
            {
                this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50 / read));
            }
            for (i = 0; i < kohad; i++)
            {
                this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50 / kohad));
            }
            this.Size = new System.Drawing.Size(kohad * 50, read * 50);
            for (int r = 0; r < read; r++)
            {
                for (int k = 0; k < kohad; k++)
                {
                    btn_tabel = new Button
                    {
                        Name = String.Format($"{r+1}{k+1}"),
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightGreen

                    };
                    this.tlp.Controls.Add(btn_tabel, k, r);
                    btn_tabel.MouseClick += Btn_tabel_MouseClick;
                    foreach (var item in read_kohad)
                    {
                        if (item.ToString() == btn_tabel.Name)
                        {
                            btn_tabel.BackColor = Color.Red;
                            btn_tabel.Enabled = false;
                        }
                    }


                }
            }
            //btn_w = (int)(100 / kohad);
            //btn_h = (int)(100 / read);
            this.tlp.Dock = DockStyle.Fill;
            //this.tlp.Size = new System.Drawing.Size(tlp.ColumnCount*btn_w*3,tlp.RowCount * btn_h*2);
            this.Controls.Add(tlp);
            message.Location = new System.Drawing.Point(10, 10);
            message.Text = "Kas tahad saada e-mailile?";
            this.Controls.Add(message);


        }
        public string[] Ostetud_piletid()
        {
            try
            {
                /*StreamReader f = new StreamReader(@"..\..\info.txt");
                read_kohad = f.ReadToEnd().Split(';');
                f.Close();*/
                connect_to_DB.Open();
                adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Piletid]", connect_to_DB);
                DataTable tabel = new DataTable();
                adapter.Fill(tabel);
                read_kohad = new string[tabel.Rows.Count];
                var index = 0;
                foreach (DataRow row in tabel.Rows)
                {
                    var rida = row["Rida"];
                    var koht = row["Koht"];
                    read_kohad[index++] = $"{rida}{koht}";
                }
                connect_to_DB.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return read_kohad;
        }

        string pocta = "";
        private void Saada_piletid(List<Pilet> piletid)
        {

            pocta = Interaction.InputBox("Email", "Email");

            if (pocta != "")
            {
                var filmivaata = File.ReadLines(@"../../info.txt").Last();
                connect_to_DB.Open();
                string text = "Kinoteatr: „Planet“\n ";
                foreach (var item in piletid)
                {
                    text += "Film:"+ cinema.FilmName +"\n " + "Rida: " + item.Rida + "Koht: " + item.Koht + "\n";
                    command = new SqlCommand("INSERT INTO Piletid(rida,koht,film) VALUES(@rida,@koht,@film)", connect_to_DB);
                    command.Parameters.AddWithValue("@rida", item.Rida);
                    command.Parameters.AddWithValue("@koht", item.Koht);
                    command.Parameters.AddWithValue("@film", cinema.FilmName);
                    command.ExecuteNonQuery();
                }
                connect_to_DB.Close();

                MailMessage message = new MailMessage();
                if (pocta.EndsWith("@gmail.com") || pocta.EndsWith("@mail.ru") || pocta.EndsWith("@bk.ru") || pocta.EndsWith("@list.ru") || pocta.EndsWith("@tthk.ee"))
                {
                    message.To.Add(new MailAddress(pocta));
                    message.From = new MailAddress(pocta);
                    message.Subject = "Ostetud piletid";
                    message.Body = text;
                    string email = "programmeeriminetthk2@gmail.com";
                    string password = "2.kuursus tarpv20";
                    SmtpClient client = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(email, password),
                        EnableSsl = true,
                    };
                    try
                    {
                        client.Send(message);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());

                    }

                }
                else
                {
                    if (MessageBox.Show("E-post on valesti sisestatud.\nKas soovite korrata?", "Viga", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        Saada_piletid(piletid);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }

                }
            }
            else
            {
                if (MessageBox.Show("E-post on valesti sisestatud.\nKas soovite korrata?", "Viga", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Saada_piletid(piletid);
                }
                else
                {
                    Environment.Exit(0);
                }

            }



        }
        
        private void Btn_tabel_MouseClick(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            btn_click.BackColor = Color.Yellow;
            //MessageBox.Show(btn_click.Name.ToString());
            var rida = int.Parse(btn_click.Name[0].ToString());
            var koht = int.Parse(btn_click.Name[1].ToString());

            var vas = MessageBox.Show("Sinu pilet on: Rida: " + rida + " Koht: " + koht, "Kas ostad?", MessageBoxButtons.YesNo);
            if (vas == DialogResult.Yes)
            {
                btn_click.BackColor = Color.Red;
                btn_click.Enabled = false;
                try
                {
                    Pilet pilet = new Pilet(rida, koht);
                    piletid.Add(pilet);
                    StreamWriter ost = new StreamWriter(@"../../info.txt", true);
                    ost.Write(btn_click.Name.ToString() + ';');
                    ost.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (vas == DialogResult.No)
            {
                btn_click.BackColor = Color.Green;
            };
            if (piletid.Count() > 0)
            {
                if (MessageBox.Show("Sul on ostetud: " + piletid.Count() + " piletid", "Kas tahad saada neid e-mailile?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Saada_piletid(piletid);
                }
            }

        }






    }
}