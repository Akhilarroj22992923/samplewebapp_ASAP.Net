using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;
using samplewebapp_ASAP.Net.Model;

namespace samplewebapp_ASAP.Net.Pages.Client
{
    public class EditModel : PageModel
    {
        public User user = new User();
        public string errorMessage = String.Empty;
        public string successMessage = String.Empty;
        public List<string> States { get; set; }
        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            States = new List<string>
            {
                "Alabama", "Alaska", "Arizona", "Arkansas", "California", // Add all states here
                "Wyoming"
            };
        }
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                DAL dal = new DAL();
                user = dal.GetUser(id, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            user.Id = Request.Form["id"];
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];
            user.Designation = Request.Form["Designation"];
            user.DateOfJoin = DateTime.Parse(Request.Form["DateOfJoin"]);
            user.Salary = decimal.Parse(Request.Form["Salary"]);
            user.Gender = Request.Form["gender"]; // Capture Gender
            user.State = Request.Form["state"];
            if (user.FirstName.Length == 0 || user.LastName.Length == 0 || user.Designation.Length == 0 || string.IsNullOrWhiteSpace(user.Gender) || string.IsNullOrWhiteSpace(user.State))
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.UpdateUser(user, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            user.FirstName = ""; user.LastName = ""; user.Designation = "";
            successMessage = "User updated.";
            Response.Redirect("/Client/Index");
        }
    }
}
