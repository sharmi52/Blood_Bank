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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        //Create Objects of UserBLL and userDAL
        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        string imageName = "no-image.jpg";

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add functionality to close this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Step 1: Get the Values fromUI
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date = DateTime.Now;
            u.image_name = imageName;

            //Step 2: Adding the Values from UI to the Database
            //Create a Boolean variable to Check Whether the Data is Inserted Successfully or not
            bool success = dal.Insert(u);

            //Step 3:Check whether the Data is Inserted Successfully or not
            if(success==true)
            {
                //Data or User Added Successfully
                MessageBox.Show("New User Added Successfully.");

                //Display the User in DataGrid View
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear TextBoxes
                Clear();


            }
            else
            {
                //Failed to Add User
                MessageBox.Show("Failed to Add New User");
            }
        }

        //Method or Function to Clear TextBoxes
        public void Clear()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtPassword.Text = "";
            txtUserID.Text = "";
            //Path to Destination Folder
            //Get the Image Path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            string imagePath = paths + "\\images\\no-image.jpg";
            //Display in Picture Box
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Find the Row Index of the Row Clicked of Users Data View
            int RowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUserName.Text = dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text = dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text = dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();
            imageName = dgvUsers.Rows[RowIndex].Cells[8].Value.ToString();

            //Display the Image of Selected User
            //Get the Image Path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            if(imageName != "no-image.jpg")
            {
                //Path to Destination Folder
                string imagePath = paths + "\\images\\" + imageName;
                //Display in Picture Box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
            else
            {
                //Path to Destination Folder
                string imagePath = paths + "\\images\\no-image.jpg";
                //Display in Picture Box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            //Display the Users in Datagrid View When the Form is Loaded
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Step 1: Get the Values from UI
            u.user_id = int.Parse(txtUserID.Text);
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date = DateTime.Now;
            u.image_name = imageName;

            //Step 2: Create a Boolean Variable to Check Whether the Data is Updated Successfully or not
            bool success = dal.Update(u);

            //Let's Check Whether the Data is Updated or Not
            if (success == true)
            {
                //Date Updated Successfully
                MessageBox.Show("User Information Updated Successfully.");

                //Refresh Data Grid View
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear the TextBoxes
                Clear();
             }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Step 1: Get the UserID from Text Box to Delete the User
            u.user_id = int.Parse(txtUserID.Text);

            //Step Create the Boolean Value to Check Whether the User Deleted or Not
            bool success = dal.Delete(u);

            //Let's Check Whether the User is Deleted or Not
            if(success==true)
            {
                //User Deleted Successfully
                MessageBox.Show("User Deleted Successfully.");

                //Refresh DataGrid View
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear the TextBoxes
                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear the User Function
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Write the Code to Upload the Image of User
            //Open Dialog Box t select Image
            OpenFileDialog open = new OpenFileDialog();

            //Filter the File Type, Only Allow Image File Type
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG; *.gif;";

            //Check if the File is Selected or Not
            if(open.ShowDialog()==DialogResult.OK)
            {
                //Check if the File exits or not
                if(open.CheckFileExists)
                {
                    //Display the Selected File on Picture Box
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //Rename the Image we Selected
                    //1. Get Extension of Image
                    string ext = Path.GetExtension(open.FileName);

                    //2. Generate Random Integer
                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                    //3. Rename  the Image
                    imageName = "Blood-Bank_MS" + RandInt + ext;

                    //4. Get the Path of Selected Image
                    string sourcePath = open.FileName;

                    //5. Get the Path of Destination
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
            //Write the Code to Get the Users Based on Keywords
            //1. Get the Keywords from the Textbox
            String keywords = txtSearch.Text;

            //Check whether the Textbox is Empty or not
            if (keywords!=null)
            {
                //Textbox is not Empty, Display Users on Data Grid View Based on the Keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //Textbox is Empty and Display All the Users on Data Grid View
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
