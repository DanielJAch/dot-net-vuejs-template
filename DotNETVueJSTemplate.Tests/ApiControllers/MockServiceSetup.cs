using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Moq;
using DotNETVueJSTemplate.Services.Dtos;
using DotNETVueJSTemplate.Services.Messaging;
using DotNETVueJSTemplate.Services.Interfaces;
using NLog;

namespace DotNETVueJSTemplate.Tests.ApiControllers
{
    public static class MockServiceSetup
    {
        private const string IDENTITY_EMAIL = "test@gmail.com";

        /// <summary>
        /// Setup Returns for services implementing IEntityReadServiceAsync
        /// </summary>
        public static void SetupBaseReadMethods<TDto, TService>(this Mock<TService> service, IEnumerable<TDto> dtos)
            where TDto : IDto
            where TService : class, IEntityReadServiceAsync<TDto>
        {
            service.Setup(c => c.GetAll())
                .Returns(() =>
                    Task.Run(() => dtos)
                );

            service.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Task<TDto>>(id =>
                    Task.Run(() => dtos.FirstOrDefault(c => c.Id == id)))
                );
        }

        /// <summary>
        /// Setup Returns for services implementing IEntityWriteServiceAsync
        /// </summary>
        public static void SetupBaseWriteMethods<TDto, TService>(this Mock<TService> service, IEnumerable<TDto> dtos)
            where TDto : IDto
            where TService : class, IEntityWriteServiceAsync<TDto>
        {
            service.Setup(c => c.Create(It.IsAny<TDto>()))
                .Callback(new Action<TDto>(newItem => {
                    var list = dtos.ToList();
                    dynamic maxId = list.Last().Id;
                    dynamic newId = maxId + 1;
                    newItem.Id = newId;

                    list.Add(newItem);
                    dtos = list;
                }))
                .Returns(new Func<TDto, Task<SaveResult<TDto>>>(dto =>
                    Task.FromResult(new SaveResult<TDto>()
                    {
                        Data = dtos.FirstOrDefault(d => d.Id == dto.Id)
                    })
                ));

            service.Setup(c => c.Update(It.IsAny<TDto>()))
                .Callback(new Action<TDto>(dto => {
                    var oldItem = dtos.FirstOrDefault(d => d.Id == dto.Id);

                    if (dto != null)
                    {
                        oldItem = dto;
                    }
                }))
                .Returns(new Func<TDto, Task<SaveResult<TDto>>>(dto =>
                    Task.FromResult(new SaveResult<TDto>()
                    {
                        Data = dtos.FirstOrDefault(d => d.Id == dto.Id)
                    })
                ));
        }

        public static ExampleEntityDto SetupExampleEntityDto(int ordinal)
        {
            return new ExampleEntityDto
            {
                Id = ordinal,
                ExampleProperty = $"Example Entity {ordinal}"
            };
        }

        public static IEnumerable<ExampleEntityDto> SetupExampleEntities()
        {
            var divisions = new List<ExampleEntityDto>();

            for (var i = 1; i <= 5; i++)
            {
                divisions.Add(MockServiceSetup.SetupExampleEntityDto(i));
            }

            return divisions;
        }

        /// <summary>
        /// Setup Returns for services implementing IEntityServiceAsync
        /// </summary>
        public static void SetupBaseMethods<TDto, TService>(this Mock<TService> service, IEnumerable<TDto> dtos)
            where TDto : IDto
            where TService : class, IEntityServiceAsync<TDto>
        {
            var list = dtos.ToList();

            service.SetupBaseReadMethods(list);
            service.SetupBaseWriteMethods(list);
        }

        public static ILogger SetupLogger()
        {
            return new Mock<ILogger>().Object;
        }

        public static HttpContext SetupHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );

            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(MockServiceSetup.IDENTITY_EMAIL),
                new string[0]
            );

            return HttpContext.Current;
        }
    }
}
