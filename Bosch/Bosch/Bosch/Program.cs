using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Bosch
{
    class Program
    {
        class Car
        {
            public double height;
            public double focalLengthX;
            public double focalLengthY;
            public double principalPointX;
            public double principalPointY;
            public Car(double height, double focalLengthX, double focalLengthY, double principalPointX, double principalPointY)
            {
                this.height = height;
                this.focalLengthX = focalLengthX;
                this.focalLengthY = focalLengthY;
                this.principalPointX = principalPointX;
                this.principalPointY = principalPointY;
            }
        }

        class Vechicle
        {
            public double width;
            public Vechicle(double width)
            {
                this.width = width;
            }
        }

        static List<Tuple<int, int>> clickPoints = new List<Tuple<int, int>>();
        static Car car;
        static Vechicle bus = new Vechicle(2.4);

        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetFileNameWithoutExtension(ofd.FileName).StartsWith("CAR1"))
                {
                    car = new Car(1.209, 1373.67, 1386.62, 642.062, 361.836);
                }
                else if (Path.GetFileNameWithoutExtension(ofd.FileName).StartsWith("CAR2"))
                {
                    car = new Car(1.209, 1385.29, 1398.29, 634.743, 360.52);
                }

                var img = Cv2.ImRead(ofd.FileName, ImreadModes.Unchanged);
                img.ConvertTo(img, MatType.CV_8U, 255.0 / 4096.0);
                var window = new Window("Pic");
                window.OnMouseCallback += click;
                do {
                    if (clickPoints.Count >= 2)
                    {
                        img.Rectangle(new Rect(clickPoints[0].Item1, clickPoints[0].Item2, Math.Abs(clickPoints[1].Item1 - clickPoints[0].Item1), Math.Abs(clickPoints[1].Item2 - clickPoints[0].Item2)),
                            Scalar.Yellow);
                    }
                    window.ShowImage(img);
                    Cv2.WaitKey();
                    if (clickPoints.Count >= 2)
                    {
                        var proportion = Math.Abs(clickPoints[1].Item1 - clickPoints[0].Item1) / car.focalLengthY;

                    }
                } while (true);
            }
        }

        static void click(MouseEvent @event, int x, int y, MouseEvent flags)
        {
            if (@event.Equals(MouseEvent.LButtonDown))
            {
                clickPoints.Clear();
                clickPoints.Add(new Tuple<int, int>(x, y));
            }
            else if (@event.Equals(MouseEvent.LButtonUp))
            {
                clickPoints.Add(new Tuple<int, int>(x, y));
            }
        }
    }
}
