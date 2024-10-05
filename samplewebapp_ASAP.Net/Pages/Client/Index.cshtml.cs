using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;
using samplewebapp_ASAP.Net.Model;

namespace samplewebapp_ASAP.Net.Pages.Client
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public List<User> listUsers = new List<User>();
        public void OnGet()
        {
            DAL dal = new DAL();
            listUsers = dal.GetUsers(_configuration);
        }
    }
}