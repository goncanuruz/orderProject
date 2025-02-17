
using OrderProject.Application.DTOs.Controller;
using OrderProject.Application.DTOs;
using OrderProject.Application.Repositories;
using OrderProject.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace OrderProject.WebAPI.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class BaseController<TEntity, ListDto, DetailDto, CreateDto, UpdateDto> : BaseController<TEntity, ListDto, DetailDto>
        where TEntity : BaseEntity
        where ListDto : IListDto
        where DetailDto : IDetailDto
        where UpdateDto : IUpdateDto
        where CreateDto : ICreateDto
    {
        protected readonly IWriteRepository<TEntity> _writeRepository;
        public BaseController( IReadRepository<TEntity> readRepository, IWriteRepository<TEntity> writeRepository) : base( readRepository)
        {
            _writeRepository = writeRepository;
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public virtual IActionResult Create(CreateDto model)
        {
            var entity = _mapper.Map<TEntity>(model);
            entity.Id = Guid.NewGuid();
            _writeRepository.Create(entity);

            return Success(entity);
        }
    }

    [Route("/api/[Controller]")]
    [ApiController]
    public class BaseController<TEntity, ListDto, DetailDto> : BaseController
        where TEntity : BaseEntity
        where ListDto : IListDto
        where DetailDto : IDetailDto
    {
        protected readonly IReadRepository<TEntity> _readRepository;
        protected bool ClientFilter = true;
        protected ControllerPermissionDto? _permission;

        public BaseController( IReadRepository<TEntity> readRepository) : base()
        {
            _readRepository = readRepository;

        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]

        public virtual IActionResult Get()
        {

            var query = _mapper.ProjectTo<ListDto>(_readRepository.GetAll());

            return Success(query);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : BaseApiController
    {
        public BaseController() : base()
        {
        }
    }
}
