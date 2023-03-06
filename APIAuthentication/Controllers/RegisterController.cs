using APIAuthentication.Models;
using APIAuthentication.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace APIAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        NetCoreAuthenticationContext dbContext = new NetCoreAuthenticationContext();
        //POST Api Controller
        [HttpPost]
        public string Post([FromBody] TblUserMst value)
        {
            //first we need to check if user already existed in db or not
            if (!dbContext.TblUserMsts.Any(user => user.UserName.Equals(value.UserName)))
            {
                TblUserMst user = new TblUserMst();

                user.UserName = value.UserName;
                user.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(value.Password),
                    Convert.FromBase64String(user.Salt)
                    ));

                //Add to database
                try
                {
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    return JsonConvert.SerializeObject("User Registered Sucessfully");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }

            }
            else
            {
                return JsonConvert.SerializeObject("Username already exists");
            }
        }
    }
}
