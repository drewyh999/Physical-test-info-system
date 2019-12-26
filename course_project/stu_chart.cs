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

namespace course_project
{
    public partial class stu_chart : Form
    {
        public stu_chart()
        {
            InitializeComponent();
        }

        private void stu_chart_Load(object sender, EventArgs e)
        {
            Series series = chart1.Series[0];

            

            series.ChartType = SeriesChartType.Radar;

            // 线宽2个像素

            series.BorderWidth = 1;

            // 线的颜色：红色

            //series.Color = System.Drawing.Color.DarkBlue;

            // 图示上的文字

            series.LegendText = "体测雷达图";

           
            // 准备数据 

            


            // 设置显示范围

            ChartArea chartArea = chart1.ChartAreas[0];

            chartArea.AxisX.Minimum = 0;

            chartArea.AxisX.Maximum = 10;

            chartArea.AxisY.Minimum = 0d;

            chartArea.AxisY.Maximum = 100d;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public void AddPointToChart(string subject,int grade)
        {
            this.chart1.Series[0].Points.AddXY(subject, grade);
        }
        public void SetHeight(string value)
        {
            label3.Text += value;
        }
        public void SetWeight(string value)
        {
            label4.Text += value;
        }
        public void SetHeart(string value)
        {
            label5.Text += value;
        }
        public void SetVital(string value)
        {
            label6.Text += value;
        }
        public void SetLow(string value)
        {
            label7.Text += value;
        }
        public void SetHigh(string value)
        {
            label8.Text += value;
        }
        public void SetEye(string value)
        {
            label9.Text += value;
        }
        public void SetBMI(string value)
        {
            label10.Text += value;
        }
    }
}
