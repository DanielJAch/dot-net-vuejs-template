namespace DotNETVueJSTemplate.Services.Messaging
{
    public class ServiceError
    {
        public ServiceError(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{this.Code}: {this.Description}";
        }
    }
}
