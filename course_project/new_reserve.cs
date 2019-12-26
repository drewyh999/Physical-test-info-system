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
    public partial class new_reserve : MetroFramework.Forms.MetroForm
    {
        string name;
        public new_reserve(string name)
        {
            this.name = name;
            InitializeComponent();
        }

        private void new_reserve_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            string sql = string.Format("insert into reserve(学生姓名,预约时间,是否完成) values ('{0}','{1}','否')", name, date);
            try
            {
                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                DBHelper.connection.Open();
                
                int result = command.ExecuteNonQuery();

                if (result != 1)
                {
                    MessageBox.Show("预约失败！");
                }
                else
                {
                    MessageBox.Show("预约成功！");
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("操作数据库失败" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }
    }
}
