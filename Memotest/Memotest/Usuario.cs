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
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
            MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
        }

        public string nombrecito;
        bool flag = false;
        int cont = 0;
        int cont2 = 0;
        private void Usuario_Load(object sender, EventArgs e)
        {
            textBox1.KeyUp += TextBoxKeyUp;
        }
        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                nombrecito = textBox1.Text;
                Jugador jug = new Jugador();
                jug.Jugadores();
                foreach (Jugador juga in jug.Jugadores())
                {
                    if (juga.nombre == nombrecito)
                    {
                        Jugador.username = nombrecito;
                        Niveles us = new Niveles();
                        us.Show();
                        this.Hide();
                        return;
                    }
                    cont++;
                }
                if (cont == jug.Jugadores().Count)
                {
                    foreach (Jugador juga in jug.Jugadores())
                    {
                        if (juga.nombre == nombrecito)
                        {
                            label3.Visible = true;
                            return;
                        }
                        cont2++;
                    }
                    if (cont2 == jug.Jugadores().Count)
                    {

                        jug.nombre = nombrecito;
                        Jugador.username = nombrecito;
                        jug.AgregameJugador();
                        Niveles us = new Niveles();
                        us.Show();
                        this.Hide();
                        return;
                    }

                }
            }

        }
    }
}
