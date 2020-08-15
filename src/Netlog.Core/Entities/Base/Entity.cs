using System;

namespace Netlog.Core.Entities.Base
{
    public abstract class Entity : EntityBase<int>
    {
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
