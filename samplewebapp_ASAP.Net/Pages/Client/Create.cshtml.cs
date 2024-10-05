using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;
using samplewebapp_ASAP.Net.Model;

namespace samplewebapp_ASAP.Net.Pages.Client
{
    public class CreateModel : PageModel
    {
        public User user = new User();
        public string errorMessage = String.Empty;
        public string successMessage = String.Empty;
        public List<string> States { get; set; }

        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            States = new List<string>
            {
                "Alabama", "Alaska", "Arizona", "Arkansas", "California", // Add all states here
                // Add the rest of the states...
                "Wyoming"
            };
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];
            user.Designation = Request.Form["Designation"];
            user.DateOfJoin = DateTime.Parse(Request.Form["DateOfJoin"]);
            user.Salary = decimal.Parse(Request.Form["Salary"]);
            user.Gender = Request.Form["gender"]; // Get gender
            user.State = Request.Form["state"];

            if (user.FirstName.Length == 0 || user.LastName.Length == 0 || user.Designation.Length == 0 || string.IsNullOrWhiteSpace(user.Gender) ||
                string.IsNullOrWhiteSpace(user.State))
                //|| !DateTime.TryParse(Request.Form["DateOfJoin"], out DateOfJoin) || !decimal.TryParse(Request.Form["Salary"], out Salary) || Salary <= 0)
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.AddUser(user, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            user.FirstName = ""; user.LastName = ""; user.Designation = ""; user.Gender = ""; user.State = "";
            //user.DateOfJoin = ""; user.Salary = "";
            successMessage = "New User added.";
            Response.Redirect("/Client/Index");
        }
    }
}
