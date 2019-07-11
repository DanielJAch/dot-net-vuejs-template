using System;

namespace DotNETVueJSTemplate.Model
{
    public interface IDataEntity :IEntity
    {
        DateTime CreateDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedDate { get; set; }

        string ModifiedBy { get; set; }
    }
}
