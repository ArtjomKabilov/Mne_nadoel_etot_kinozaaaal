using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
    public partial class room : Form
    {
        Label message = new Label();
        Button[] btn = new Button[3];
        string[] texts = new string[3];
        TableLayoutPanel tlp = new TableLayoutPanel();


        public room()
        { }
        public room(string title, string body, string button1, string button2, string button3)
        {

            texts[0] = button1;
            texts[1] = button2;
            texts[2] = button3;
            this.ClientSize = new System.Drawing.Size(400, 100);
            this.Text = title;
            int x = 10;
            this.BackColor = Color.FromArgb(21, 0, 206);
            Label lbl = new Label()
            {
                Text = "Valige saali suurus",
                Location = new System.Drawing.Point(10, 10),
                Size = new Size(240, 40),
                Font = new Font("Arial", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 61, 167)

            };
            this.Controls.Add(lbl);

            for (int i = 0; i < 3; i++)
            {
                btn[0] = new Button
                {
                    Location = new System.Drawing.Point(x, 50),
                    Size = new System.Drawing.Size(80, 25),
                    Text = texts[0],
                    BackColor = Color.FromArgb(255, 255, 255)
                };
                btn[1] = new Button
                {
                    Location = new System.Drawing.Point(x, 50),
                    Size = new System.Drawing.Size(80, 25),
                    Text = texts[1],
                    BackColor = Color.FromArgb(255, 255, 255)
                };
                btn[2] = new Button
                {
                    Location = new System.Drawing.Point(x, 50),
                    Size = new System.Drawing.Size(80, 25),
                    Text = texts[2],
                    BackColor = Color.FromArgb(255, 255, 255)
                };
                btn[0].Click += MyForm_Click;
                btn[1].Click += Room_Click;
                btn[2].Click += Room_Click1;
                x += 100;
                this.Controls.Add(btn[i]);
            }
            message.Location = new System.Drawing.Point(10, 10);
            message.Text = body;
            this.Controls.Add(message);
        }
        /*public room(int x, int y)
        {
            _x = x;
            _y = y;
            using (StreamWriter w = new StreamWriter("../../info.txt", true)) 
            {
                w.Write(""); 
            }
            using (StreamReader r = new StreamReader("../../info.txt"))
            {
                int counter = 0;
                string[] tickets = r.ReadToEnd().Split(',');
                bought = "";

                foreach (var item in tickets)
                {
                    bought += tickets;
                    counter++;
                }
            }

        }*/

        private void Room_Click1(object sender, EventArgs e)
        {
            saal saal = new saal(10, 15);
            saal.StartPosition = FormStartPosition.CenterScreen;
            saal.ShowDialog();
        }

        private void Room_Click(object sender, EventArgs e)
        {
            saal saal = new saal(6, 10);
            saal.StartPosition = FormStartPosition.CenterScreen;
            saal.ShowDialog();
        }

        private void MyForm_Click(object sender, EventArgs e)
        {
            saal saal = new saal(5, 8);
            saal.StartPosition = FormStartPosition.CenterScreen;
            saal.ShowDialog();
        }
    }
}

