using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DelmonPrize
{
    public class Sqlconnection
    {
        // public string ConnectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public string ConnectionString = "Data Source=192.168.1.8;Initial Catalog=DelmonPrize; MultipleActiveResultSets=true; User ID=sa;password=Ram72763@";

        SqlConnection con;
        public void OpenConection()
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
        }


        public void CloseConnection()
        {
            con = new SqlConnection(ConnectionString);
            con.Close();
        }
        public void ExecuteQueries(string Query_, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            foreach (SqlParameter parm in parameters)
            {
                cmd.Parameters.Add(parm);
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Parameters.Clear();
        }

        public SqlDataReader DataReader(string Query_, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            foreach (SqlParameter parm in parameters)
            {
                cmd.Parameters.Add(parm);
            }
            SqlDataReader dr = cmd.ExecuteReader();
            cmd.Dispose();
            cmd.Parameters.Clear();
            return dr;

        }


        public object ShowDataInGridViewORCombobox(string Query_)
        {
            SqlDataAdapter adapt = new SqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }

    }
}