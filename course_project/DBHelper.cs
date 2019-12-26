using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace course_project
{
    class DBHelper
    {
        private static string connString = "server=localhost;User Id=root;password=ccx777777;Database=course_project";
        public static MySqlConnection connection = new MySqlConnection(connString);
    }
}
