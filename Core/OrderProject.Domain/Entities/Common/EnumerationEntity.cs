using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderProject.Domain.Entities.Common
{
    public class EnumerationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnumId { get; private set; }

        protected EnumerationEntity(int enumId)
        {
            EnumId = enumId;
        }
    }
}
