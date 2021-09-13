using Blood_Bank.DAL;
using Blood_Bank.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_Bank
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        //Create the Object of Donors DAL
        donorDAL dal = new donorDAL();

        private void frmhome_Load(object sender, EventArgs e)
        {
            //Load all the Blood Donors Count When Form is Loaded
            //Call allDonorCountMethod
            allDonorCount();

            //Display all the Donors
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //Display the Username of Logged In User
            lblUser.Text = frmLogin.loggedInUser;
        }

        public void allDonorCount()
        {
            //Get the Donor Count from Database and set in Respective Label
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Code to Close the Application
            this.Hide();
        }

        private void dgvDhoners_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Users Form
            frmUsers users = new frmUsers();
            users.Show();
        }

        private void donorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Manage Donors Form
            frmDonors donors = new frmDonors();
            donors.Show();
        }

        private void frmhome_Activated(object sender, EventArgs e)
        {
            //Call allDonorCount Method
            allDonorCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywords from the Search TextBox
            string keywords = txtSearch.Text;

            //Check Whether the TextBox is Empty or Not
            if(keywords != null)
            {
                //Filter the Donors based on Keywords
                DataTable dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Display all the Donors
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }
    }
}
