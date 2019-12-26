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
    public partial class doc_context_new_value : MetroFramework.Forms.MetroForm
    {
        string selected_tag, key;
         doctor doc;
        public doc_context_new_value(string selected_tag,string key, doctor doc)
        {
            this.selected_tag = selected_tag;
            this.key = key;
            this.doc = doc;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(metroTextBox1.Text == "")
            {
                MessageBox.Show("输入不能为空");
            }
            string sql = string.Format("update phy_test set {0} = '{1}' where 学号 = {2}", key, metroTextBox1.Text, selected_tag);
            int result = 0;
            try
            {
                MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                DBHelper.connection.Open();
                result = command.ExecuteNonQuery();

            }
            catch
            {
            }
            finally
            {
                DBHelper.connection.Close();
            }
            if (result < 1)
            {
                MessageBox.Show("修改失败");
            }
            else
            {
                MessageBox.Show("修改成功");
            
                doc.filllist();
                this.Close();
            }
        }

        private void doc_context_new_value_Load(object sender, EventArgs e)
        {

        }
    }
}
