namespace WebApplication_AutoMapper.Model
{
    public class AuthorDTO
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
        public string Address
        {
            get; set;
        }
        public string CreateTime { get; internal set; }
    }
}
