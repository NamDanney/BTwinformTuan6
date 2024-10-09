using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            Model1 context = new Model1();
            return context.Student.ToList();
        }

        public List<Student> GetAllHasNoMajor()
        {
            Model1 context = new Model1();
            return context.Student.Where(x => x.MajorID == null).ToList();

        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            Model1 context = new Model1();
            return context.Student.Where(x => x.MajorID == null && x.FacultyID == facultyID).ToList();
        }

        public bool IsStudent(string studentID)
        {
            Model1 context = new Model1();
            return context.Student.Any(x => x.StudentID == studentID);
        }

        public void AddStudent(Student student)
        {
            using(Model1 context = new Model1())
            {
                if(IsStudent(student.StudentID))
                {
                    throw new Exception("Mã sinh viên đã tồn tại");
                }
                context.Student.Add(student);
                context.SaveChanges();
            }
        }

        public void DeleteStudent(string studentID)
        {
            using (Model1 context = new Model1())
            {
                var student = context.Student.SingleOrDefault(x => x.StudentID == studentID);
                if (student == null)
                {
                    throw new Exception("Sinh viên không tồn tại | Vui lòng nhập mã sinh viên để xóa");
                }
                else
                {
                    context.Student.Remove(student);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using(Model1 context = new Model1())
            {
                context.Student.AddOrUpdate(student);
                context.SaveChanges();
            }
        }
    }
}
