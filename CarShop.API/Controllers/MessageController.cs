using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Concrete;
using CarShop.API.UserProcess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IAnonimMessageService _anonimMessageService;
        private readonly IAdminMessageService _adminMessageService;

        public MessageController(IAnonimMessageService anonimMessageService, IAdminMessageService adminMessageService)
        {
            _anonimMessageService = anonimMessageService;
            _adminMessageService = adminMessageService;
        }


        [HttpPost("anonimMessage")]
        public IActionResult NewMessage([FromBody] AnonimMessageRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz istek.");
            }

            try
            {
                var anonimMessageModel = _anonimMessageService.NewMessage(
                    request.FullName,
                    request.Email,
                    request.Phone,
                    request.Message,
                    request.CarId
                );

                return Ok(anonimMessageModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }

        }


        [HttpGet("getMessage")]
        public IActionResult GetMessage()
        {
            try
            {
                var result = _anonimMessageService.GetMessage();

                if (result == null || !result.Data.Any())
                {
                    return NotFound(new { message = result.Message }); 
                }

                return Ok(result.Data); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Mesajlar alınamadı.", error = ex.Message });
            }
        }

        [HttpPost("answerMessage")]
        public async Task<IActionResult> AnswerMessage([FromBody] AnswerMessageDto dto)
        {

            var result = await _adminMessageService.AnswerMessageAsync(dto.Sender, dto.Receiver, dto.Message, dto.MessageId);
            return Ok(result); 

        }
        public class AnswerMessageDto
        {
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public string Message { get; set; }
            public int MessageId { get; set; }
        }


    }


    public class AnonimMessageRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int CarId { get; set; }
    }




}
