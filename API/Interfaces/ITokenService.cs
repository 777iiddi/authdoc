using API.models;

namespace API.Interfaces
{
    public interface ItokenService //interface for the token service
    {
        String createToken(ApplicationUser user); //method to create a token for the user

    }
}
