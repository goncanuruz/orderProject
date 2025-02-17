using AutoMapper;
using OrderProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Extensions
{
    public static class QuerableExtension
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static IQueryable<T> ToMapper<T>(this IQueryable<BaseEntity> queryable)
        {
            return _mapper.ProjectTo<T>(queryable);
        }
    }
}
