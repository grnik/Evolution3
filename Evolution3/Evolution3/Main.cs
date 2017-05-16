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
using RunModel = Run.Run;

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
        }

        private void btInitDB_Click(object sender, EventArgs e)
        {
            EvoluationContext.Init(10);
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            RunModel run = new RunModel();

            MessageBox.Show(run.Search().ToString());
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            Execute.Execute execute = new Execute.Execute();
            double[] incomePar = new double[]{6, 12.0, 3.0, 4.0};
            MessageBox.Show(execute.Run(ref incomePar).ToString());
        }

        private void btDBFilling_Click(object sender, EventArgs e)
        {
            DBFilling filling = new DBFilling();

            filling.Filling();
        }

        private void btClearResult_Click(object sender, EventArgs e)
        {
            DBFilling filling = new DBFilling();

            filling.ClearResult();
        }
    }
}
