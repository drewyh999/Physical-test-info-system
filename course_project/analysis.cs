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
using MySql.Data.MySqlClient;

namespace course_project
{
    public partial class analysis : MetroFramework.Forms.MetroForm
    {
        public analysis()
        {
            InitializeComponent();
        }

        private void analysis_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1;
            string sql2;
            if(comboBox1.Text == "体检")
            {
                sql1 = "show columns from phy_test";
                sql2 = "select distinct 学年 from phy_test";
            }
            else
            {
                sql1 = "show columns from spo_test";
                sql2 = "select distinct 学年 from spo_test";
            }
            try
            {
                comboBox2.Items.Clear();
                MySqlCommand command = new MySqlCommand(sql1, DBHelper.connection);
                MySqlDataReader dataReader;
                DBHelper.connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if ((string)dataReader["Field"] != "学号" && (string)dataReader["Field"] != "学年")
                    {
                        comboBox2.Items.Add((string)dataReader["Field"]);
                    }
                }
                DBHelper.connection.Close();
                DBHelper.connection.Open();
                comboBox3.Items.Clear();
                command = new MySqlCommand(sql2, DBHelper.connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    comboBox3.Items.Add((string)dataReader["学年"]);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("数据库操作异常" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label3.Visible = true;
                comboBox2.Visible = true;
                label4.Visible = false;
                comboBox3.Visible = false;
            }
            else
            {
                label3.Visible = false;
                comboBox2.Visible = false;
                label4.Visible = true;
                comboBox3.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string database;
            chart1.Series[0].Points.Clear();
           
            if(comboBox1.Text == "体测")
            {
                database = "spo_test";
            }
            else
            {
                database = "phy_test";
            }
            try
            {
                
                if (radioButton1.Checked)
                {
                    string subject = comboBox2.Text;
                    chart1.Series[0].ChartType = SeriesChartType.Line;
                    chart1.Series[0].LegendText = subject + "均值趋势";
                    foreach (var obj in comboBox3.Items)
                    {
                        string year = obj.ToString();
                        DBHelper.connection.Open();
                        string sizesql = string.Format("select count(*) from {0} where 学年 = '{1}'", database, year);
                        MySqlCommand sizecom = new MySqlCommand(sizesql, DBHelper.connection);
                        int size = Convert.ToInt32(sizecom.ExecuteScalar());
                        DBHelper.connection.Close();


                        DBHelper.connection.Open();
                       
                        string sql = string.Format("select {0} from {1} where 学年 = '{2}'", subject, database,year);
                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        MySqlDataReader datareader = command.ExecuteReader();
                        double sum = 0;
                        while (datareader.Read())
                        {
                            string text = (string)datareader[subject];
                            if (text != "暂无成绩")
                            {
                                double value = Convert.ToDouble(text);
                                sum += value;
                            }
                        }
                        chart1.Series[0].Points.AddXY(year, sum / size);
                        DBHelper.connection.Close();
                    }
                    chart1.Visible = true;
                }
                else
                {
                    string year = comboBox3.Text;
                    chart1.Series[0].ChartType = SeriesChartType.Column;
                    chart1.Series[0].LegendText = year + "学年均值";
                    string sizesql = string.Format("select count(*) from {0} where 学年 = '{1}'",database,year);
                    MySqlCommand sizecom = new MySqlCommand(sizesql, DBHelper.connection);
                    DBHelper.connection.Open();
                    int size = Convert.ToInt32(sizecom.ExecuteScalar());
                    DBHelper.connection.Close();
                    foreach(var obj in comboBox2.Items)
                    {
                        string subject = obj.ToString();
                        


                        DBHelper.connection.Open();

                        string sql = string.Format("select {0} from {1} where 学年 = '{2}'", subject, database, year);
                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        MySqlDataReader datareader = command.ExecuteReader();
                        double sum = 0;
                        while (datareader.Read())
                        {
                            string text = (string)datareader[subject];
                            if (text != "暂无成绩")
                            {
                                double value = Convert.ToDouble(text);
                                sum += value;
                            }
                        }
                        chart1.Series[0].Points.AddXY(subject, sum / size);
                        DBHelper.connection.Close();
                    }
                    chart1.Visible = true;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("数据库操作错误" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
               
            }
        }
    }
}
