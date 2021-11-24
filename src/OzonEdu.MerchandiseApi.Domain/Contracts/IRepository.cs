using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.Contracts
{

    public interface IRepository<TEntity> where TEntity : Entity
    {
    }
}