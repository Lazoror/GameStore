using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Models.LanguageModels
{
    public abstract class TranslationEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid LanguageId { get; set; }

        public Guid EntityId { get; set; }

        public Language Language { get; set; }
    }
}