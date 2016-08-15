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
        int x, y, ParejaActual, primerI, s, j,z,w;        
        int CantClick = 0;
        int Ganando = 0;
        int Aciertos = 0;
        int Mal = 0;
        Random random = new Random();
        private void Form3_Load(object sender, EventArgs e)
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
            counter = 90;
            counter2 = 3;
            counter3 = 3;
            x = 120;
            y = 120;
            z = 390;
            w = 390;
            s = 120;
            j = 120;
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
            timer1.Interval = 1000;
            timer1.Start();
            lblTiempo.Text = counter.ToString();
            lblTiempo.ForeColor = Color.Black;
            listTarjetas = tarje.Traemelos3();
            random = new Random();
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

                if (i2 >= 0 && i2 < 8)
                {

                    fondos[i2].Location = new Point(s, 86);
                    s = fondos[i2].Right + 35;
                }
                if (i2 >= 8 && i2 < 16)
                {
                    fondos[i2].Location = new Point(j, 282);
                    j = fondos[i2].Right + 35;
                }
                if (i2 >= 16 && i2 < listRandom.Count())
                {
                    fondos[i2].Location = new Point(z, 482);
                    z = fondos[i2].Right + 35;
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
                
                if (i >= 0 && i < 8)
                {
                    estaPic.Location = new Point(x, 86);
                    x = estaPic.Right + 35;
                }
                if (i >= 8 && i < 16)
                {
                    estaPic.Location = new Point(y, 282);
                    y = estaPic.Right + 35;

                }
                if (i >= 16 && i < listRandom.Count())
                {
                    estaPic.Location = new Point(w, 482);
                    w = estaPic.Right + 35;
                }
                this.Controls.Add(estaPic);
                pictures[i] = estaPic;
            }
        }
        PictureBox picAnterior;
        PictureBox img;
        PictureBox imgAnterior;
        PictureBox fotito;
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
                            label3.Text = Ganando.ToString();
                            for (int j = 0; j < listRandom.Count(); j++)
                            {
                                fondos[j].Click -= new EventHandler(fondos_Click);
                            }
                            timer1.Stop();
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
                            timer1.Stop();
                            this.timer3.Start();
                        }
                    }
                    if (Ganando == (listRandom.Count() / 2))
                    {
                        timer1.Stop();

                        CustomMessageForm mimsgg = new CustomMessageForm("Fin");
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
        private void timer1_Tick_1(object sender, EventArgs e)
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
                counter2 = 3;
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
                timer1.Start();
            }
        }    
    }
}
