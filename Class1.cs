using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Planet

    {
        public double x, y, vx, vy, m, T, vMax, vMin, xMinVel, yMinVel, xMaxVel, yMaxVel, energy;
        public int n;
        public Planet(double x, double y, double vx, double vy, double m, int n)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.m = m;
            this.n = n;
        }
         public double r => Math.Sqrt(this.x * this.x + this.y * this.y);
        public double v => Math.Sqrt(this.vx * this.vx + this.vy * this.vy);
        public void FindPhase(Planet planet2, Planet planet3, double delta)
        {
            double GM = 4* Math.PI * Math.PI;
            double r21 = Math.Sqrt((this.x - planet2.x) * (this.x - planet2.x) + (this.y - planet2.y) * (this.y - planet2.y));
            double r31 = Math.Sqrt((this.x - planet3.x) * (this.x - planet3.x) + (this.y - planet3.y) * (this.y - planet3.y));
            if (this.n == 0)
            {
                this.vx = this.vx + (-1 / (r * r * r) * this.x + planet2.m / Math.Pow(r21, 3) * (planet2.x - this.x) + planet3.m / Math.Pow(r31, 3) * (planet3.x - this.x)) * GM * delta;
                this.vy = this.vy + (-1 / (r * r * r) * this.y + planet2.m / Math.Pow(r21, 3) * (planet2.y - this.y) + planet3.m / Math.Pow(r31, 3) * (planet3.y - this.y)) * GM * delta;
            }
            else if (this.n==1)
            {
                this.vx = this.vx + (-1 / (r * r * r) * this.x - planet2.m / Math.Pow(r21, 3) * (planet2.x - this.x) + planet3.m / Math.Pow(r31, 3) * (planet3.x - this.x)) * GM * delta;
                this.vy = this.vy + (-1 / (r * r * r) * this.y - planet2.m / Math.Pow(r21, 3) * (planet2.y - this.y) + planet3.m / Math.Pow(r31, 3) * (planet3.y - this.y)) * GM * delta; 
            }
            else if (this.n == 2)
            {
                this.vx = this.vx + (-1 / (r * r * r) * this.x - planet2.m / Math.Pow(r21, 3) * (planet2.x - this.x) - planet3.m / Math.Pow(r31, 3) * (planet3.x - this.x)) * GM * delta;
                this.vy = this.vy + (-1 / (r * r * r) * this.y - planet2.m / Math.Pow(r21, 3) * (planet2.y - this.y) - planet3.m / Math.Pow(r31, 3) * (planet3.y - this.y)) * GM * delta;
            }
            this.x = this.x + this.vx * delta;
            this.y = this.y + this.vy * delta;
        }
        public void FindPhaseInTime(Planet planet2, Planet planet3, double delta, double time)
        {
            double finalTime = time;
            while (time <= finalTime + 0.05)
            {
                this.FindPhase(planet2, planet3, delta);
                planet2.FindPhase(this, planet3, delta);
                planet3.FindPhase(this, planet2, delta);
                time += delta;
            }
        }
        public void FindPeriod(Planet planet2, Planet planet3, double delta)
        {
            this.T = 0;
            if (this.n == 2)
               delta = (1e-4);
            else if (this.n == 1)
                delta = 1.8*(1e-5);
            var startX = this.x;
            var startY = this.y;
            for (int i=0; i<100000; i++)
            {
                this.FindPhase(planet2, planet3, delta);// Отойдём немного от начальной точки
                planet2.FindPhase(this, planet3, delta);
                planet3.FindPhase(this, planet2, delta);
                this.T += delta;
            }
            while (((Math.Abs(startX - this.x)>1e-6) && (Math.Abs(startY - this.y) > 1e-4)))
            {
                this.FindPhase(planet2, planet3, delta);
                planet2.FindPhase(this, planet3, delta);
                planet3.FindPhase(this, planet2, delta);
                this.T += delta;
            }
        }
        public void FindMinMaxVel(Planet planet2, Planet planet3, double delta)
        {
             this.FindPeriod(planet2, planet3, delta);
            planet2.FindPhase(this, planet3, delta);
            planet3.FindPhase(this, planet2, delta);
            double time=0;
             this.vMax = Int32.MinValue;
             this.vMin = Int32.MaxValue;
            while (time < this.T)
            {
                this.FindPhase(planet2, planet3, delta);
                planet2.FindPhase(this, planet3, delta);
                planet3.FindPhase(this, planet2, delta);
                if (v >= vMax)
                {
                    this.vMax = v;
                    this.xMaxVel = this.x;
                    this.yMaxVel = this.y;
                }
                if (v <= this.vMin)
                {
                    this.vMin = v;
                    this.xMinVel = this.x;
                    this.yMinVel = this.y;
                }
                time += delta;
            }
        }
        public void FindEnergy(Planet planet2, Planet planet3, double delta)
        {
            double GM = 4 * Math.PI * Math.PI;
            double r21 = Math.Sqrt((this.x - planet2.x) * (this.x - planet2.x) + (this.y - planet2.y) * (this.y - planet2.y));
            double r31 = Math.Sqrt((this.x - planet3.x) * (this.x - planet3.x) + (this.y - planet3.y) * (this.y - planet3.y));
            this.energy =  this.v * this.v /2 - GM  /this.r - GM *  planet2.m /r21 - GM * planet3.m /r31;
        }
    }
}
