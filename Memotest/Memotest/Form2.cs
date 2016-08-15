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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = System.Drawing.Image.FromFile(Configuracion.RootFolder + "Inicio.jpg");
        }
        PictureBox[] pictures;
        PictureBox[] fondos;
        Jugador jug = new Jugador();
        int counter;
        int counter2;
        int counter3;
        Tarjetas tarje = new Tarjetas();
        List<Tarjetas> listTarjetas = new List<Tarjetas>();
        List<Tarjetas> listRandom = new List<Tarjetas>();
        int x, y, ParejaActual, primerI, s, j;
        int CantClick = 0;
        int Ganando = 0;
        int Aciertos = 0;
        int Mal = 0;
        Random random = new Random();
        List<int> listint = new List<int>();
        List<int> posRandom = new List<int>();
        private void Form2_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        private void Inicio()
        {
            jug.Traemelo(Jugador.username);
            for (int i = 0; i < listRandom.Count(); i++)
            {
                if (pictures.Count() > 0)
                    pictures[i].Dispose();
                if (fondos.Count() > 0)
                    fondos[i].Dispose();
            } 
            listRandom.Clear();
            listTarjetas.Clear();
            ParejaActual = 0;
            primerI = 0;
            CantClick = 0;
            Aciertos = 0;
            Mal = 0; 
            label3.Text = Aciertos.ToString();
            label4.Text = Mal.ToString();
            Ganando = 0;
            counter = 60;
            counter2 = 3;
            counter3 = 3;
            x = 120;
            y = 120;
            s = 120;
            j = 120;
            timer1.Interval = 1000;
            timer1.Start();
            lblTiempo.Text = counter.ToString();
            lblTiempo.ForeColor = Color.Black;
            listTarjetas = tarje.Traemelos2();
            var random = new Random();
            listRandom = listTarjetas.OrderBy(x => random.Next()).ToList();
            pictures = new PictureBox[listRandom.Count()];
            fondos = new PictureBox[listRandom.Count()];
            for (int i2 = 0; i2 < listRandom.Count(); i2++)
            {
                fondos[i2] = new PictureBox();
                fondos[i2].Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "tarjAtras.jpg");
                fondos[i2].AutoSize = true;
                fondos[i2].BackColor = Color.Transparent;
                fondos[i2].Click += new EventHandler(fondos_Click);
                fondos[i2].Name = listRandom[i2].id.ToString();
                fondos[i2].BringToFront();

                if (i2 >= 0 && i2 < 8)
                {

                    fondos[i2].Location = new Point(s, 94);
                    s = fondos[i2].Right + 55;
                }
                if (i2 >= 8 && i2 < listRandom.Count())
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
                if (i >= 0 && i < 8)
                {
                    estaPic.Location = new Point(x, 94);
                    x = estaPic.Right + 55;
                }
                if (i >= 8 && i < listRandom.Count())
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Ayuda ayu = new Ayuda();
            timer1.Stop();
            ayu.ShowDialog();
            timer1.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Niveles el = new Niveles();
            el.Show();
            this.Close();
        }

        PictureBox fotito;
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
                            Ganando++;
                            label3.Text = Aciertos.ToString();
                            for (int j = 0; j < listRandom.Count(); j++)
                            {
                                fondos[j].Click -= new EventHandler(fondos_Click);
                            }
                            this.timer2.Start();
                           

                        }
                        else
                        {
                           
                            (new SoundPlayer(Configuracion.RootFolder + "sad.wav")).Play();
                            Mal++;
                            fotito = pic;
                            label4.Text = Mal.ToString();
                            for (int j = 0; j < listRandom.Count(); j++)
                            {
                                fondos[j].Click -= new EventHandler(fondos_Click);
                            }
                            this.timer3.Start();
                        }
                    }
                    if (Ganando == (listRandom.Count() / 2))
                    {
                        timer1.Stop();
                        if (jug.nivelmemo == 2)
                        {
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
                        else
                        {
                            CustomMessageForm mimsgg = new CustomMessageForm("Ganaste");
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
                    Niveles niv = new Niveles();
                    niv.Show();
                    this.Close();
                }
            }
            if (counter == 15)
                lblTiempo.ForeColor = Color.Red;
            lblTiempo.Text = counter.ToString();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            counter2--;
            if (counter2 == 0)
            {
                CantClick = 0;
                img.Visible = false;
                imgAnterior.Visible = false;
                Application.DoEvents();
                for (int j = 0; j < listRandom.Count(); j++)
                {
                    fondos[j].Click += new EventHandler(fondos_Click);
                }
                counter2 = 5;
                timer2.Stop();
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            counter3--;
            if (counter3 == 0)
            {
                CantClick = 0;
                fotito.Visible = true;
                picAnterior.Visible = true;
                Application.DoEvents();
                for (int j = 0; j < listRandom.Count(); j++)
                {
                    fondos[j].Click += new EventHandler(fondos_Click);
                }
                counter3 = 3;
                timer3.Stop();
            }
        }
    }
}
