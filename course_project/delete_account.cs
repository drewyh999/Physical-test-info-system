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
    public partial class delete_account : MetroFramework.Forms.MetroForm
    {
        public delete_account()
        {
            InitializeComponent();
        }

        private void delete_account_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("输入不完整,请补全信息");
                return;
            }
            else
            {
                DialogResult choice = MessageBox.Show("确定要删除？账号信息会全部丢失", "警告", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    try
                    {
                        if (comboBox1.Text == "学生账号")
                        {
                            string sql = string.Format("delete from pwd_stu where id_num = '{0}'", textBox1.Text);
                            DBHelper.connection.Open();
                            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("删除成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("账号不存在");

                            }
                        }
                        else
                        {
                            string sql = string.Format("delete from pwd_others where id_num = '{0}'", textBox1.Text);
                            DBHelper.connection.Open();
                            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("删除成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("账号不存在");
                                
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("数据库操作失败" + ex.Message);
                    }
                    finally
                    {
                        DBHelper.connection.Close();
                    }
                }
            }
        }
    }
}
