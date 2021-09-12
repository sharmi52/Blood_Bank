using Blood_Bank.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_Bank.DAL
{
    class loginDAL
    {
        //Create a Static String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
            //Create a Boolean Variable and Set its Default Value to False
            bool isSuccess = false;

            //Object for SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql Query to Check Login Based on Username and Password
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password = @password";

                //Create Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value Through Parameters
                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);

                //Sql Data adapter to get the data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Datatable to Hold the Data from Database Temporarily
                DataTable dt = new DataTable();

                //Fill the Data from Adapter to dt
                adapter.Fill(dt);
        
                //Check whether user exits or not
                if (dt.Rows.Count > 0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //Display Error Message if There is any Execution Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }
            return isSuccess;
        }
    }
}
