using System;
namespace News.BLL.DataTransferObjects.AuthorsDto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Document { get; set; }
    }
}
