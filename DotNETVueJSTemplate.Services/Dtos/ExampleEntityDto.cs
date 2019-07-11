using System;

namespace DotNETVueJSTemplate.Services.Dtos
{
    public class ExampleEntityDto : IDto
    {
        public int Id { get; set; }
        public string ExampleProperty { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
