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
    public partial class pwd_recover : MetroFramework.Forms.MetroForm
    {
        public pwd_recover()
        {
            InitializeComponent();
        }

        private void pwd_recover_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("输入不完整,请补全信息");
                return;
            }
            else
            {
                DialogResult choice = MessageBox.Show("确定要重置密码为身份证后5位？", "警告", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    try
                    {
                        if (comboBox1.Text == "学生账号")
                        {
                            string sql = string.Format("update pwd_stu set password = '{0}' where id_num = '{1}'", textBox1.Text.Substring(textBox1.Text.Length - 5), textBox1.Text);
                            DBHelper.connection.Open();
                            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("重置成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("账号不存在");
                                
                            }
                        }
                        else
                        {
                            string sql = string.Format("update pwd_others set password = '{0}' where id_num = '{1}'", textBox1.Text.Substring(textBox1.Text.Length - 5), textBox1.Text);
                            DBHelper.connection.Open();
                            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("重置成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("账号不存在");
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("数据库操作错误" + ex.Message);
                    }
                    finally
                    {
                        DBHelper.connection.Clone();
                    }
                }
            }
        }
    }
}
