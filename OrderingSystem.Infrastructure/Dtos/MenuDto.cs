using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Dtos
{
    public class AddMenuDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public MenuType MenuType { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public Guid? MenuGroupId { get; set; }
    }
    public class UpdateMenuDto : AddMenuDto
    {

    }
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MenuType MenuType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Guid? MenuGroupId { get; set; }
    }
}
