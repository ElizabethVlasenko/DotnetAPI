namespace DotnetAPI.Models
{
    //partial to allow adding something to the class from inside another file
    public partial class UserSalary
    {
        public int UserId { get; set; }
        public decimal Salary { get; set; }
    }
}
