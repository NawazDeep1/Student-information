using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project.BLL;
using Final_Project.DAL;
using Final_Project.Valid;
using System.IO;


namespace Final_Project.GUI
{
    public partial class Student_Information : Form
    {
        List<Student> lists = new List<Student>();

        private void ClearAll()
        {
            textBoxStudentID.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxStudentID.Focus();
        }

        public Student_Information()
        {
            InitializeComponent();
        }

        private void textBoxStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxChoiceSearchby.SelectedIndex;
            switch (choice)
            {
                case -1:
                    MessageBox.Show("Please select search optn");
                    break;
                case 0:
                    Student std = StudentDAL.Search(Convert.ToInt32(textBoxInputInfo.Text));
                    if (std != null)
                    {
                        textBoxStudentID.Text = (std.StudentId).ToString();
                        textBoxFirstName.Text = std.FirstName;
                        textBoxLastName.Text = std.LastName;
                        maskedTextBoxPhoneNumber.Text = std.PhoneNumber;
                    }
                    else
                    {
                        MessageBox.Show("Student not found");
                        textBoxInputInfo.Clear();
                        textBoxInputInfo.Focus();
                    }
                    break;



                default:
                    break;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure about that ", "confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if ((Validation.IsValidID(textBoxStudentID)) && (Validation.IsValidName(textBoxFirstName)) && (Validation.IsValidName(textBoxLastName)) && (Validation.IsUniqueID(lists, Convert.ToInt32(textBoxStudentID.Text))))
            {
                Student aStudent = new Student();
                aStudent.StudentId = Convert.ToInt32(textBoxStudentID.Text);
                aStudent.FirstName = textBoxFirstName.Text;
                aStudent.LastName = textBoxLastName.Text;
                aStudent.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                StudentDAL.Save(aStudent);

                ClearAll();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            StudentDAL.Delete(Convert.ToInt32(textBoxStudentID.Text));
            MessageBox.Show("Student record has been deleted", "Confirmation");

        }

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            Student aStudent = new Student();
            if ((Validation.IsValidID(textBoxStudentID)) && (Validation.IsValidName(textBoxFirstName)) && (Validation.IsValidName(textBoxLastName)) && (Validation.IsUniqueID(lists, Convert.ToInt32(textBoxStudentID.Text))))
            {
                aStudent.StudentId = Convert.ToInt32(textBoxStudentID.Text);
                aStudent.FirstName = textBoxFirstName.Text;
                aStudent.LastName = textBoxLastName.Text;
                aStudent.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                lists.Add(aStudent);
                buttonListStudent.Enabled = true;

                ClearAll();

            }
        }

        private void buttonListStudent_Click(object sender, EventArgs e)
        {
            listViewStudent.Items.Clear();
            StudentDAL.ListStudent(listViewStudent);
        }

        
    }
}
