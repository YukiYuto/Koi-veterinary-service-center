using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IOrderItemsRepository : IRepository<OrderItems>
    {
        void Update(OrderItems orderItems);
        Task<OrderItems> GetById(int id);
    }
}
