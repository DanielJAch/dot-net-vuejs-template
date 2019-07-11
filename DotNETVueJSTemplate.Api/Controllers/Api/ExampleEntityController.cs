using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DotNETVueJSTemplate.Services.Dtos;
using DotNETVueJSTemplate.Services.Interfaces;
using NLog;

namespace DotNETVueJSTemplate.Api.Controllers.Api
{
    [RoutePrefix("api/example-entities")]
    public class ExampleEntityController : BaseApiController
    {
        private IExampleEntityService _exampleEntityService;

        public ExampleEntityController(IExampleEntityService exampleEntityService, ILogger logger) : base(logger)
        {
            this._exampleEntityService = exampleEntityService;
        }

        // GET: api/example-entities
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var entities = await this._exampleEntityService.GetAll();

                return this.Ok(entities);
            }
            catch (Exception e)
            {
                return this.HandleException(e);
            }
        }

        // GET: api/example-entities/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var entity = await this._exampleEntityService.GetById(id);

                if (entity == null)
                {
                    return this.NotFound();
                }

                return this.Ok(entity);
            }
            catch (Exception e)
            {
                return this.HandleException(e);
            }
        }

        // POST: api/example-entities
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ExampleEntityDto divisionDto)
        {
            try
            {
                var result = await this._exampleEntityService.Create(divisionDto);

                if (result.Errors != null && result.Errors.Any())
                {
                    foreach (var serviceError in result.Errors)
                    {
                        this.ModelState.AddModelError(serviceError.Code, serviceError.Description);
                    }
                }

                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.HandleException(e);
            }
        }

        // PUT: api/example-entities/5
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ExampleEntityDto divisionDto)
        {
            try
            {
                var result = await this._exampleEntityService.Update(divisionDto);

                if (result.Errors != null && result.Errors.Any())
                {
                    foreach (var serviceError in result.Errors)
                    {
                        this.ModelState.AddModelError(serviceError.Code, serviceError.Description);
                    }
                }

                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.HandleException(e);
            }

        }
    }
}
