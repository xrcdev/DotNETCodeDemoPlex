using System;

namespace WebApplication_AutoMapper.Model
{
    public class AuthorModel
    {
        
        public int Id
        {
            get; set;
        }
        public string FirstName
        {
            get; set;
        }
        public string LastName
        {
            get; set;
        }
        public string Address1
        {
            get; set;
        }
        public DateTime CreateTime { get; set; }
    }
}
