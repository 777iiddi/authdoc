using API.models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ItokenService //interface for the token service
    {
        Task<string> createToken(ApplicationUser user); //method to create a token for the user

    }
}
