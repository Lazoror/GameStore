using System;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class PlatformTypeBuilder
    {
        private Platform _platform;

        public PlatformTypeBuilder()
        {
            _platform = new Platform();
        }

        public PlatformTypeBuilder WithId(Guid id)
        {
            _platform.Id = id;

            return this;
        }

        public PlatformTypeBuilder WithType(string type)
        {
            _platform.Name = type;

            return this;
        }

        public Platform Build()
        {
            var result = _platform;
            _platform = new Platform();

            return result;
        }
    }
}