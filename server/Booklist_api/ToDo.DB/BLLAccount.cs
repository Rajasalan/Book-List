using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DB
{
    public class BLLAccount : Generic
    {
        
        public List<usp_fetch_books> GetBookList(RequestInput requestInput)
        {         

            SqlConnection con = new SqlConnection(toDoConnection);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_fetch_books", con);
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader iDR = cmd.ExecuteReader();
                List<usp_fetch_books> OutputData = new List<usp_fetch_books>();
                OutputData = DataReaderMapToList<usp_fetch_books>(iDR);
                return OutputData;
            }
            catch (Exception ex)
            {              
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public void InsertBooks(RequestInput requestInput, string mode, int book_id, string title, string author, string description, string description2)
        {           
            SqlConnection con = new SqlConnection(toDoConnection);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_manage_books", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mode", mode));
                cmd.Parameters.Add(new SqlParameter("@book_id", book_id));
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@author", author));
                cmd.Parameters.Add(new SqlParameter("@description", description));
                cmd.Parameters.Add(new SqlParameter("@description2", description2));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


    }


   
    public class usp_fetch_books
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }
    }

   
}
