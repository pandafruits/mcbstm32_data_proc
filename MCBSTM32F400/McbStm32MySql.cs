using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MCBSTM32F400
{
    static public class McbStm32MySql
    {
        static private string connStr = "Server=localhost;UserId=root;Password=root;Database=mcbstm32f400";

        static public void InsertEnvironmentalData(McbStm32Environment environ)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string query = @"INSERT INTO environment(temperature, humidity, core_temperature, device_time, acquisition_time) 
                                 VALUES(@temperature, @humidity, @core_temperature, @device_time, @acquisition_time)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@temperature", environ.Temperature);
                    cmd.Parameters.AddWithValue("@humidity", environ.Humidity);
                    cmd.Parameters.AddWithValue("@core_temperature", environ.CoreTemperature);
                    cmd.Parameters.AddWithValue("@device_time", environ.DeviceTime);
                    cmd.Parameters.AddWithValue("@acquisition_time", environ.AcquisitionTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static public List<string[]> ExecuteQuery(string query)
        {
            List<string[]> rows = new List<string[]>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cols = new string[reader.FieldCount];

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cols[i] = Convert.ToString(reader.GetValue(i));
                            }

                            rows.Add(cols);
                        }
                    }
                }
                return rows;
            }
        }

        static public int RowCount(string query)
        {
            return ExecuteQuery(query).Count;
        }
    }

}
