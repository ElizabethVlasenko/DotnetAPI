namespace DotnetAPI.Models
{
    //partial to allow adding something to the class from inside another file
    public partial class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }


        public UserJobInfo()
        {
            if (JobTitle == null)
            {
                JobTitle = "";
            }
            if (Department == null)
            {
                Department = "";
            }
        }
    }
}
