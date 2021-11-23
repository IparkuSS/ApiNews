using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews.Models
{
    public class AuthorData
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Document { get; set; }




    }
}
