using Framework.Persisting.Enums;

namespace Framework.Persisting.Interfaces
{
    public interface IOrderItem
    {
        string Name { get; set; }
        eOrderDirection Direction { get; set; }
    }
}
