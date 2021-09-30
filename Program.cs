using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static double FindVel(double coord1, double coord2, double v1, double v2, double delta)
        {
            double r = Math.Sqrt(coord1 * coord1 + coord2 * coord2);
            v1 = v1 + (-1 / (r * r * r) * coord1) * 4*Math.PI*Math.PI * delta;
            return v1;
        }
        public static double FindCoord(double coord1, double v1, double delta)
        {
            coord1 = coord1 + v1 * delta;
            return coord1;
        }
        public static double[] FindPhase(double coord1, double coord2, double v1, double v2, double delta, double time)
        {
            double finalTime = time;
            double[] phase = new double[4];
            while (time <= finalTime + 0.05)
            {
                v2 = FindVel(coord2, coord1, v2, v1, delta);
                v1 = FindVel(coord1, coord2, v1, v2, delta);
                coord1 = FindCoord(coord1, v1, delta);
                coord2 = FindCoord(coord2, v2, delta);
                time += delta;
            }
            phase[0] = coord1;
            phase[1] = coord2;
            phase[2] = v1;
            phase[3] = v2;
            return phase;
        }
    }
}
