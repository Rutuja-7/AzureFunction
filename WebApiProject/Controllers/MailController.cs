using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;
using WebApiProject.Repository;

namespace WebApiProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    public class MailController : ControllerBase
    {
        private readonly IMailRepository _mailService;
        public MailController(IMailRepository mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// This method will send mail to the user based upon the user mail id provided
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("SendMail")]
        public IActionResult Send(MailRequest request)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendEmail(request);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}