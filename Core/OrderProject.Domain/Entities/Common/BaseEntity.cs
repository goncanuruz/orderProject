using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Common
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public Guid? DeletedUserId { get; set; }
    }
    public class BaseEntity: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public Guid? DeletedUserId { get; set; }
    }
}
