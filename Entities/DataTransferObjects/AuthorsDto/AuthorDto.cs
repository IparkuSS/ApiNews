using Entities.Models;
using System;
using System.Collections.Generic;
namespace Entities.DataTransferObjects.AuthorsDto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Document { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
