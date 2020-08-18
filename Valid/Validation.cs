using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Windows.Forms;

namespace Final_Project.Valid
{
    public class Validation
    {
        public static bool IsValidID(string input)
        {
            int tempID;
            if ((input.Length != 7) || (Int32.TryParse(input, out tempID)))
            {
                MessageBox.Show("invalid student ID");
                return false;
            }

            return true;
        }

        public static bool IsValidID(TextBox text)
        {
            int tempID;
            if ((text.TextLength != 7) || !(Int32.TryParse(text.Text, out tempID)))
            {
                MessageBox.Show("invalid student ID");
                text.Clear();
                text.Focus();
                return false;
            }

            return true;
        }

        public static bool IsValidName(TextBox text)
        {
            for (int i = 0; i < text.TextLength; i++)
            {
                if (char.IsDigit(text.Text, i) || (char.IsWhiteSpace(text.Text, i)))

                {
                    MessageBox.Show("invalid Name, Please Enter again", "invalid name");
                    text.Clear();
                    text.Focus();
                    return false;
                }
            }

            return true;
        }

        public static bool IsUniqueID(List<Student> lists, int id)

        {
            foreach (Student s in lists)
            {
                if (s.StudentId == id)
                {
                    MessageBox.Show("PLease enter unique id ");
                    return false;
                }
            }
            return true;
        }
    }
}
