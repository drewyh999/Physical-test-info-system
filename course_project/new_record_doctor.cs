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
    public partial class new_record_doctor : MetroFramework.Forms.MetroForm
    {
        public new_record_doctor()
        {
            InitializeComponent();
        }

        private void new_record_doctor_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void validate_stu_num(object sender, EventArgs e)
        {
            string sql = string.Format("select * from pwd_stu where stu_num = '{0}'", textBox1.Text);
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
            try
            {
                string sql = string.Format("insert into phy_test(学号,学年,身高,体重,心率,肺活量,血压低压,血压高压,视力)", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox10.Text);

                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
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
    }
}
