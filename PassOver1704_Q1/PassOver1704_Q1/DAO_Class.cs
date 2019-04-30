using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1704_Q1
{
    class DAO_Class : IDAO_Class
    {
        static SqlCommand cmd = new SqlCommand();

        static DAO_Class()
        {
            //Command and Data Reader            
            cmd.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=PassOverDB;Integrated Security=True");
            cmd.Connection.Open();
        }
        public static void Close()
        {
            cmd.Connection.Close();
        }

        public void AddANumberToX(int x)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO TableX (X) Values ({x})";

            cmd.ExecuteNonQuery();

        }

        public void AddANumberToY(int y)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO TableY (Y) Values ({y})";

            cmd.ExecuteNonQuery();

        }

        public void UpdateTheResultsTable()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TableX.X X, Operation.Operation, TableY.Y Y " +
                                "FROM TableX " +
                                "CROSS JOIN Operation " +
                                "CROSS JOIN TableY "+
                                "WHERE X>0 AND Y>0";
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    var resultTable = new
                    {
                        X = (int)reader["X"],
                        Operation = (string)reader["Operation"],
                        Y = (int)reader["Y"]
                    };
                    insetIntoResults(resultTable);
                }
            }
        }

        public void insetIntoResults(object resultTable)
        {
            dynamic UserObject = resultTable;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=PassOverDB;Integrated Security=True");
            cmd2.Connection.Open();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = $"INSERT INTO ResultAll (X, OPERATION, Y) Values ({UserObject.X}, '{UserObject.Operation}', {UserObject.Y})";

            cmd2.ExecuteNonQuery();
            cmd2.Connection.Close();
        }
        
        public void UpdateRawIntoResults(double result, int ResultID)
        {
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=PassOverDB;Integrated Security=True");
            cmd2.Connection.Open();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = $"UPDATE ResultAll SET Result = {result} WHERE ID = {ResultID}";

            cmd2.ExecuteNonQuery();
            cmd2.Connection.Close();
        }

        public void UpdateTheResults()
        {
            double CalcResult = 0D;
            int ResultID = 1;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=PassOverDB;Integrated Security=True");
            cmd2.Connection.Open();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = $"SELECT * FROM ResultAll";
            SqlDataReader reader = cmd2.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                var resultTable = new
                {
                    X = reader["X"],
                    Operation = reader["Operation"],
                    Y = reader["Y"]
                };

                CalcResult = CalcTheResult(reader["X"], reader["Operation"], reader["Y"]);
                UpdateRawIntoResults(CalcResult, ResultID++);
            }
        }

        public double CalcTheResult(object X, object Operation, object Y)
        {
            
            switch (Operation)
            {
                case "/":
                    {
                        int x = (Convert.ToInt32(X));
                        int y = (Convert.ToInt32(Y));
                        if (y > 0)
                            return (x / y);
                        return -1;
                    }
                case "*": return ((Convert.ToInt32(X)) * (Convert.ToInt32(Y)));
                case "+": return ((Convert.ToInt32(X)) + (Convert.ToInt32(Y)));
                case "-": return ((Convert.ToInt32(X)) - (Convert.ToInt32(Y)));
                default: return (Convert.ToInt32(X)) + (Convert.ToInt32(Y));
            }
            
            //return -1;
        }

        public void printTheResults()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM ResultAll";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                Console.WriteLine($"ID {reader["ID"],5}   X {reader["X"],5}   Op {reader["Operation"],5}   Y {reader["Y"],5}   Res {reader["Result"],5}");

                //Console.ReadLine();
            }
        }

    }
}
