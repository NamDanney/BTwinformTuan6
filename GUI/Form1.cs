using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {

        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorService majorService = new MajorService();
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var ListStudent = studentService.GetAll();
                var listFaculty = facultyService.GetAll();
                FillMajor(listFaculty.FirstOrDefault()?.FacultyID);
                FillFacluty(listFaculty);
                BindGrid(ListStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["Col_MSSV"].Value = item.StudentID;
                dataGridView1.Rows[index].Cells["Col_HoTen"].Value = item.FullName;
                if (item.Faculty != null)
                    dataGridView1.Rows[index].Cells["Col_Khoa"].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells["Col_DTB"].Value = item.AverageScore + "";
                if (item.Major != null)
                    dataGridView1.Rows[index].Cells["Col_Major"].Value = item.Major.Name + "";
            }
        }

        private void FillFacluty(List<Faculty> listFaculty)
        {
            this.comboBox1.DataSource = listFaculty;
            this.comboBox1.DisplayMember = "FacultyName";
            this.comboBox1.ValueMember = "FacultyID";
        }

        private void FillMajor(int? facultyID)
        {
            if (facultyID.HasValue)
            {
                var listMajor = majorService.GetAllByFaculty(facultyID.Value);
                if (listMajor.Any())
                {
                    comboBox2.DataSource = listMajor;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "MajorID";
                }

                else
                {
                    comboBox2.DataSource = null;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is int selectedFacultyID)
            {
                FillMajor(selectedFacultyID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin sinh viên từ các điều khiển trên form
                var student = new Student
                {
                    StudentID = textBox1.Text,
                    FullName = textBox2.Text,
                    AverageScore = double.TryParse(textBox3.Text, out double score) ? (double?)score : null,
                    FacultyID = comboBox1.SelectedValue as int?,
                    MajorID = comboBox2.SelectedValue as int?
                };

                // Thêm sinh viên vào cơ sở dữ liệu
                studentService.AddStudent(student);

                var ListStudent = studentService.GetAll();
                BindGrid(ListStudent);

                MessageBox.Show("Thêm sinh viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetInnermostExceptionMessage(ex));
            }
        }

        private string GetInnermostExceptionMessage(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex.Message;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                string studentID = textBox1.Text;
                studentService.DeleteStudent(studentID);
                var ListStudent = studentService.GetAll();
                BindGrid(ListStudent);

                MessageBox.Show("Xóa sinh viên thành công!");
            }catch (Exception ex)
            {
                MessageBox.Show(GetInnermostExceptionMessage(ex));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var student = new Student
                {
                    StudentID = textBox1.Text,
                    FullName = textBox2.Text,
                    AverageScore = double.TryParse(textBox3.Text, out double score) ? (double?)score : null,
                    FacultyID = comboBox1.SelectedValue as int?,
                    MajorID = comboBox2.SelectedValue as int?
                };

                // Cập nhật sinh viên trong cơ sở dữ liệu
                studentService.UpdateStudent(student);

                var ListStudent = studentService.GetAll();
                BindGrid(ListStudent);

                MessageBox.Show("Cập nhật sinh viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetInnermostExceptionMessage(ex));
            }
        }
    }
}
