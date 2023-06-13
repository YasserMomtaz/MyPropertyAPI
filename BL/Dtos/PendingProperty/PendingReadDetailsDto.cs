using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.PendingProperty
{
    public class PendingReadDetailsDto
    {
        public int Id { get; internal set; }
        public string Title { get; internal set; }
        public string? Username { get; internal set; }
    }
}
