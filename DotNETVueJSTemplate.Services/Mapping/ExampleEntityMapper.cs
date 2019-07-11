using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNETVueJSTemplate.Model;
using DotNETVueJSTemplate.Services.Dtos;

namespace DotNETVueJSTemplate.Services.Mapping
{
    public static class ExampleEntityMapper
    {
        internal static IEnumerable<ExampleEntityDto> ToDto(this IEnumerable<ExampleEntity> entities)
        {
            return entities?.Select(m => m.ToDto());
        }

        internal static ExampleEntityDto ToDto(this ExampleEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ExampleEntityDto
            {
                Id = entity.Id,
                ExampleProperty = entity.ExampleProperty,
                CreateDate = entity.CreateDate,
                CreatedBy = entity.CreatedBy,
                ModifiedDate = entity.ModifiedDate,
                ModifiedBy = entity.ModifiedBy
            };
        }

        internal static ExampleEntity ToEntity(this ExampleEntityDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new ExampleEntity
            {
                // Do not modify changes to system-generated values like Id, CreateDate, CreatedBy, ModifiedDate, and ModifiedBy
                ExampleProperty = dto.ExampleProperty
            };
        }

        internal static void Update(this ExampleEntity entity, ExampleEntityDto dto, DbContext context, string modifiedBy, DateTime modifiedDate)
        {
            if (entity != null && dto != null)
            {
                entity.ExampleProperty = dto.ExampleProperty;
                entity.ModifiedDate = modifiedDate;
                entity.ModifiedBy = modifiedBy;
            }
        }
    }
}
