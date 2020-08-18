using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Windows.Forms;
using System.IO;

namespace Final_Project.DAL
{
    public  static class StudentDAL
    {
        private static string filePath = Application.StartupPath + @"\Student.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save(Student std)

        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(std.StudentId + "," + std.FirstName + "," + std.LastName + "," + std.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Student Data has been Saved at file Student.dat");

        }


        public static void ListStudent(ListView listViewStudent)

        {
           
            StreamReader sReader = new StreamReader(filePath);
            listViewStudent.Items.Clear();

            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');

                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                listViewStudent.Items.Add(item);
                line = sReader.ReadLine();
            }

            sReader.Close();

        }
        public static List<Student> ListStudents() 
        {
            List<Student> lists = new List<Student>();


            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                Student std = new Student();
                std.StudentId = Convert.ToInt32(fields[0]);
                std.FirstName = fields[1];
                std.LastName = fields[2];
                std.PhoneNumber = fields[3];
                lists.Add(std);
                line = sReader.ReadLine();

            }
            return lists;

        }

        public static Student Search(int stdId)

        {
            Student std = new Student();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');

                if (stdId == Convert.ToInt32(fields[0]))
                {
                    std.StudentId = Convert.ToInt32(fields[0]);
                    std.FirstName = fields[1];
                    std.LastName = fields[2];
                    std.PhoneNumber = fields[3];
                    sReader.Close();
                    return std;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static void Delete(int stdId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((stdId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }
                line = sReader.ReadLine();

            }
            sReader.Close();
            sWriter.Close();

            
            File.Delete(filePath);
            File.Move(fileTemp, filePath);

        }
    }
}
