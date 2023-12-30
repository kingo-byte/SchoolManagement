namespace SchoolManagement.Models
{
    public class User
    {
        int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// To know if the user wants to signIn or signUp since flutter is restricted to 2 apis
        /// </summary>
        public string Action {  get; set; } = string.Empty; 

        public List<Course> Courses = new List<Course>(); 
    }
}
