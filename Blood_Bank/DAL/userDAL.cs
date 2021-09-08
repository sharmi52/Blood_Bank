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
    class userDAL
    {
        //Create a Static String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data from database
        public DataTable Select()
        {
            // Create an Object to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Create a DataTable to Hold the Data from Database
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query to Get Data from Database
                string sql = "Select * FROM tbl_users";

                //Create SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Sql Data Adapterto Hold the Data from Database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Transfer Data from SqlData Adapter to DataTable
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                // Display Error Message if there's any Exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Insert Data into Database for User Module
        public bool Insert(userBLL u)
        {
            //Create a boolean variable and set  its default value to false
            bool isSuccess = false;

            //Create an object of SqlConnection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a string Variable to Store the INSERT Query
                string sql = "INSERT INTO tbl_users(username, email, password, full_name, contact, address, added_date, image_name) VALUES (@username, @email, @password, @full_name, @contact, @address, @added_date, @image_name)";

                //Create a SQL Command to pass the value in our query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create the perameter to pass get the value from UI and pass it on SQL Query above
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);

                //Open Database Connection 
                conn.Open();

                //Create an Integer Variable to Hold the Value After the Query is Executed
                int rows = cmd.ExecuteNonQuery();

                //The Value of Rows will be Greater than 0 if the Query is Executed Successfully
                //Else it will be 0

                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Executed Successfully
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                //Display Error Message if there's any Exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection 
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region UPDATE Data in Database (User Module)
        public bool Update(userBLL u)
        {
            //Create a Bollean Variable and Set its Default Value to False
            bool isSuccess = false;

            //Create an Object for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a String Variable to Hold the Sql Query
                string sql = "UPDATE tbl_users SET username=@username, email=@email, password=@password, full_name=@full_name, contact=@contact, address=@address, added_date=@added_date, image_name=@image_name WHERE user_id=@user_id";

                //Create Sql Command to Execute Query and Also Pass the Values to Sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Now Pass the Values to Sql Query
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open Database Connection
                conn.Open();

                // Create an Integer Variable to Hold the Value After the Query is Executed
                int rows = cmd.ExecuteNonQuery();
                
                //If the Query is Executed Successfully then the Value of Rows will be Greater than 0
                //Else It will be 0

                if(rows>0)
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
            catch(Exception ex)
            {
                //Display Error Messsage if there's any Exceptional Error
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Delete Data From Database (User Module)
        public bool Delete(userBLL u)
        {
            //Create a Boolean Variable and Set its Default Value to False
            bool isSuccess = false;

            //Create an Object for SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a String Variable to Hold the Sql Query to Delete Data
                string sql = "DELETE FROM tbl_users WHERE user_id=@user_id";

                //Create Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value Through Parameters
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open the Database COnnection
                conn.Open();

                //Create an Integer Variable to Hold the Value After Query is Executed
                int rows = cmd.ExecuteNonQuery();

                //If the Query is Executed Successfully then the Value of Rows will be Greater than 0
                //Else It will be 0

                if (rows > 0)
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
            catch(Exception ex)
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

        #endregion

        #region SEARCH
        public DataTable Search(string keywords)
        {
            //1. Create an SQL Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //2. Create Data Table to Hold the data from Database Temporarily
            DataTable dt = new DataTable();

            //Write the Code to Search the Users
            try
            {
                //Write thr SQL Query to Search the User from Database
                string sql = "SELECT * FROM tbl_users WHERE user_id LIKE '%" + keywords + "%' OR full_name LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'";

                //Create SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create SQL Data Adapter to Get the Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();
                //Pass the Data from Adapter to Datatable
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                //Display Error Messages If there's any Exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the Database  Connection
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
