using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;

namespace course_project
{
    class ExcelInput
    {
        private static DataSet getData()
        {
            
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return null;

            
            var path = file.FileName;
            string fileSuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileSuffix))
                return null;
            using (DataSet ds = new DataSet())
            {
                //判断Excel文件是2003版本还是2007版本
                string connString = "";
                if (fileSuffix == ".xls")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                else
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

                //读取文件
                
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //获取Excel的第一个Sheet名称
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                string sql_select = "select * from [" + sheetName + "]";
                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {
                   
                    cmd.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }
        public static void AddFromExcel(string mode)
        {
            var dataset = getData();
            if(dataset == null)
            {
                return;
            }
            try
            {
                var table = dataset.Tables[0];
    
                foreach(DataRow row in table.Rows)
                {
                    DBHelper.connection.Open();
               
                    if(mode == "input")
                    {
                        string num = row["学号"].ToString();
                        string year = row["学年"].ToString();
                        string fif = row["50米跑"].ToString();
                        string thous = row["1000米跑"].ToString();
                        string str = row["坐位体前屈"].ToString();
                        string jump = row["跳远"].ToString();
                        string pull = row["引体向上"].ToString();
                        string seat = row["仰卧起坐"].ToString();
                        string sql = string.Format("insert into spo_test(学号,学年,50米跑,1000米跑,坐位体前屈,跳远,引体向上,仰卧起坐) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", num, year, fif, thous, str, jump, pull, seat);
                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        if(command.ExecuteNonQuery() < 1)
                        {
                            MessageBox.Show("导入失败");
                            return;
                        }
                    }
                    else
                    {
                        string num = row["学号"].ToString();
                        string year = row["学年"].ToString();
                        string height = row["身高"].ToString();
                        string weight = row["体重"].ToString();
                        string heart = row["心率"].ToString();
                        string vit = row["肺活量"].ToString();
                        string low = row["血压低压"].ToString();
                        string high = row["血压高压"].ToString();
                        string eye = row["视力"].ToString();
                        string sql = string.Format("insert into phy_test(学号,学年,身高,体重,心率,肺活量,血压低压,血压高压,视力)", num,year,height,weight,heart,vit,low,high,eye);

                        MySqlCommand command = new MySqlCommand(sql, DBHelper.connection);
                        if (command.ExecuteNonQuery() < 1)
                        {
                            MessageBox.Show("导入失败");
                            return;
                        }
                    }
                    
                    DBHelper.connection.Close();
                }
                MessageBox.Show("导入成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show("操作数据库失败" + ex.Message);
            }
        }


    }
}
