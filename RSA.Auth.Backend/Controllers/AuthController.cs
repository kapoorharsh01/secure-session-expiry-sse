using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RsaAuth.Backend.Context;
using RsaAuth.Backend.DTOs;
using RsaAuth.Backend.Helpers;
using RsaAuth.Backend.Models;

namespace RsaAuth.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public AuthController(AppDbContext authContext)
        {
            _authContext = authContext;

        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> userSignUp(SignupDto dto)
        {
            var user = new User
            {
                FullName = EncDscRSA.Decrypt(dto.FullName),
                DateOfBirth = dto.DateOfBirth,
                Mobile = EncDscRSA.Decrypt(dto.Mobile),
                Email = EncDscRSA.Decrypt(dto.Email),
                PasswordHash = dto.Password
            };

            try
            {
                await _authContext.Users.AddAsync(user);
                await _authContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Email already exists");
            }

            return Ok(new { message = "Sign up successfull" });
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> userAuthenticate([FromBody] LoginDto dto)
        {
            if (dto == null) return BadRequest();

            var email = EncDscRSA.Decrypt(dto.Email);

            var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return BadRequest("Invalid credentials");

            if (user.PasswordHash != dto.Password) return BadRequest("Invalid credentials");

            SessionStore.SessionExpiry = DateTime.UtcNow.AddSeconds(20);

            return Ok(new { name = user.FullName, message = "Login successfull" });
        }

        [HttpGet("events")]
        public async Task Events()
        {
            Response.ContentType = "text/event-stream";

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                var remaining = SessionStore.SessionExpiry - DateTime.UtcNow;

                if(remaining.TotalSeconds <= 10 && remaining.TotalSeconds > 0)
                {
                    await Response.WriteAsync($"event: warning\ndata: {Math.Ceiling(remaining.TotalSeconds)}\n\n");
                    await Response.Body.FlushAsync();
                }

                if(remaining.TotalSeconds <= 0)
                {
                    await Response.WriteAsync($"event: expired\ndata: session-expired\n\n");
                    await Response.Body.FlushAsync();
                    break;
                }

                await Task.Delay(1000);
            }
        }

        [HttpPost("extend")]
        public ActionResult ExtendSession()
        {
            SessionStore.SessionExpiry = DateTime.UtcNow.AddSeconds(20);
            return Ok(new { message = "Session extended" }); 
        }




















































        //[HttpPost("login")]
        //public async Task<ActionResult> userLogin(LoginDto dto)
        //{
        // BELOW APPROACH WON'T WORK AS EVERYTIME RSA GIVES DIFF. ENCRYPTED VALUE

        //var encEmail = EncDscRSA.Encrypt(dto.Email);
        //var encPass = EncDscRSA.Encrypt(dto.Password);

        //var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Email == encEmail && u.Password == encPass);

        //if (user == null)
        //{
        //    return Unauthorized(new { message = "Invalid Credentials" });
        //}

        //return Ok(new {data = user, pvtKey = RsaKeyManager.loadPrivateKey()});


        // even below approach isn't optimised cuz decrypting every row in db is quite heavy op. to perform
        // better to store values without decryption or perform hashing for passwords
        //var users = await _authContext.Users.ToListAsync();

        //foreach(var user in users)
        //{
        //    var encEmail = EncDscRSA.Decrypt(user.Email);
        //    var encPass = EncDscRSA.Decrypt(user.PasswordHash);

        //    if (encEmail == dto.Email && encPass == dto.Password)
        //        return Ok(new { data = user, privateKey = RsaKeyManager.loadPrivateKey() });
        //}

        //return Unauthorized(new { message = "Invalid Credentials" });
        //}


    }

}
