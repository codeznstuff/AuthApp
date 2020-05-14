using Akka.Actor;
using AuthorizationManager.Managers;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationsController : Controller
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ApplicationsController");

        private readonly IActorRef _applicationActorPool;

        public ApplicationsController(IActorRef applicationActorPool)
        {
            _applicationActorPool = applicationActorPool;
        }

        [ActionName("GetUser")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery(Name = "userId")] Guid userId)
        {
            try
            {
                var result = await _applicationActorPool.Ask<User>(new Messages.GetUser(userId));
                switch (result)
                {
                    case User user:
                        return user;
                    default:
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUser", ex);
                return BadRequest();
            }
        }

        [ActionName("GetApplicationUser")]
        [HttpGet]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser([FromQuery(Name = "applicationName")] string applicationName, [FromQuery(Name = "userId")] Guid userId)
        {
            try
            {
                var result = await _applicationActorPool.Ask<ApplicationUser>(new Messages.GetApplicationUser(applicationName, userId));
                switch (result)
                {
                    case ApplicationUser applicationUser:
                        return applicationUser;
                    default:
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetApplicationUser", ex);
                return BadRequest();
            }
        }

        [ActionName("GetApplicationUsers")]
        [HttpGet]
        public async Task<ActionResult<ApplicationUser[]>> GetApplicationUsers([FromQuery(Name = "applicationName")] string applicationName)
        {
            try
            {
                var result = await _applicationActorPool.Ask<ApplicationUser[]>(new Messages.GetApplicationUsers(applicationName));
                switch (result)
                {
                    case ApplicationUser[] applicationUsers:
                        return applicationUsers;
                    default:
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetApplicationUsers", ex);
                return BadRequest();
            }
        }

        //[Authorize(Policy = "RequiresAdministrativePrivileges")]
        [ActionName("AddApplication")]
        [HttpPost]
        public async Task<ActionResult<Application>> AddApplication([FromBody] string applicationName)
        {
            try
            {
                var result = await _applicationActorPool.Ask<Application>(new Messages.AddApplication(applicationName));
                switch (result)
                {
                    case Application application:
                        return application;
                    default:
                        return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddApplication", ex);
                return BadRequest();
            }
        }

        //[Authorize(Policy = "RequiresAdministrativePrivileges")]
        [ActionName("RemoveApplication")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveApplication([FromQuery(Name = "applicationId")] Guid applicationId)
        {
            try
            {
                var result = await _applicationActorPool.Ask<bool>(new Messages.RemoveApplication(applicationId));
                switch (result)
                {
                    case true:
                        return Accepted();
                    default:
                        return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in RemoveApplication", ex);
                return BadRequest();
            }
        }
    }
}