using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.Apartment
{
    public class ApartmentListPaginationDto
    {
        public IEnumerable<ApartmentList> ApartmentList { get; set; }=new List<ApartmentList>();
        public int ApartmentCount { get; set; }

    }
}
