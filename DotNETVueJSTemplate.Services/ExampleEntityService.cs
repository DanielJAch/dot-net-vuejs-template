using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DotNETVueJSTemplate.Data;
using DotNETVueJSTemplate.Services.Dtos;
using DotNETVueJSTemplate.Services.Interfaces;
using DotNETVueJSTemplate.Services.Mapping;
using DotNETVueJSTemplate.Services.Messaging;
using DotNETVueJSTemplate.Services.Validation;
using NLog;

namespace DotNETVueJSTemplate.Services
{
    public sealed class ExampleEntityService : IExampleEntityService
    {
        private readonly ExampleContext _context;
        private readonly ILogger _logger;

        public ExampleEntityService(ExampleContext context, ILogger logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<IEnumerable<ExampleEntityDto>> GetAll()
        {
            return (await this._context.ExampleEntities.ToListAsync())
                .ToDto();
        }

        public async Task<ExampleEntityDto> GetById(int id)
        {
            return (await this._context.ExampleEntities.SingleOrDefaultAsync(m => m.Id == id))
                .ToDto();
        }

        public async Task<SaveResult<ExampleEntityDto>> Create(ExampleEntityDto dto)
        {
            var saveResult = new SaveResult<ExampleEntityDto>();
            var validator = new ExampleEntityValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var newEntity = dto.ToEntity();

                newEntity = this._context.ExampleEntities.Add(newEntity);

                await this._context.SaveChangesAsync();

                saveResult.Succeeded = true;
                saveResult.Data = newEntity.ToDto();
            }
            else
            {
                saveResult.Errors =
                    validationResult.Errors.Select(m => new ServiceError(m.PropertyName, m.ErrorMessage));
            }

            return saveResult;
        }

        public async Task<SaveResult<ExampleEntityDto>> Update(ExampleEntityDto dto)
        {
            var saveResult = new SaveResult<ExampleEntityDto>();
            var existingEntity = await this._context.ExampleEntities.SingleOrDefaultAsync(m => m.Id == dto.Id);

            if (existingEntity != null)
            {
                var validator = new ExampleEntityValidator();
                var validationResult = await validator.ValidateAsync(dto);

                if (validationResult.IsValid)
                {
                    existingEntity.Update(dto, this._context, null, DateTime.Now);

                    await this._context.SaveChangesAsync();

                    existingEntity = await this._context.ExampleEntities.SingleOrDefaultAsync(m => m.Id == dto.Id);

                    saveResult.Succeeded = true;
                    saveResult.Data = existingEntity.ToDto();
                }
                else
                {
                    saveResult.Errors =
                        validationResult.Errors.Select(m => new ServiceError(m.PropertyName, m.ErrorMessage));
                }
            }
            else
            {
                saveResult.Errors = saveResult.Errors.Append(new ServiceError("NonExistentEntity",
                    $"Entity Not Found (ID: {dto.Id})."));
            }

            return saveResult;
        }
    }
}
