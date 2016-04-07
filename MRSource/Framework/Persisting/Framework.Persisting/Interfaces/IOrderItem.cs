using Framework.Persisting.Enums;

namespace Framework.Persisting.Interfaces
{
    public interface IOrderItem
    {
        string SqlName { get; set; }
        eOrderDirection Direction { get; set; }
    }
}
