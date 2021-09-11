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
    class donorDAL
    {
        //Create a Connection String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region SELECT to Display Data in Datagridview from Database
        public DataTable Select()
        {
           // Create Object to DataTable to Hold the data from database and return it
           DataTable dt = new DataTable();

        //Create object of SQL Connection to Connect Database
        SqlConnection conn = new SqlConnection(myconnstrng);

        try
        {
          //Write SQL Query to Select the Data from Database
          string sql = "SELECT * FROM tbl_donors";

         //Create the SqlCommand to Execute the Query
         SqlCommand cmd = new SqlCommand(sql, conn); 

        //Create the Sql Data Adapter to Hold the data Temporaly
         SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //Open Database Connection
        conn.Open();

        //Pass the Data from Adapter to DataTable
        adapter.Fill(dt);
       }
        catch(Exception ex)
        {
            //Display Message if There's any Exceptional Errors
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
    #region INSERT data to database
    public bool Insert(donorBLL d)
{
    //Create a boolean variable and set  its default value to false
    bool isSuccess = false;

    //Create SqlConnection to connect database
    SqlConnection conn = new SqlConnection(myconnstrng);

    try
    {
        //Write the Query to INSERT data into database
        string sql = "INSERT INTO tbl_donors(first_name, last_name, email, contact, gender, address, blood_group, added_date, image_name, added_by) VALUES (@first_name, @last_name, @email, @contact, @gender, @address, @blood_group, @added_date, @image_name, @added_by)";

        //Create a SQL Command to pass the value in our query
        SqlCommand cmd = new SqlCommand(sql, conn);

        //Pass the value to SQL Query
        cmd.Parameters.AddWithValue("@first_name", d.first_name);
        cmd.Parameters.AddWithValue("@last_name", d.last_name);
        cmd.Parameters.AddWithValue("@email", d.email);
        cmd.Parameters.AddWithValue("@contact", d.contact);
        cmd.Parameters.AddWithValue("@gender", d.gender);
        cmd.Parameters.AddWithValue("@address", d.address);
        cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
        cmd.Parameters.AddWithValue("@added_date", d.added_date);
        cmd.Parameters.AddWithValue("@image_name", d.image_name);
        cmd.Parameters.AddWithValue("@added_by", d.added_by);

        //Open Database Connection 
        conn.Open();

        //Create an Integer Variable to Hold the Value After the Query is Executed
        int rows = cmd.ExecuteNonQuery();

        //The Value of Rows will be Greater than 0 if the Query is Executed Successfully
        //Else it will be 0
        if (rows > 0)
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
    catch (Exception ex)
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
    #region UPDATE donors in Database
    public bool Update(donorBLL d)
        {
            //Create a Bollean Variable and Set its Default Value to False
            bool isSuccess = false;

            //Create an Object for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a SQL Query to Update Donors  
                string sql = "UPDATE tbl_donors SET first_name=@first_name, last_name=@last_name, email=@email, contact=@contact, gender=@gender, address=@address, blood_group=@blood_group,  image_name=@image_name, added_by=@added_by WHERE donor_id=@donor_id";

                //Create Sql Command here
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value to Sql Query
                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);

                //Open Database Connection
                conn.Open();

                // Create an Integer Variable to Hold the Value After the Query is Executed
                int rows = cmd.ExecuteNonQuery();
               //If the Query is Executed Successfully then the Value of Rows will be Greater than 0 Else It will be 0
                if (rows>0)
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
    #region DELETE donors from Database
            public bool Delete(donorBLL d)
        {
            //Create a Boolean Variable and Set its Default Value to False
            bool isSuccess = false;

            //Create an Object for SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a String Variable to Hold the Sql Query to Delete Data
                string sql = "DELETE FROM tbl_donors WHERE donor_id=@donor_id";

                //Create Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value Through Parameters
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);

                //Open the Database COnnection
                conn.Open();

                //Create an Integer Variable to Hold the Value After Query is Executed
                int rows = cmd.ExecuteNonQuery();

                //If the Query is Executed Successfully then the Value of Rows will be Greater than 0 Else It will be 0
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

    }

}
