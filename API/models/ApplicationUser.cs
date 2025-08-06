using Microsoft.AspNetCore.Identity;

namespace API.models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }= String.Empty; //eviter  null 
    }
}
