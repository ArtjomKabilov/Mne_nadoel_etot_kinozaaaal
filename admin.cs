using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
   
    class admin: System.Windows.Forms.Form
    {

        static string connect_KinoDB = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artem Kabilov\OneDrive\Рабочий стол\MneNadoelo_kinozal-master\AppData\Database1.mdf;Integrated Security=True";
        public SqlConnection connect_to_DB = new SqlConnection(connect_KinoDB);

        SqlCommand command;
        SqlDataAdapter adapter;
        TextBox lg, ps;
        Button btn2;
        MessageBox mb;

        public admin()
        {
            this.Size = new System.Drawing.Size(300, 500);
            this.BackgroundImage = new Bitmap(@"../../image/kos.jpg");
            lg = new TextBox()
            {
               Size = new Size(100,500),
               Location = new Point(100,100),
               

            };
            this.Controls.Add(lg);
            ps = new TextBox()
            {
                Size = new Size(100, 500),
                Location = new Point(100, 200),
                PasswordChar = '*'
            };
            this.Controls.Add(ps);
            btn2 = new Button()
            {
                Size = new Size(100, 50),
                Location = new Point(100, 250),
                Text = "Logi sisse",
                
            };
            this.Controls.Add(btn2);
            btn2.MouseClick += Btn2_MouseClick1;
            
        }

        private void Btn2_MouseClick1(object sender, MouseEventArgs e)
        {
            if (ps.Text == "admin" && lg.Text == "admin")
            {
                this.Size = new System.Drawing.Size(600, 800);
                this.BackgroundImage = new Bitmap(@"../../image/kos.jpg");
                Button btn = new Button()
                {
                    Size = new Size(75, 75),
                    Location = new Point(20, 30),
                    Text = "vaata tabel"
                };
                btn.MouseClick += Btn_MouseClick;
                this.Controls.Add(btn);
                lg.Hide();
                ps.Hide();
                btn2.Hide();
            }
            else
            {
               MessageBox.Show("Vale sisselogimine või parool");
            }

        }

        /*public void Data()
{
   connect_to_DB.Open();
   tabel = new DataTable();
   dataGridView = new DataGridView();
   dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick; ;
   adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Movie]", connect_to_DB);
   adapter.Fill(tabel);
   dataGridView.DataSource = tabel;
   dataGridView.Location = new System.Drawing.Point(10, 75);
   dataGridView.Size = new System.Drawing.Size(400, 200);
   this.Controls.Add(dataGridView);
   this.Controls.Add(nameText);
   this.Controls.Add(dateText);
   this.Controls.Add(genreText);
   this.Controls.Add(imageText);
   this.Controls.Add(posters);
   connect_to_DB.Close();
}

private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
{
   id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
   nameText.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
   dateText.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
   genreText.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
   imageText.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
   posters.Image = Image.FromFile(@"C:..\..\image\" + dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
}*/

        private void ClearData()
        {
            //Id = 0;
            nameText.Text = "";
            dateText.Text = "";
            genreText.Text = "";
            imageText.Text = "";
            //save.FileName = "";
            posters.Image = Image.FromFile("../../image/index.png");

        }
        private void Del_MouseClick(object sender, MouseEventArgs e)
        {
            if (id != 0)
            {
                command = new SqlCommand("DELETE Movie WHERE ID_movie=@ID_movie", connect_to_DB);
                connect_to_DB.Open();
                command.Parameters.AddWithValue("@ID_movie", id);
                command.ExecuteNonQuery();
                //Data();
                ClearData();
                connect_to_DB.Close();
                MessageBox.Show("Andmed on kustutatud");
            }
            else
            {
                MessageBox.Show("Viga kustutamisega");
            }
        }

        private void FilmiAdd_MouseClick(object sender, MouseEventArgs e)
        {
            if (nameText.Text != "" && dateText.Text != "" && genreText.Text != "" && posters.Image != null)
            {
                command = new SqlCommand("UPDATE Movie SET name=@name,date=@date,genre=@genre, image=@image WHERE ID_movie=@ID_movie", connect_to_DB);
                connect_to_DB.Open();
                command.Parameters.AddWithValue("@ID_movie", id);
                command.Parameters.AddWithValue("@name", nameText.Text);
                command.Parameters.AddWithValue("@date", dateText.Text);
                command.Parameters.AddWithValue("@genre", genreText.Text.Replace(",", "."));
                string file_pilt = imageText.Text;
                command.Parameters.AddWithValue("@image", file_pilt);
                command.ExecuteNonQuery();
                //Data();
                ClearData();
                connect_to_DB.Close();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }
        TextBox nameText, dateText, genreText, imageText;
        PictureBox posters;
        DataTable tabel;
        DataGridView dataGridView;
        int id;
        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            nameText = new TextBox
            {
                Location = new System.Drawing.Point(100, 200)
            };
            dateText = new TextBox
            {
                Location = new System.Drawing.Point(100, 225)
            };
            genreText = new TextBox
            {
                Location = new System.Drawing.Point(100, 250)
            };
            imageText = new TextBox
            {
                Location = new System.Drawing.Point(100, 275)
            };
            posters = new PictureBox()
            {
                Size=new System.Drawing.Size(180,250),
                Location = new System.Drawing.Point(300, 150),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Button filmiAdd = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(220, 300),
                Text = "Värskendada film"
            };
            Button del = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(100, 30),
                Text = "Kustuta"
            };
            Button lisa = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(220, 200),
                Text = "Lisa film"
            };
            connect_to_DB.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick1;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID_movie, name,date,genre,image FROM [dbo].[Movie]", connect_KinoDB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(150, 400);
            dataGridView.Size = new System.Drawing.Size(400, 200);
            this.Controls.Add(dataGridView);
            this.Controls.Add(filmiAdd);
            filmiAdd.MouseClick += FilmiAdd_MouseClick;

            this.Controls.Add(nameText);
            this.Controls.Add(dateText);
            this.Controls.Add(genreText);
            this.Controls.Add(imageText);
            this.Controls.Add(posters);

            this.Controls.Add(del);
            this.Controls.Add(lisa);
            del.MouseClick += Del_MouseClick;
            lisa.MouseClick += Lisa_MouseClick;

            connect_to_DB.Close();
        }

        private void Lisa_MouseClick(object sender, MouseEventArgs e)
        {
            if (nameText.Text != "" && dateText.Text != "" && genreText.Text != "" && imageText.Text != "")
            {
                command = new SqlCommand("INSERT INTO Movie(name,date,genre,image) VALUES(@name,@date,@genre,@image)", connect_to_DB);
                connect_to_DB.Open();
                command.Parameters.AddWithValue("@name", nameText.Text);
                command.Parameters.AddWithValue("@date", dateText.Text);
                command.Parameters.AddWithValue("@genre", genreText.Text.Replace(",", "."));
                command.Parameters.AddWithValue("@image", imageText.Text);
                //string file_pilt = imageText.Text;
                //command.Parameters.AddWithValue("@image", file_pilt);
                command.ExecuteNonQuery();
                //Data();
                ClearData();


                connect_to_DB.Close();
                MessageBox.Show("Andmed uuendatud");

                
            }
            else
            {
                MessageBox.Show("Viga");
            }

        }

        private void DataGridView_RowHeaderMouseClick1(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            nameText.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            dateText.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            genreText.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            imageText.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            posters.Image = Image.FromFile(@"C:..\..\image\" + dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
            
        }

    }
}
