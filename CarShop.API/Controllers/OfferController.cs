using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Entity;
using CarShop.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IUserMakeAnOfferService _userMakeAnOfferService;

        public OfferController(IUserMakeAnOfferService userMakeAnOfferService)
        {
            _userMakeAnOfferService = userMakeAnOfferService;
        }

        [HttpPost("makeNewOffer")]
        public IActionResult NewOffer([FromBody] NewOfferRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz istek.");
            }
            try
            {
                var makeAnOfferModel = _userMakeAnOfferService.NewOffer(
                    request.FullName,
                    request.Email,
                    request.Phone,
                    request.Offer,
                    request.CarId
                );

                return Ok(makeAnOfferModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }
        }
        

    }
}