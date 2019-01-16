using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary1
{
    public class DataBase
    {
        //private string strConnection1 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.139", "root", "test", "test");
        //private string strConnection2 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.154", "root", "1234", "gudi");

        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "\\public\\DBInfo.json";
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                foreach (JProperty col in jo.Properties())
                {
                    Console.WriteLine("{0} : {1}", col.Name, col.Value);
                    map.Add(col.Name, col.Value);
                }

                string strConnection1 = string.Format("server={0};user={1};password={2};database={3};",map["server"], map["user"], map["password"], map["database"]);
                conn.ConnectionString = strConnection1;
                conn.Open();

                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
