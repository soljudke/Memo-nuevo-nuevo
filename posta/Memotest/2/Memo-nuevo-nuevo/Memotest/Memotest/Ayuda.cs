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
    public partial class Ayuda : Form
    {
        public Ayuda()
        {
            MaximizeBox = false;
            MinimizeBox = false;
            this.Location = new Point(100, 100);
            InitializeComponent();
        }

        private void Ayuda_Load(object sender, EventArgs e)
        {
            this.Location = new Point(300, 200);
        }
    }
}
