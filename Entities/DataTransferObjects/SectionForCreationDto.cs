using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class SectionForCreationDto
    {
        public Guid Id { get; set; }

        public string NameSection { get; set; }
        public string TitleImagePath { get; set; }
    }
}
