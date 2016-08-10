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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MinimizeBox = false;
            MaximizeBox = false;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        //1382x744
        private void Inicio()
        {
            jug.Traemelo(Jugador.username);
            counter = 60;
            x = 150;
            y = 150;
            s = 150;
            j = 150;
            timer1.Interval = 1000;
            timer1.Start();
            lblTiempo.Text = counter.ToString();
            lblTiempo.ForeColor = Color.Black;
            listTarjetas = tarje.Traemelos();
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
                //posRandom.Add(y);


                if (i2 >= 0 && i2 < 6)
                {

                    fondos[i2].Location = new Point(s, 94);
                    s = fondos[i2].Right + 55;
                }
                if (i2 >= 6 && i2 < listRandom.Count())
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

                estaPic.BackColor = Color.Transparent;
                estaPic.SendToBack();
                estaPic.Name = listRandom[i].id.ToString();
                posRandom.Add(x);
                if (i >= 0 && i < 6)
                {

                    estaPic.Location = new Point(x, 94);

                    x = estaPic.Right + 55;

                }
                if (i >= 6 && i < listRandom.Count())
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


                            SoundPlayer simpleSound = (new SoundPlayer(Configuracion.RootFolder + "aplau.wav"));
                            simpleSound.Play();
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
