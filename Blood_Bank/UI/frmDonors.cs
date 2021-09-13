using Blood_Bank.BLL;
using Blood_Bank.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_Bank.UI
{
    public partial class frmDonors : Form
    {
        public frmDonors()
        {
            InitializeComponent();
        }

        // Create Objec of Donor BLL and Donor DAL
        donorBLL d = new donorBLL();
        donorDAL dal = new donorDAL();
        userDAL udal = new userDAL();

        //Global Variable for Image
        string imageName = "no-image.jpg";

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //We will write the code to add new donor
            //Step 1: Get the data from Manage Donors Form
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            d.added_date = DateTime.Now;

            //Get the ID of Logged In User
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;  //TODO: Get the ID of Logged in User
            d.image_name = imageName;

            //Step 2: Inserting Data into Database
            //Create a Boolean variable to Check Whether the Data is Inserted Successfully or not
            bool isSuccess = dal.Insert(d);

            //Check whether the Data is Inserted Successfully or not
            if (isSuccess==true)
            {
                //Data or Donor Added Successfully
                MessageBox.Show("New Donor Added Successfully.");

                //Refresh DataGrid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

                //Clear all the Textbox
                Clear();
            }
            else
            {
                //Failed to Insert Data
                MessageBox.Show("Failed to Add New User");
            }

        }

        //Method or Function to Clear TextBoxes
        public void Clear()
        {
            //Clear All the Textbox
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtDonorID.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";

            //Clear the PictureBox
            //First we need to get the Image Path
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            string imagepath = path + "\\images\\no-image.jpg";

            //Display in Picture Box
            pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void frmDonors_Load(object sender, EventArgs e)
        {
            //Display Donors in Datagrid View
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //First we need to get the Image Path
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            string imagepath = path + "\\images\\no-image.jpg";

            //Display in Picture Box
            pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Select the data from datagrid View and Display in our Form

            //1. Find the row selected
            int RowIndex = e.RowIndex;

            txtDonorID.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();

            imageName = dgvDonors.Rows[RowIndex].Cells[9].Value.ToString();
            //Display the image of Selected Donor
            //Get the image path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);
            string imagePath = paths + "\\images\\" + imageName;
            //Display the Image of Selected User
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Add the funtionality to Update the Donors
            //Step 1: Get the Values from Form
            d.donor_id = int.Parse(txtDonorID.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            // Get the ID of Logged In User
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;
            d.image_name = imageName;

            // Create a Boolean Variable to Check Whether the Data is Updated Successfully or not
            bool isSuccess = dal.Update(d);

            //Let's Check Whether the Data is Updated or Not
            if (isSuccess == true)
            {
                //Donor Updated Successfully
                MessageBox.Show("Donor Updated Successfully.");
                Clear();

                //Refresh Data Grid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Donors.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the value from form
            d.donor_id = int.Parse(txtDonorID.Text);

            //Step Create the Boolean Value to Check Whether the donor Deleted or Not
            bool isSuccess = dal.Delete(d);

            if (isSuccess == true)
            {
                //Donor Deleted Successfully
                MessageBox.Show("Donor Deleted Successfully.");
                Clear();

                //Refresh DataGrid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Failed to Delete Donor
                MessageBox.Show("Failed to Delete Donor");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear the TextBox
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Code to Select Image and Upload 
            //1. Open Dialog Box to select Image
            OpenFileDialog open = new OpenFileDialog();

            //2. Filter the File Type, Only Allow Image File Type
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG; *.gif;";

            //3. Check whether the Image is Selected or Not
            if (open.ShowDialog() == DialogResult.OK)
            {
                //Check if the File exits or not
                if (open.CheckFileExists)
                {
                    //Display the Selected File in Picture Box
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //Rename the Image we Selected
                    //1. Get Extension of Image
                    string ext = Path.GetExtension(open.FileName);
                    string name = Path.GetFileNameWithoutExtension(open.FileName);

                    //Generate Random but Globally Unique Identifier
                    Guid g = new Guid();
                    g = Guid.NewGuid();

                    //Finally Rename our Image
                    imageName = "Blood_Bank_MS_" + name + g + ext;

                    //Get the Path of Selected Image
                    string sourcePath = open.FileName;

                    //Get the Path of Destination
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    //Paths to Destination Folder
                    string destinationPath = paths + "\\images\\" + imageName;

                    //6. Copy image to the Destination Folder
                    File.Copy(sourcePath, destinationPath);

                    //7. Display Message
                    MessageBox.Show("Image Successfully Uploaded.");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Let's Add the Functionality to Search the Donors

            //1. Get the Keywords Type on the Search TextBox
            String keywords = txtSearch.Text;

            //Check whether the Textbox is Empty or not
            if (keywords != null)
            {
                //Display the Information of Donors Based on Keywords
                DataTable dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Display All the Donors on Data Grid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }
    }
}
