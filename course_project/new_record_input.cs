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
    public partial class new_record_input : MetroFramework.Forms.MetroForm
    {
        public new_record_input()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = string.Format("select * from pwd_stu where stu_num = '{0}'",textBox1.Text);
            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
            MySqlDataReader dataReader;
            try
            {
                DBHelper.connection.Open();
                dataReader = command.ExecuteReader();
                MessageBox.Show(sql);


                if (!dataReader.HasRows)
                {
                    MessageBox.Show("学号不存在");

                }
                else
                {
                    while (dataReader.Read())
                    {
                        string gender = (string)dataReader["gender"];
                        if(gender == "男")
                        {
                            label7.Visible = true;
                            label8.Visible = false;
                            textBox8.Visible = false;
                            textBox7.Visible = true;

                        }
                        else
                        {
                            
                            label7.Visible = false;
                            label8.Visible = true;
                            textBox8.Visible = true;
                            textBox7.Visible = false;
                        }

                    }
                }
                dataReader.Close();
            }
            catch
            {
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
           
            if(textBox7.Text != "")
            {
                 sql = string.Format("insert into spo_test(学号,学年,50米跑,1000米跑,坐位体前屈,跳远,引体向上,仰卧起坐) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text,"暂无成绩");
            }
            else
            {
                sql = string.Format("insert into spo_test(学号,学年,50米跑,1000米跑,坐位体前屈,跳远,引体向上,仰卧起坐) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, "暂无成绩", textBox8.Text);
            }
            try
            {

                MySqlCommand command = new MySqlCommand(sql,DBHelper.connection);
                DBHelper.connection.Open();

                int result = command.ExecuteNonQuery();

                if (result != 1)
                {
                    MessageBox.Show("添加失败！");
                }
                else
                {
                    MessageBox.Show("添加成功！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("操作数据库出错");

            }
            finally
            {
                DBHelper.connection.Close();
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void new_record_input_Load(object sender, EventArgs e)
        {

        }
    }
}
