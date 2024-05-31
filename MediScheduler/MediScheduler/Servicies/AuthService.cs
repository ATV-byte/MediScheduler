using System.Security.Claims;
using System.Threading.Tasks;
using MediScheduler.Entities;
using MediScheduler.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AuthService
{
    private readonly MediSchedulerContext _context;

    public AuthService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<string?> Authenticate(UserDTO model, HttpContext httpContext)
    {
        var userAccount = _context.Users.Where(x => x.Username == model.UserName).FirstOrDefault();
        if (userAccount is null || userAccount.Password != model.Password)
        {
            return "Invalid Username or Password";
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim(ClaimTypes.Role, userAccount.Role),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await httpContext.SignInAsync(principal);
        return null;
    }
}
