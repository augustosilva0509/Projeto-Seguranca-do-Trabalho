using Sylvan.Data.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public static class ServerCalls
    {
        private static string serverDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\server\\";
        public static void CheckCode(string teamCode)
        {
            bool exists = false;
            string[] codes = System.IO.File.ReadLines(serverDirectory + "codes.txt").ToArray();
            foreach (string code in codes)
            {
                if (code == teamCode)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
                throw new ArgumentOutOfRangeException();
            
            if (System.IO.File.Exists(serverDirectory + "results.csv"))
            {
                DataTable dataTable = new DataTable();
                using var reader = CsvDataReader.Create(serverDirectory + "results.csv");
                while (reader.Read())
                {
                    if (reader.GetString("Código") == teamCode)
                    {
                        reader.Close();
                        throw new ArgumentException();
                    }
                        
                }
            }
        }
        public static void SendResult(Result bestResult, string teamCode = "0")
        {
            DataTable dataTable = new DataTable();
            if (!System.IO.File.Exists(serverDirectory + "results.csv"))
            {
                dataTable.Columns.Add("Código", typeof(string));
                dataTable.Columns.Add("Resultado", typeof(int));
                dataTable.Rows.Add(teamCode, bestResult.corrects);
                using var writer = CsvDataWriter.Create(serverDirectory + "results.csv");
                writer.Write(dataTable.CreateDataReader());
            }
            else
            {
                dataTable.Columns.Add("Código", typeof(string));
                dataTable.Columns.Add("Resultado", typeof(int));
                using var reader = CsvDataReader.Create(serverDirectory + "results.csv");
                while (reader.Read())
                {
                    dataTable.Rows.Add(reader.GetString("Código"), reader.GetInt16("Resultado"));
                }
                dataTable.Rows.Add(teamCode, bestResult.corrects);

                reader.Close();
                using var writer = CsvDataWriter.Create(serverDirectory + "results.csv");
                writer.Write(dataTable.CreateDataReader());
            }
        }
    }
}
