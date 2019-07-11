using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DotNETVueJSTemplate.Api.Controllers.Api;
using DotNETVueJSTemplate.Services.Dtos;
using DotNETVueJSTemplate.Services.Interfaces;
using DotNETVueJSTemplate.Services.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DotNETVueJSTemplate.Tests.ApiControllers
{
    [TestClass]
    public class TestExampleEntityController
    {
        private readonly ExampleEntityController _exampleEntitiesController;
        private readonly IList<ExampleEntityDto> _exampleEntities;

        public TestExampleEntityController()
        {
            MockServiceSetup.SetupHttpContext();

            this._exampleEntities = new List<ExampleEntityDto>(MockServiceSetup.SetupExampleEntities());
            this._exampleEntitiesController = new ExampleEntityController(this.SetupExampleEntitiesService(), MockServiceSetup.SetupLogger());
        }

        private IExampleEntityService SetupExampleEntitiesService()
        {
            var exampleEntityRepo = new Mock<IExampleEntityService>();

            exampleEntityRepo.SetupBaseMethods(this._exampleEntities);

            return exampleEntityRepo.Object;
        }

        [TestMethod]
        public async Task ExampleEntities_GetAll_Test()
        {
            var result = await this._exampleEntitiesController.Get() as OkNegotiatedContentResult<IEnumerable<ExampleEntityDto>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            CollectionAssert.AreEqual(result.Content.ToList(), this._exampleEntities.ToList());
        }

        [TestMethod]
        public async Task ExampleEntities_GetById_Test()
        {
            var result = await this._exampleEntitiesController.Get(3) as OkNegotiatedContentResult<ExampleEntityDto>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(result.Content.Id, 3);
            Assert.AreEqual(result.Content.ExampleProperty, "Example Entity 3");
        }

        [TestMethod]
        public async Task ExampleEntity_Create_Test()
        {
            var exampleEntity = new ExampleEntityDto
            {
                ExampleProperty = "Brand New ExampleEntity"
            };

            var result = await this._exampleEntitiesController.Post(exampleEntity) as OkNegotiatedContentResult<SaveResult<ExampleEntityDto>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.Data);
        }

        [TestMethod]
        public async Task ExampleEntity_Update_Test()
        {
            var getResult = await this._exampleEntitiesController.Get(3) as OkNegotiatedContentResult<ExampleEntityDto>;

            Assert.IsNotNull(getResult);
            Assert.IsNotNull(getResult.Content);

            // Need to reset HttpContext since the above request killed it.
            MockServiceSetup.SetupHttpContext();

            var exampleEntity = getResult.Content;
            var newName = "Renamed ExampleEntity " + exampleEntity.Id;

            exampleEntity.ExampleProperty = newName;

            var saveResult = await this._exampleEntitiesController.Put(exampleEntity.Id, exampleEntity) as OkNegotiatedContentResult<SaveResult<ExampleEntityDto>>;

            Assert.IsNotNull(saveResult);
            Assert.IsNotNull(saveResult.Content);
            Assert.IsNotNull(saveResult.Content.Data);
            Assert.AreEqual(saveResult.Content.Data.ExampleProperty, newName);

            // Need to reset HttpContext since the above request killed it.
            MockServiceSetup.SetupHttpContext();

            getResult = await this._exampleEntitiesController.Get(3) as OkNegotiatedContentResult<ExampleEntityDto>;

            Assert.IsNotNull(getResult);
            Assert.IsNotNull(getResult.Content);
            Assert.AreEqual(getResult.Content.ExampleProperty, newName);
        }
    }
}
