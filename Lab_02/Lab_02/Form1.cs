using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            Algorithm alg = new Algorithm();
            List<List<double>> result = alg.Calculate();
            CreateGraph.GetGraphI(result[0], this.chart1, result[5]);
            /*
            CreateGraph.GetGraph(result[0], this.chart1, "I", result[5]);
            CreateGraph.GetGraph(result[1], this.chart2, "Uc", result[5]);
            CreateGraph.GetGraph(result[2], this.chart3, "Rp", result[5]);
            CreateGraph.GetGraph(result[3], this.chart4, "I * Rp", result[5]);
            CreateGraph.GetGraph(result[4], this.chart5, "T0", result[5]);
            */
        }
    }
}
