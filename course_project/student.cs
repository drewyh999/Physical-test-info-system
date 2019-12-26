using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace course_project
{
    public partial class student : MetroFramework.Forms.MetroForm
    {
        string id, name;
        public student(string id,string name)
        {
            this.id = id;
            this.name = name;
            InitializeComponent();
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pwd ch = new change_pwd(id, name);
            ch.Show();
        }

        private void 体检预约ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_reserve ns = new new_reserve(name);
            ns.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.Show();
        }

        private void student_Load(object sender, EventArgs e)
        {
            this.Text += name;
            try
            {
                tabControl1.TabPages.Clear();
                string sql = string.Format("select * from phy_test natural join spo_test where 学号 = '{0}'",id);
                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                MySqlDataReader dataReader;
                DBHelper.connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    stu_chart sa = new stu_chart();
                    string year =(string) dataReader["学年"];
                    string[] subjects = { "50米跑", "1000米跑", "坐位体前屈", "跳远", "引体向上", "仰卧起坐"};
                    foreach(string str in subjects)
                    {
                        string value = (string)dataReader[str];
                        if(value != "暂无成绩")
                        {
                            sa.AddPointToChart(str, Convert.ToInt32(value));
                        }
                    }
                    //string[] phy_subs = { "身高", "体重", "心率", "肺活量", "血压低压", "血压高压", "视力" };
                    
                    string height = (string)dataReader["身高"];
                    sa.SetHeight(height);
                    string weight = (string)dataReader["体重"];
                    sa.SetWeight(weight);
                    string heartbeat = (string)dataReader["心率"];
                    sa.SetHeart(heartbeat);
                    string vital = (string)dataReader["肺活量"];
                    sa.SetVital(vital);
                    string low = (string)dataReader["血压低压"];
                    sa.SetLow(low);
                    string high = (string)dataReader["血压高压"];
                    sa.SetHigh(high);
                    string eye = (string)dataReader["视力"];
                    sa.SetEye(eye);
                    TabPage tb = new TabPage();
                    tb.Text = year + "学年";
                    double height_value = Convert.ToDouble(height)/100;
                    double weight_value = Convert.ToDouble(weight);
                    double bmi = weight_value / (height_value * height_value);
                    sa.SetBMI(Convert.ToString(bmi).Substring(0,5));
                    sa.Dock = DockStyle.Fill;
                    sa.FormBorderStyle = FormBorderStyle.None;
                    sa.TopLevel = false;
                    tb.Controls.Add(sa);
                    tabControl1.TabPages.Add(tb);
                    sa.Show();

                }
                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作异常" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }
    }
}
