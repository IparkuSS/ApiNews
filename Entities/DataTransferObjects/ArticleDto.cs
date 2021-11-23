using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string ShortDescription { get; set; }
        public Guid IdSubsection { get; set; }
        public string Heading { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public DateTime AddTime { get; set; }
        public string Text { get; set; }
    }
}
