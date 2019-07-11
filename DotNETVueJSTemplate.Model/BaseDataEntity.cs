using System;

namespace DotNETVueJSTemplate.Model
{
    public abstract class BaseDataEntity : IDataEntity
    {
        public int Id { get; set; }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value.ToUtc(); }
        }

        public string CreatedBy { get; set; }

        private DateTime? _modifiedDate;
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value.ToUtc(); }
        }

        public string ModifiedBy { get; set; }
    }
}
