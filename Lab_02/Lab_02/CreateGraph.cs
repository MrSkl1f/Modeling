using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_02
{
    static class CreateGraph
    {
        public static void GetGraph(List<double> result, Chart graph, string name, List<double> t)
        {
            Series series = new Series(name);
            graph.Titles.Clear();
            graph.Titles.Add("Graph for " + name);
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Red;
            series.BorderWidth = 2;
            for (int i = 0; i < result.Count; i++)
            {
                series.Points.AddXY(t[i], result[i]);
            }
            graph.Series.Clear();
            graph.Series.Add(series);
        }

        public static void GetGraphI(List<double> result, Chart graph, List<double> t)
        {
            graph.Titles.Clear();
            graph.Titles.Add("Graph for I with Imax");

            double maxI = result.Max() * 0.35;
            Series maxIseries = new Series("maxI");
            maxIseries.ChartType = SeriesChartType.Line;
            maxIseries.Color = Color.Blue;
            maxIseries.BorderWidth = 2;
            for (int i = 0; i < result.Count; i++)
            {
                maxIseries.Points.AddXY(t[i], maxI);
            }
            Series series = new Series("Graph I");
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Red;
            series.BorderWidth = 2;
            for (int i = 0; i < result.Count; i++)
            {
                series.Points.AddXY(t[i], result[i]);
            }
            graph.Series.Clear();
            graph.Series.Add(series);
            graph.Series.Add(maxIseries);
        }
    }
}
