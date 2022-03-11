using Api.Interfaces;
using Api.Models.Requests;
using Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("promo")]
public class PromotionController : ControllerBase
{
    private readonly ILogger<PromotionController> _logger;
    private readonly IPromotionService _promotionService;

    public PromotionController(ILogger<PromotionController> logger, IPromotionService promotionService)
    {
        _logger = logger;
        _promotionService = promotionService;
    }

    [HttpPost]
    public int PostPromotion(PromotionRequest promotionRequest)
    {
        var promotion = _promotionService.CreatePromotion(
            promotionRequest.Name,
            promotionRequest.Description
        );

        return promotion.Id;
    }

    [HttpPut("{id}")]
    public ObjectResult PutPromotion(int id, PromotionRequest promotionRequest)
    {
        try
        {
            _promotionService.EditPromotion(id, promotionRequest.Name, promotionRequest.Description);
            return GetResponseResult(
                "Successfully updated promotion.",
                StatusCodes.Status200OK
            );
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            return GetResponseResult(
                "Invalid promotion id.",
                StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpGet("{id}")]
    public JsonResult GetPromotion(int id)
    {
        try
        {
            return new JsonResult(_promotionService.GetPromotion(id));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            return GetErrorJsonResult("Invalid promotion id.", StatusCodes.Status400BadRequest);
        }
    }

    [HttpGet]
    public JsonResult GetPromotions()
    {
        var promotions = _promotionService.GetAllPromotions();
        var promotionResponse = promotions.Select(promotion => new PromotionResponse
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Description = promotion.Description
        });

        return new JsonResult(promotionResponse);
    }

    [HttpDelete("{id}")]
    public ObjectResult DeletePromotion(int id)
    {
        try
        {
            _promotionService.DeletePromotion(id);
            return GetResponseResult(
                "Successfully deleted promotion.",
                StatusCodes.Status200OK
            );
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            
            return GetResponseResult(
                "Promotion with such id does not exist.",
                StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpPost("{id}/participant")]
    public int PostParticipant(int id, ParticipantRequest participantRequest)
    {
        try
        {
            var participant = _promotionService.AddParticipant(id, participantRequest.Name);
            return participant.Id;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return -1;
        }
    }

    [HttpDelete("{id}/participant/{participantId}")]
    public ObjectResult DeleteParticipant(int id, int participantId)
    {
        try
        {
            _promotionService.DeleteParticipant(id, participantId);
            return GetResponseResult(
                "Successfully deleted participant.",
                StatusCodes.Status200OK
            );
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            return GetResponseResult(
                "Promotion or participant with such id does not exist.",
                StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpPost("{id}/prize")]
    public int PostPrize(int id, PrizeRequest prizeRequest)
    {
        try
        {
            var participant = _promotionService.AddPrize(id, prizeRequest.Description);
            return participant.Id;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return -1;
        }
    }

    [HttpDelete("{id}/prize/{prizeId}")]
    public ObjectResult DeletePrize(int id, int prizeId)
    {
        try
        {
            _promotionService.DeletePrize(id, prizeId);
            return GetResponseResult(
                "Successfully deleted prize.",
                StatusCodes.Status200OK
            );
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            return GetResponseResult(
                "Promotion or prize with such id does not exist.",
                StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpPost("{id}/raffle")]
    public JsonResult PostRaffle(int id)
    {
        try
        {
            var raffle = _promotionService.CreateRaffle(id);
            if (raffle == null)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return GetErrorJsonResult(
                    "Number of participants and prized does not match.",
                    StatusCodes.Status409Conflict);
            }

            return new JsonResult(raffle);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex.Message);
            return GetErrorJsonResult("Invalid promotion id", StatusCodes.Status400BadRequest);
        }
    }

    private JsonResult GetErrorJsonResult(string message, int statusCode)
    {
        return new JsonResult(new Response
        {
            Message = message
        })
        {
            StatusCode = statusCode
        };;
    }
    
    private ObjectResult GetResponseResult(string message, int statusCode)
    {
        return new ObjectResult(new Response
        {
            Message = message
        })
        {
            StatusCode = statusCode
        };
    }
}