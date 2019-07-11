using System.Collections.Generic;

namespace DotNETVueJSTemplate.Services.Messaging
{
    public class SaveResult<T> : SaveResult
    {
        public T Data { get; set; }
    }

    public class SaveResult
    {
        public SaveResult()
        {
            this.Errors = new List<ServiceError>();
        }

        public bool Succeeded { get; set; }

        public IEnumerable<ServiceError> Errors { get; set; }

        public dynamic Metadata { get; set; }
    }
}
