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
    public partial class change_pwd : MetroFramework.Forms.MetroForm
    {
        string id;
        string type;
        public change_pwd(string id,string type)
        {
            this.id = id;
            this.type = type;
            InitializeComponent();
        }

        private void change_pwd_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(metroTextBox2.Text != metroTextBox3.Text)
            {
                MessageBox.Show("两次输入的新密码不相同，请重新输入");
                return;
            }
            if(metroTextBox1.Text == "" || metroTextBox2.Text == "" || metroTextBox3.Text == "")
            {
                MessageBox.Show("输入不能为空");
            }
            string sql;
            if(type == "stu")
            {
                sql = string.Format("update pwd_stu set password = {0} where stu_num = '{1}' and password = '{2}'", metroTextBox3.Text, id, metroTextBox1.Text);

            }
            else
            {
                sql = string.Format("update pwd_others set password = {0} where id_num = '{1}' and password = '{2}'", metroTextBox3.Text, id, metroTextBox1.Text);

            }
            try
            {
               
                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                DBHelper.connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("更改成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("更改失败，原密码错误");
                }
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
