using System;
using System.Linq;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        public readonly AirlineDbContext _context;
        public readonly IMapper _mapper;
        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{secret}", Name="GetSubscriptionBySecret")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(string secret)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.Secret == secret);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.WebhookURI ==webhookSubscriptionCreateDto.WebhookURI);
            if(subscription == null)
            {
                subscription = _mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);
                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanAus";
                try
                {
                    _context.WebhookSubscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
                return CreatedAtRoute(nameof(GetSubscriptionBySecret), new {secret = webhookSubscriptionReadDto.Secret}, webhookSubscriptionReadDto);
            }
            else 
            {
                return NoContent();
            }
        }
    }
}