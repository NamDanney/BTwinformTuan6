using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MajorService
    {
        public List<Major> GetAllByFaculty( int FacultyId)
        {
            Model1 context = new Model1();
            return context.Major.Where(x => x.FacultyID == FacultyId).ToList();
        }

    }
}
