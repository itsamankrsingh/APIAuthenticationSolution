using APIAuthentication.Models;
using APIAuthentication.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace APIAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        NetCoreAuthenticationContext dbContext = new NetCoreAuthenticationContext();
        //POST Api Controller
        [HttpPost]
        public string Post([FromBody] TblUserMst value)
        {
            //first we need to check if user already existed in db or not
            if (dbContext.TblUserMsts.Any(user => user.UserName.Equals(value.UserName)))
            {
                TblUserMst user = dbContext.TblUserMsts.Where(user => user.UserName.Equals(value.UserName)).FirstOrDefault();

                //calculate hash password from data of client and compare with hash in server with salt
                var client_post_hash_password = Convert.ToBase64String(
                    Common.SaltHashPassword(
                        Encoding.ASCII.GetBytes(value.Password),
                        Convert.FromBase64String(user.Salt)
                        ));

                if (client_post_hash_password.Equals(user.Password))
                {
                    return JsonConvert.SerializeObject(user);
                }
                else
                {
                    return JsonConvert.SerializeObject("Wrong Password");
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Username does not exists");
            }
        }
    }
}
