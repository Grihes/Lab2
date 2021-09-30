using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static double x1 = 0.903412226;
        static double y1 = 0.435060396;
        static double vx1 = -2.705345311;
        static double vy1 = 5.617707424;
        static double x2 = -1.29857914;
        static double y2 = -1.00974065;
        static double vx2 = 2.874083543;
        static double vy2 = -3.69622134;
        static double x3 = 4.960855451;
        static double y3 = 0.73202266974;
        static double vx3 = -0.41644877737;
        static double vy3 = 2.822222461;
        static double delta = 0.8 * (1e-5);
        static double m1 = 3.0025138260432 * (1e-6);
        static double m2 = 3.2126696832579 * (1e-7);
        static double m3 = 0.9543137254901962 * (1e-3);

        private void Form1_Load(object sender, EventArgs e)
        {
            var planet1 = new Planet(x1, y1, vx1, vy1, m1, 0);
            var planet2 = new Planet(x2, y2, vx2, vy2, m2, 1);
            var planet3 = new Planet(x3, y3, vx3, vy3, m3, 2);
            BuildOrbit(planet1, planet2, planet3, delta, true);
            BuildOrbit(planet1, planet2, planet3, delta, false);
            BuildTextBox1(planet1, planet2, planet3, delta, true);
            BuildTextBox2(planet2, planet1, planet3, delta, true);
            BuildTextBox3(planet3, planet2, planet1, delta, true);
            BuildTextBox1(planet1, planet2, planet3, delta, false);
            BuildTextBox2(planet2, planet1, planet3, delta, false);
            BuildTextBox3(planet3, planet2, planet1, delta, false);
            BuildChart1(planet1, planet2, planet3, delta);
            BuildChart2(planet1, planet2, planet3, delta);
            BuildChart3(planet1, planet2, planet3, delta);
        }
        private void BuildOrbit(Planet p1, Planet p2, Planet p3, double delta, bool interaction)
        {
            if (interaction)
            {
                chart1.Series[p1.n].Points.Clear();
                for (double i = 0; i <= 15; i += 0.05)
                {
                    chart1.Series[p1.n].Points.AddXY(p1.x, p1.y);
                    chart1.Series[p2.n].Points.AddXY(p2.x, p2.y);
                    chart1.Series[p3.n].Points.AddXY(p3.x, p3.y);
                    p1.FindPhaseInTime(p2, p3, delta, i);
                }
            }
            else
            {
                var m2= p2.m;
                var m3= p3.m;
                p2.m =p3.m = 0;
                chart1.Series[p1.n + 3].Points.Clear();
                for (double i = 0; i <= 15; i += 0.05)
                {
                    chart1.Series[p1.n + 3].Points.AddXY(p1.x, p1.y);
                    chart1.Series[p2.n + 3].Points.AddXY(p2.x, p2.y);
                    chart1.Series[p3.n + 3].Points.AddXY(p3.x, p3.y);
                    p1.FindPhaseInTime(p2, p3, delta, i);
                }
                p2.m = m2;
                p3.m=m3;
            }
        }
        private void BuildTextBox1(Planet p1, Planet p2, Planet p3, double delta, bool interaction)
        {
            if (interaction)
            {

                p1.FindMinMaxVel(p2, p3, delta);
                textBox1.Text = " Earth with interaction: " + "Max: "+'(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')' +   " Min: " + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' +" Period: "+ Math.Round(p1.T, 4 ).ToString();
            }
            else
            {
                var m2 = p2.m;
                var m3 = p3.m;
                p2.m = p3.m = 0;
                p1.FindMinMaxVel(p2, p3, delta);
                textBox4.Text = "Earth: " + "Max: "+ '(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')'  + " Min: "  + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' + " Period: " + Math.Round(p1.T, 4).ToString();
                p2.m = m2;
                p3.m = m3;
            }
        }
        private void BuildTextBox2(Planet p1, Planet p2, Planet p3, double delta, bool interaction)
        {
            if (interaction)
            {
                p1.FindMinMaxVel(p2, p3, delta);
                textBox2.Text = "Mars with interaction: " + "Max: "+ '(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')' + " Min: "  + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' + " Period: " +  Math.Round(p1.T, 4).ToString();
            }
            else
            {
                var m2 = p2.m;
                var m3 = p3.m;
                p2.m = p3.m = 0;
                p1.FindMinMaxVel(p2, p3, delta);
                textBox5.Text = "Mars " + "Max: "+  '(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')' + " Min: " + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' + " Period: "+ Math.Round(p1.T, 4).ToString();
                p2.m = m2;
                p3.m = m3;
            }
        }
        private void BuildTextBox3(Planet p1, Planet p2, Planet p3, double delta, bool interaction)
        {
            if (interaction)
            {
                p1.FindMinMaxVel(p2, p3, delta);
                textBox3.Text = "Jupiter with interaction: " + "Max: "+ '(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')' + " Min: " + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' + " Period: "+ Math.Round(p1.T, 4).ToString();
            }
            else
            {
                var m2 = p2.m;
                var m3 = p3.m;
                p2.m = p3.m = 0;
                p1.FindMinMaxVel(p2, p3, delta);
                textBox6.Text = "Jupiter: " + "Max: "+ '(' + Math.Round(p1.xMaxVel, 4).ToString() + ':' + Math.Round(p1.yMaxVel, 4).ToString() + ')' + " Min: " + '(' + Math.Round(p1.xMinVel, 4).ToString() + ':' + Math.Round(p1.yMinVel, 4).ToString() + ')' + " Period: "+Math.Round(p1.T, 4).ToString();
                p2.m = m2;
                p3.m = m3;
            }
        }
        private void BuildChart1(Planet p1, Planet p2, Planet p3, double delta)
        {
            for (double i = 0; i <= 15; i += 0.01)  
            {
                p1.FindEnergy(p2, p3, delta);
                p2.FindEnergy(p1, p3, delta);
                p3.FindEnergy(p1, p2, delta);
                chart2.Series[p1.n].Points.AddXY(i, p1.energy);
                chart2.Series[p2.n].Points.AddXY(i, p2.energy);
                chart2.Series[p3.n].Points.AddXY(i, p3.energy);
                p1.FindPhaseInTime(p2, p3, delta, i);
            }
        }
        private void BuildChart2(Planet p1, Planet p2, Planet p3, double delta)
        {
            chart3.Series[p1.n].Points.Clear();
            double L1;
            double L2;
            double L3;
            for (double i = 0; i <= 15; i += 0.1)
            {
                L1 = p1.m * p1.v * p1.r;
                L2 = p2.m * p2.v * p2.r;
                L3 = p3.m * p3.v * p3.r;
                chart3.Series[p1.n].Points.AddXY(i, L1);
                chart3.Series[p2.n].Points.AddXY(i, L2);
                chart3.Series[p3.n].Points.AddXY(i, L3);
                p1.FindPhaseInTime(p2, p3, delta, i);
            }
        }
        private void BuildChart3(Planet p1, Planet p2, Planet p3, double delta)
        {
            chart4.Series[p1.n].Points.Clear();
            for (double i = 0; i <= 2*p3.T; i += 0.05)
                {
                chart4.Series[p1.n].Points.AddXY(i, p1.r);
                p1.FindPhaseInTime(p2, p3, delta, i);
                chart4.Series[p2.n].Points.AddXY(i, p2.r);
                chart4.Series[p3.n].Points.AddXY(i, p3.r);
                }
        }
    }
}
