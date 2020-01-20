using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}