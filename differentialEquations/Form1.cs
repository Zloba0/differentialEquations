using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace differentialEquations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double a = 0;
        double b = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            double h = 0.1;
            try
            {
                h = Convert.ToDouble(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Input step", "Worning");
                return;
            }
            double x0 = a;
            //try
            //{
            //    x0 = Convert.ToDouble(textBox3.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("Input x0", "Worning");
            //    return;
            //}
            double y0 = 0;
            try
            {
                y0 = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Input y0", "Worning");
                return;
            }
            double yLast;
            double yNext = y0;
            double x = a;
            func2 f;
            func fI;
            func3 c;
            if (radioButton1.Checked)
            {
                f = f1Maded;
                fI = f1;
                c = f1Maded;
            }
            else if (radioButton2.Checked)
            {
                f = f2Maded;
                fI = f2;
                c = f2Maded;
            }
            else
            {
                MessageBox.Show("Check function", "Worning");
                return;
            }
            for (double xi = a; xi <= b; xi+= 0.0001)
            {
                list.Add(xi, f(xi,y0 - c(x0)));
            }
            list2.Add(x0, y0);
            do
            {
                if (x + h > b)
                {
                    h = b - x;
                }
                yLast = yNext;
                yNext = yLast + h * fI(x, yLast);
                x += h;
                list2.Add(x, yNext);
            } while(x < b);
            LineItem myCurve = pane.AddCurve(" ", list, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve(" ", list2, Color.Red, SymbolType.None);

            zedGraph.AxisChange();

            zedGraph.Invalidate();
        }
        public delegate double func (double x, double y);
        public delegate double func2 (double x, double c);
        public delegate double func3(double x);
        public double f1(double x, double y)
        {
            return x*y;
        }
        public double f1Maded(double x)
        {
            return Math.Exp(x*x/2);
        }
        public double f1Maded(double x, double c)
        {
            return Math.Exp(x*x/2) + c;
        }
        public double f2(double x, double y)
        {
            return 2*x;
        }
        public double f2Maded(double x)
        {
            return x*x;
        }
        public double f2Maded(double x, double c)
        {
            return x*x + c;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0,2";
            textBox3.Text = "0";
            textBox3.ReadOnly = true;
            textBox4.Text = "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            double h = 0.1;
            try
            {
                h = Convert.ToDouble(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Input step", "Worning");
                return;
            }
            double x0 = a;
            //try
            //{
            //    x0 = Convert.ToDouble(textBox3.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("Input x0", "Worning");
            //    return;
            //}
            double y0 = 0;
            try
            {
                y0 = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Input y0", "Worning");
                return;
            }
            double yLast;
            double yNext = y0;
            double x = x0;
            func2 f;
            func fI;
            func3 c;
            if (radioButton1.Checked)
            {
                f = f1Maded;
                fI = f1;
                c = f1Maded;
            }
            else if(radioButton2.Checked)
            {
                f = f2Maded;
                fI = f2;
                c = f2Maded;
            }
            else
            {
                MessageBox.Show("Check function", "Worning");
                return;
            }
            for (double xi = a; xi <= b; xi+= 0.0001)
            {
                list.Add(xi, f(xi, y0 - c(x0)));
            }
            list2.Add(x0, y0);
            double r1;
            double r2;
            do
            {
                if (x + h > b)
                {
                    h = b - x;
                }
                yLast = yNext;
                r1 = h * fI(x, yLast);
                r2 = h * fI(x + h, yNext + r1);
                yNext = yLast + (r1 + r2)/2;
                x += h;
                list2.Add(x, yNext);
            } while (x < b);
            LineItem myCurve = pane.AddCurve(" ", list, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve(" ", list2, Color.Red, SymbolType.None);

            zedGraph.AxisChange();

            zedGraph.Invalidate();
        }

        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            radioButton2.Checked = false;
        }
        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            radioButton1.Checked = false;
        }
    }
}
