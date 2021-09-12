using Blood_Bank.BLL;
using Blood_Bank.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_Bank.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Create the Object of BLL and DAL
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Write the Code to Close the Application
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //White the code to Login our Application
            //Step 1: Get the username and password from Manage Login Form
            l.username = txtUsername.Text;
            l.password = txtPassword.Text;
            
            //Creck the Login Credentials 
            bool isSuccess = dal.loginCheck(l);

            //Check whether the login is Success or not
            if (isSuccess == true)
            {
                //Login Success
                //Display Success Message
                MessageBox.Show("Login Successful.");

                //Display home form
                frmHome home = new frmHome();
                home.Show();
                this.Hide(); //To Close Login Form
            }
            else
            {
                //Lohin Failed
                //Display the Error Message 
                MessageBox.Show("Login Failed. Try Again");
            }

        }
    }
}
