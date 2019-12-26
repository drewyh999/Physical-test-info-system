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
    public partial class input : MetroFramework.Forms.MetroForm
    {
        private DataSet dataset = new DataSet();//声明并初始化DataSet
        private MySqlDataAdapter dataadapter;//声明DataAdapter
        string id, name;
        public input(string id,string name)
        {
            this.id = id;
            this.name = name;
            //MessageBox.Show(this.Text);
            InitializeComponent();
        }

        private void input_Load(object sender, EventArgs e)
        {
            this.Text += this.name;
            string sql = "select * from spo_test";
            

            dataadapter = new MySqlDataAdapter(sql, DBHelper.connection);
            //填充数据集
            dataadapter.Fill(dataset, "spo_test");

            dataGridView1.DataSource = dataset.Tables["spo_test"];

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
            
        }
   

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "" || metroTextBox1.Text == "")
            {
                MessageBox.Show("关键字与搜索字段不能为空");
                return;
            }
            dataset.Tables["spo_test"].Clear();
            string sql = string.Format("select * from spo_test where {0} like '%{1}%'", metroComboBox1.Text, metroTextBox1.Text);

            dataadapter = new MySqlDataAdapter(sql, DBHelper.connection);
            //填充数据集
            dataadapter.Fill(dataset, "spo_test");
            dataGridView1.DataSource = dataset.Tables["spo_test"];
        }

        private void 手动录入新记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_record_input nri = new new_record_input();
            nri.Show();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            (this.dataGridView1.DataSource as DataTable).Rows.Clear();
            string sql = "select * from spo_test";


            dataadapter = new MySqlDataAdapter(sql, DBHelper.connection);
            //填充数据集
            dataadapter.Fill(dataset, "spo_test");

            dataGridView1.DataSource = dataset.Tables["spo_test"];

        }

        private void 删除当前记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridView1Row in dataGridView1.SelectedRows)
            {
                int result = 0;
                DialogResult choice = MessageBox.Show("确定要删除？", "警告", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    try
                    {
                        string sql = string.Format("delete from spo_test where 学号 ='{0}' and 学年 = '{1}'", dataGridView1Row.Cells[0].Value.ToString(), dataGridView1Row.Cells[1].Value.ToString());
                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        DBHelper.connection.Open();
                        result = command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
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

                    }


                    dataGridView1.Rows.Remove(dataGridView1Row);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定保存？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //生成用于修改的command命令
                MySqlCommandBuilder builder = new MySqlCommandBuilder(dataadapter);

                dataadapter.Update(dataset, "spo_test");
            }
        }

        private void 从excel文件导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelInput.AddFromExcel("input");
            (this.dataGridView1.DataSource as DataTable).Rows.Clear();
            string sql = "select * from spo_test";


            dataadapter = new MySqlDataAdapter(sql, DBHelper.connection);
            //填充数据集
            dataadapter.Fill(dataset, "spo_test");

            dataGridView1.DataSource = dataset.Tables["spo_test"];
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.Show();
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pwd ch = new change_pwd(id, "input");
            ch.Show();
        }
    }
}
