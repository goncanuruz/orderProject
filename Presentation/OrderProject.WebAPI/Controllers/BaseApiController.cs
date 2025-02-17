using AutoMapper;
using OrderProject.Application.DTOs;
using OrderProject.Application.Extensions;
using OrderProject.Application.MappingProfiles;
using Microsoft.AspNetCore.Mvc;

namespace OrderProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly BaseResponse _baseResponse;
        protected readonly IMapper _mapper;
        public BaseApiController()
        {
            _baseResponse = BaseResponse.Create();
            _mapper = new MapperConfiguration(c => c.AddProfile(new OrderProjectMapperProfile())).CreateMapper();
            QuerableExtension.SetMapper(_mapper);
        }

        //#SUCCESS
        [NonAction]
        protected IActionResult Success<T>(T data)
        {
            _baseResponse.SetSuccess("Başarılı", data);
            return Success(_baseResponse);
        }
        [NonAction]
        protected IActionResult Success<T>()
        {
            _baseResponse.SetSuccess("Başarılı");
            return Success(_baseResponse);
        }
        [NonAction]
        protected IActionResult Success(BaseResponse data)
        {
            return Ok(data);
        }

        //#BAD REQUEST
        [NonAction]
        protected IActionResult BadRequest()
        {
            _baseResponse.SetError("Hata");
            return BadRequest(_baseResponse);
        }
        [NonAction]
        protected IActionResult BadRequest(BaseResponse data)
        {
            return StatusCode(400, data);
        }


        //#ERROR
        [NonAction]
        protected IActionResult Error()
        {
            _baseResponse.SetError("Hata");
            return Error(_baseResponse);
        }
        [NonAction]
        protected IActionResult Error(BaseResponse data)
        {
            return StatusCode(500, data);
        }

    }
}
