namespace EShopping.Core.Infrastructure.Interface
{
    public interface IBaseEntity : IEntity
    {
        void OnCreate();

        void OnUpdate();

        void OnDelete();
    }
}
