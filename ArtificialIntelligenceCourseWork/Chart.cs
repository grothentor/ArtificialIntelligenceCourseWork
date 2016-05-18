using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligenceCourseWork
{
    [Serializable]
    public class Chart
    {
        public double startPoint { set; get; }
        public double secondPoint { set; get; }
        public double thirdPoint { set; get; }
        public double endPoint { set; get; }
        public Chart()
        {
            startPoint = 0;
            secondPoint = 0;
            thirdPoint = 0;
            endPoint = 0;
        }
        public Chart(params double[] points)
        {
            switch (points.Length)
            {
                case 1: this.startPoint = this.secondPoint = this.thirdPoint = this.endPoint = points[0];
                    break;
                case 2:
                    {
                        this.startPoint = this.secondPoint = points[0];
                        this.thirdPoint = this.endPoint = points[1];
                    }
                    break;
                case 3:
                    {
                        this.startPoint = points[0];
                        this.secondPoint = this.thirdPoint = points[1];
                        this.endPoint = points[2];
                    }
                    break;
                case 4:
                    {
                        this.startPoint = points[0];
                        this.secondPoint = points[1];
                        this.thirdPoint = points[2];
                        this.endPoint = points[3];
                    }
                    break;
                default:
                    break;
            }
        }
        public double find(double point)
        {
            if (point < startPoint || point > endPoint) return 0;
            else if (point <= thirdPoint && point >= secondPoint) return 1;
            else if (point < secondPoint) return (point - startPoint) / (secondPoint - startPoint);
            else return (endPoint - point) / (endPoint - thirdPoint);
        }
    }
}
