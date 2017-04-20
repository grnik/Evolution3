using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataDB;

namespace Evolution3
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btCreateDB_Click(object sender, EventArgs e)
        {
            EvoluationContext context = new EvoluationContext();

            context.Database.CreateIfNotExists();

            Init.Fill();
        }

        private void btInitDB_Click(object sender, EventArgs e)
        {
            EvoluationContext.Init(10);
        }
    }
}
