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
    public partial class doctor : MetroFramework.Forms.MetroForm
    {
        string id;
        string name;
        public doctor(string id,string name)
        {
            this.id = id;

            this.name = name;
            InitializeComponent();
        }

        private void doctor_Load(object sender, EventArgs e)
        {
            this.Text += name + "医生";
            string sql = "select * from reserve where 是否完成 = '否'";
            MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
            MySqlDataReader dataReader;
            DBHelper.connection.Open();
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    string text = "学生姓名:" + (string)dataReader["学生姓名"] + ":预约时间:" + Convert.ToString(dataReader["预约时间"]).Substring(0,8);
                    checkedListBox1.Items.Add(text);
                }
                dataReader.Close();

            }
            DBHelper.connection.Close();

            this.filllist();
            
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pwd ch = new change_pwd(id, "doctor");
            ch.Show();
        }

        private void 手动录入新记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_record_doctor nrd = new new_record_doctor();
            nrd.Show();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        public void filllist()
        {
            string stu_num;
            string year;
            string height;
            string weight;
            string heartbeat;
            string vital;
            string low;
            string high;
            string eye;
            try
            {
                string sql;
                if (metroTextBox1.Text == "" || metroComboBox1.Text == "")
                {
                    sql = string.Format("select * from phy_test ");
                }
                else
                {
                    sql = string.Format("select * from phy_test where {0} like '%{1}%'", metroComboBox1.Text, metroTextBox1.Text);

                    

                }
                //MessageBox.Show(sql);
                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                MySqlDataReader dataReader;
                DBHelper.connection.Open();
                dataReader = command.ExecuteReader();
                
                listView1.Items.Clear();

                if (!dataReader.HasRows)
                {
                    MessageBox.Show("没有查到相关记录！");

                }
                else
                {
                    while (dataReader.Read())
                    {

                        stu_num = (string)dataReader["学号"];
                         year = (string)dataReader["学年"]; 
                         height = (string)dataReader["身高"]; 
                         weight = (string)dataReader["体重"]; 
                         heartbeat = (string)dataReader["心率"]; 
                         vital = (string)dataReader["肺活量"]; 
                         low = (string)dataReader["血压低压"]; 
                         high = (string)dataReader["血压高压"]; 
                         eye = (string)dataReader["视力"]; 
                        ListViewItem listviewstu = new ListViewItem(stu_num);
                        listviewstu.Tag = stu_num;

                        listviewstu.SubItems.AddRange(new string[] { year,height,weight,heartbeat,vital,low,high,eye});
                        listView1.Items.Add(listviewstu);

                    }
                }
                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误" + ex.Message);
            }
            finally
            {
                DBHelper.connection.Close();
            }
        }

        private void on_form_close(object sender, FormClosedEventArgs e)
        {
            
            foreach (object obj in checkedListBox1.CheckedItems)
            {
                int result = 0;
                string item = Convert.ToString(obj);
                string name = item.Split(':')[1];
                string date = item.Split(':')[3];
                string sql = string.Format("update reserve set 是否完成 = '是' where 学生姓名 = '{0}' and 预约时间 = '{1}'", name, date);
                try
                {
                   
                    MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                    DBHelper.connection.Open();
                    result = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库操作错误" + ex.Message);
                }
                finally
                {
                    DBHelper.connection.Close();
                }
                if (result < 1)
                {
                    MessageBox.Show("修改失败");
                }
                
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 身高ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("身高");
        }
        private void alter(string key)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选中记录");

            }
            doc_context_new_value d = new doc_context_new_value((string)listView1.SelectedItems[0].Tag, key,  this);
            d.Show();
        }

        private void 体重ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("体重");
        }

        private void 肺活量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("肺活量");
        }

        private void 血压低压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("血压低压");
        }

        private void 血压高压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("血压高压");
        }

        private void 视力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alter("视力");
        }

        private void 从excel文件导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelInput.AddFromExcel("doctor");
            filllist();
        }

        private void 删除本条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选中记录");

            }
            else
            {
                DialogResult choice = MessageBox.Show("确定要删除？", "警告", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    int result = 0;
                    try
                    {
                        string sql = string.Format("delete from phy_test where 学号 ='{0}' and 学年 = '{1}'", (string)listView1.SelectedItems[0].Tag, listView1.SelectedItems[0].SubItems[1].Text);
                        //MessageBox.Show(sql);
                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        DBHelper.connection.Open();
                        result = command.ExecuteNonQuery();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("数据库操作异常" + ex.Message);
                    }
                    finally
                    {
                        DBHelper.connection.Close();
                    }
                    if (result < 1)
                    {
                        MessageBox.Show("删除失败");
                    }
                    else
                    {
                        MessageBox.Show("删除成功");
                        filllist();
                    }
                }
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.Show();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            filllist();
        }
    }
}
