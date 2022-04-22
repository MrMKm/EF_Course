using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class CourseDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int Capacity { get; set; }
    }
}
