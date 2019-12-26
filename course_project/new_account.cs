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
    public partial class new_account : MetroFramework.Forms.MetroForm
    {
        public new_account()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox3.Visible = true;
                label4.Visible = true;
                comboBox1.Visible = true;
                label5.Visible = true;
            }
            else
            {
                textBox3.Visible = false;
                label4.Visible = false;
                comboBox1.Visible = false;
                label5.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id_num = textBox1.Text;
            string name = textBox2.Text;
            if (id_num == null || name == null)
            {
                MessageBox.Show("用户名及身份证号不能为空");
                return;
            }
            if(id_num.Length < 5)
            {
                MessageBox.Show("身份证号长度不足");
                return;
            }
            string password = id_num.Substring(id_num.Length - 5);
            try
            {
                if (radioButton1.Checked)
                {
                    string stu_num = textBox3.Text;
                    string sql = string.Format("insert into pwd_stu (stu_num,id_num,name,password,gender) values ('{0}','{1}','{2}','{3}','{4}')", stu_num, id_num,name,password,comboBox1.Text);
                    DBHelper.connection.Open();
                    Console.WriteLine(sql);
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    int count = command.ExecuteNonQuery();
                    Console.WriteLine("the execution number is " + count);
                    if (count == 1)
                    {
                        MessageBox.Show("插入成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("插入失败");
                    }
                }
                else if (radioButton2.Checked)
                {
                    string sql = string.Format("insert into pwd_others(id_num, name, password, type) values('{0}', '{1}', '{2}', '{3}')", id_num, name, password, "manage");

                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    int count = command.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("插入成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("插入失败");
                    }
                }
                else if (radioButton3.Checked)
                {
                    string sql = string.Format("insert into pwd_others(id_num, name, password, type) values('{0}', '{1}', '{2}', '{3}')", id_num, name, password, "input");
                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    int count = command.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("插入成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("插入失败");
                    }
                }
                else if (radioButton4.Checked)
                {
                    string sql = string.Format("insert into pwd_others(id_num, name, password, type) values('{0}', '{1}', '{2}', '{3}')", id_num,name,password,"doctor");
                    DBHelper.connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    int count = command.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("插入成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("插入失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入失败" + ex.Message);
            }
            finally
            {

                DBHelper.connection.Close();
            }
        }

        private void new_account_Load(object sender, EventArgs e)
        {

        }
    }
}
