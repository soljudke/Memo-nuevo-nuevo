using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace Memotest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
        }
        PictureBox[] pictures;
        PictureBox[] fondos;
        Jugador jug = new Jugador();
        int counter;
        Tarjetas tarje = new Tarjetas();
        List<Tarjetas> listTarjetas = new List<Tarjetas>();
        List<Tarjetas> listRandom = new List<Tarjetas>();
        int x, y, ParejaActual, primerI, s, j;        
        int CantClick = 0;
        int Ganando = 0;
        Random random = new Random();
        List<int> listint = new List<int>();
        List<int> posRandom = new List<int>();
        private void Form3_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        private void Inicio()
        {
            jug.Traemelo(Jugador.username);
            counter = 60;
            x = 0;
            y = 0;
            s = 0;
            j = 0;
            timer1.Interval = 1000;
            timer1.Start();
            lblTiempo.Text = counter.ToString();
            lblTiempo.ForeColor = Color.Black;
            listTarjetas = tarje.Traemelos3();
            var random = new Random();
            listRandom = listTarjetas.OrderBy(x => random.Next()).ToList();
            pictures = new PictureBox[listRandom.Count()];
            fondos = new PictureBox[listRandom.Count()];
            for (int i2 = 0; i2 < listRandom.Count(); i2++)
            {
                fondos[i2] = new PictureBox();
                fondos[i2].Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "tarjAtras2.jpg");
                fondos[i2].AutoSize = true;
                fondos[i2].BackColor = Color.Transparent;
                fondos[i2].Click += new EventHandler(fondos_Click);
                fondos[i2].Name = listRandom[i2].id.ToString();
                fondos[i2].BringToFront();

                if (i2 >= 0 && i2 < 10)
                {

                    fondos[i2].Location = new Point(s, 94);
                    s = fondos[i2].Right + 55;
                }
                if (i2 >= 10 && i2 < listRandom.Count())
                {
                    fondos[i2].Location = new Point(j, 340);
                    j = fondos[i2].Right + 55;
                }
                this.Controls.Add(fondos[i2]);
            }
            for (int i = 0; i < listRandom.Count(); i++)
            {

                PictureBox estaPic = new PictureBox();
                estaPic.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + listRandom[i].foto);
                estaPic.AutoSize = true;
                //estaPic.Size = new Size(20, 30);
                estaPic.BackColor = Color.Transparent;
                estaPic.SendToBack();
                estaPic.Name = listRandom[i].id.ToString();
                posRandom.Add(x);
                if (i >= 0 && i < 10)
                {
                    estaPic.Location = new Point(x, 94);
                    x = estaPic.Right + 55;
                }
                if (i >= 10 && i < listRandom.Count())
                {
                    estaPic.Location = new Point(y, 340);
                    y = estaPic.Right + 55;

                }
                this.Controls.Add(estaPic);
                pictures[i] = estaPic;
            }

        }
        PictureBox picAnterior;
        PictureBox img;
        PictureBox imgAnterior;
        protected void fondos_Click(object sender, EventArgs e)
        {
            var pic = sender as PictureBox;
            img = pictures.FirstOrDefault(p => p.Name == pic.Name);
            for (int i = 0; i < listRandom.Count(); i++)
            {
                if (pic != null && pic.Name == fondos[i].Name)
                {
                    if (CantClick == 0)
                    {
                        ParejaActual = listRandom[i].pareja;
                        CantClick = 1;
                        primerI = i;
                        picAnterior = pic;
                        imgAnterior = img;
                        pic.Visible = false;

                    }
                    else if (CantClick == 1)
                    {
                        pic.Visible = false;
                        if (ParejaActual == listRandom[i].pareja)
                        {

                            (new SoundPlayer(Configuracion.RootFolder + "aplau.wav")).Play();
                            MessageBox.Show("Si");
                            Ganando++;
                            CantClick = 0;
                            img.Visible = false;
                            imgAnterior.Visible = false;
                            Application.DoEvents();

                        }
                        else
                        {

                            (new SoundPlayer(Configuracion.RootFolder + "sad.wav")).Play();
                            MessageBox.Show("No");
                            CantClick = 0;
                            pic.Visible = true;
                            picAnterior.Visible = true;
                        }
                    }
                    if (Ganando == (listRandom.Count() / 2))
                    {
                        timer1.Stop();
                        jug.nivelmemo++;
                        jug.Modificar();
                        CustomMessageForm mimsgg = new CustomMessageForm("Nivel");
                        DialogResult resultt = mimsgg.ShowDialog();
                        if (resultt == DialogResult.OK)
                        {
                            Niveles el = new Niveles();
                            el.Show();
                            this.Close();
                        }
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
                CustomMessageForm mimsg = new CustomMessageForm("Perdiste");
                DialogResult result = mimsg.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    Inicio();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            if (counter == 15)
                lblTiempo.ForeColor = Color.Red;
            lblTiempo.Text = counter.ToString();
        }
    }
}
