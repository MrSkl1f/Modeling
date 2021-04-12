using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02
{
    class Algorithm
    {
        readonly List<double> arrI      = new List<double>() { 0.5, 1, 5, 10, 50, 200, 400, 800, 1200 };
        readonly List<double> arrTZero  = new List<double>() { 6700, 6790, 7150, 7270, 8010, 9185, 10010, 11140, 12010 };
        readonly List<double> arrM      = new List<double>() { 0.5, 0.55, 1.7, 3.0, 11.0, 32.0, 40.0, 41.0, 39.0 };
        readonly List<double> arrT      = new List<double>() { 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000 };
        readonly List<double> arrSigma  = new List<double>() { 0.031, 0.27, 2.05, 6.06, 12.0, 19.9, 29.6, 41.1, 54.1, 67.7, 81.5 };

        readonly double tw      = 2000;
        double RpForGraph       = 0;
        double TZeroForGraph    = 0;

        List<double> IGraph;
        List<double> UGraph;
        List<double> TGraph;
        List<double> RpGraph;
        List<double> TZeroGraph;
        List<double> IRPGraph;

        public Algorithm()
        {
            IGraph = new List<double>();
            UGraph = new List<double>();
            TGraph = new List<double>();
            RpGraph = new List<double>();
            TZeroGraph = new List<double>();
            IRPGraph = new List<double>();
        }

        double Interpolation(double Y, List<double> tableY, List<double> table)
        {
            int iMax = 0,
                iMin = 0;
            for (int i = 0; i < tableY.Count; i++)
            {
                if (Y > tableY[i])
                    iMax = i;
                else
                {
                    iMax = i;
                    break;
                }
            }
            if (iMax == 0)
                iMax = 1;
            iMin = iMax - 1;
            return table[iMin] + (table[iMax] - table[iMin]) / (tableY[iMax] - tableY[iMin]) * (Y - tableY[iMin]);
        }

        double GetInt(double I, double arg)
        {
            double tZero = Interpolation(I, arrI, arrTZero);
            TZeroForGraph = tZero;
            double m = Interpolation(I, arrI, arrM);
            double t = tZero + (tw - tZero) * (Math.Pow(arg, m));
            double sigma = Interpolation(t, arrT, arrSigma);
            return sigma * arg;
        }

        double CalculateIntegral(double I)
        {
            double a = 0,
                b = 1,
                n = 100,
                h = (b - a) / n,
                result = (GetInt(I, a) + GetInt(I, b)) / 2,
                curr = 0;

            for (int k = 0; k < n - 1; k++)
            {
                curr += h;
                result += GetInt(I, curr);
            }
            return result * h;
        }

        double GetRp(double Le, double R, double I)
        {
            double res = Le / (2 * Math.PI * Math.Pow(R, 2) * CalculateIntegral(I));
            return res;
        }

        double f(double I, double U, double Le, double R, double Lk, double Rk)
        {
            RpForGraph = GetRp(Le, R, Math.Abs(I));
            return (U - (Rk + RpForGraph) * I) / Lk;
        }

        double g(double I, double Ck)
        {
            return -I / Ck;
        }

        List<double> GetCoefs(double I, double U, double Le, double R, double Lk, double hn, double Rk, double Ck)
        {
            double k1 = f(I, U, Le, R, Lk, Rk),
                q1 = g(I, Ck),
                k2 = f(I + hn * k1 / 2, U + hn * q1 / 2, Le, R, Lk, Rk),
                q2 = g(I + hn * k1 / 2, Ck),
                k3 = f(I + hn * k2 / 2, U + hn * q2 / 2, Le, R, Lk, Rk),
                q3 = g(I + hn * k2 / 2, Ck),
                k4 = f(I + hn * k3, U + hn * q3, Le, R, Lk, Rk),
                q4 = g(I + hn * k3, Ck);
            return new List<double>() { k1, k2, k3, k4, q1, q2, q3, q4 };
        }

        List<double> GetIAndU(double I, double U, double Le, double R, double Lk, double hn, double Rk, double Ck)
        {
            List<double> coefs = GetCoefs(I, U, Le, R, Lk, hn, Rk, Ck);
            List<double> result = new List<double>() { 
                I + hn * (coefs[0] + 2 * coefs[1] + 2 * coefs[2] + coefs[3]) / 6,
                U + hn * (coefs[4] + 2 * coefs[5] + 2 * coefs[6] + coefs[7]) / 6
            };
            return result;
        }

        public List<List<double>> Calculate()
        {
            double R = 0.35,
                Le = 12,
                Lk = 0.000187 * 2,
                Ck = 0.000268,
                Rk = 0.25 / 5,
                Uc = 1400,
                I = 0,
                hn = 0.000001,
                t = 0;

            List<double> currIAndU;

            for (int k = 0; k < 1000; k++)
            {
                currIAndU = GetIAndU(I, Uc, Le, R, Lk, hn, Rk, Ck);
                I = currIAndU[0];
                Uc = currIAndU[1];
                t += hn;
                IGraph.Add(I);
                UGraph.Add(Uc);
                TGraph.Add(t);
                RpGraph.Add(RpForGraph);
                TZeroGraph.Add(TZeroForGraph);
                IRPGraph.Add(I * RpForGraph);
            }

            return new List<List<double>>() { IGraph, UGraph, RpGraph, IRPGraph, TZeroGraph, TGraph };
        }

    }
}
