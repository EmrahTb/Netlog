namespace Netlog.Core.Entities.Base
{
    public interface IEntityBase<TId>
    {
        TId ID { get; }
    }
}
