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
    public partial class login : MetroFramework.Forms.MetroForm
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;
            if(id == null ||password == null)
            {
                MessageBox.Show("用户名及密码不能为空");
                return;
            }
            try
            {
                if (radioButton1.Checked)//如果选择登录类型为学生
                {
                    string sql = string.Format("select * from pwd_stu where stu_num = '{0}' and password = '{1}' ", id, password);
                    DBHelper.connection.Open();
                    Console.WriteLine(sql);
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    MySqlDataReader count = command.ExecuteReader();
                    Console.WriteLine("the execution number is " + count);
                    if (count.HasRows)
                    {
                        count.Read();
                        string name = (string)count["name"];
                        DBHelper.connection.Close();
                        student stu = new student(textBox1.Text,name);
                        stu.Show();
                        count.Close();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("登录失败：用户名不存在或者密码不正确");
                    }
                }
                else if(radioButton2.Checked)
                {
                    string sql = string.Format("select * from pwd_others where id_num = '{0}' and password = '{1}' and type = 'manage'", id, password);
                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    MySqlDataReader count = command.ExecuteReader();
                    if (count.HasRows)
                    {
                        count.Read();
                        string name = (string)count["name"];
                        DBHelper.connection.Close();
                        manage man = new manage(textBox1.Text,name);
                        man.Show();
                        count.Close();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("登录失败：用户名不存在或者密码不正确");
                    }
                }
                else if (radioButton3.Checked)
                {
                    string sql = string.Format("select * from pwd_others where id_num = '{0}' and password = '{1}' and type = 'input'", id, password);
                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    MySqlDataReader count = command.ExecuteReader();
                    if (count.HasRows)
                    {
                        count.Read();
                        string name = (string)count["name"];
                        DBHelper.connection.Close();
                        input ipt = new input(textBox1.Text,name);
                        ipt.Show();
                        count.Close();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("登录失败：用户名不存在或者密码不正确");
                    }
                }
                else if (radioButton4.Checked)
                {
                    string sql = string.Format("select * from pwd_others where id_num = '{0}' and password = '{1}' and type = 'doctor'", id, password);
                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    MySqlDataReader count = command.ExecuteReader();
                    if (count.HasRows)
                    {
                        count.Read();
                        string name = (string)count["name"];
                        DBHelper.connection.Close();
                        doctor doc = new doctor(textBox1.Text,name);
                        doc.Show();
                        count.Close();
                        this.Visible = false;
                        
                    }
                    else
                    {
                        MessageBox.Show("登录失败：用户名不存在或者密码不正确");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //input i = new input("asdasd","asdawd");
            //i.Show();
            //doctor doc = new doctor("asdwd", "asdwa");
            //doc.Show();
            //manage man = new manage("adasa", "asdawd");
            //man.Show();
            //about a = new about();
            //a.Show();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
