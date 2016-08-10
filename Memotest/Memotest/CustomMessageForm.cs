using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memotest
{
    public partial class CustomMessageForm : Form
    {
       
        public CustomMessageForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            this.Location = new Point(100, 100);
        }
        public CustomMessageForm(string estado)
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            btnSi.DialogResult = DialogResult.Yes;
            btnNo.DialogResult = DialogResult.No;
            btnOk.DialogResult = DialogResult.OK;
            switch (estado)
            {
                case "Ganaste":
                    pictureBox1.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "/feliz.png");
                    btnOk.Visible = false;
                    btnSi.Visible = true;
                    btnNo.Visible = true;
                    label1.Text = "¡Felicidades, ganaste!";
                    label2.Text = "¿Jugamos de nuevo?";
                    break;
                case "Perdiste":
                    pictureBox1.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "/triste.png");
                    btnOk.Visible = false;
                    btnSi.Visible = true;
                    btnNo.Visible = true;
                    label1.Text = "¡Casi lo logras!";
                    label2.Text = "¿Intentamos de nuevo?";
                    break;
                case "Nivel":
                    pictureBox1.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "/aplausos.gif");
                    btnNo.Visible = false;
                    btnSi.Visible = false;
                    btnOk.Visible = true;
                    label1.Text = "¡Felicidades!";
                    label2.Text = "Pasaste al segundo nivel";
                    break;
                case "Silabas":
                    pictureBox1.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "/aplausos.gif");
                    btnNo.Visible = false;
                    btnSi.Visible = false;
                    btnOk.Visible = true;
                    label1.Text = "¡Felicidades!";
                    label2.Text = "¡Completaste el nivel!";
                    break;
                case "Fin":
                    pictureBox1.Image = System.Drawing.Image.FromFile(Configuracion.RootFolder + "/aplausos.gif");
                    btnNo.Visible = false;
                    btnSi.Visible = false;
                    btnOk.Visible = true;
                    label1.Text = "¡Felicidades!";
                    label2.Text = "¡Completaste el juego!";
                    break;
                default:
                    break;
            }
        }
        public static void Show(string estado)
        {
            using (var form = new CustomMessageForm(estado))
            {
                form.ShowDialog();
            }
        }
        internal enum enumMessageIcon
        {
            Ganaste,
            Perdiste,
            Seguro,
        }

        internal enum enumMessageButton
        {
            Ganaste,
            Perdiste,
            Seguro,
        }
        private void CustomMessageForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(400, 200);
        }
    }
}
