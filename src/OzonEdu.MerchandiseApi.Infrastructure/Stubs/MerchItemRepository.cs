using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Infrastructure.Repositories.Infrastructure.Interfaces;
namespace OzonEdu.MerchandiseApi.Infrastructure.Stubs
{
    public class MerchItemRepository : IMerchItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchItemRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"UPDATE merch_items SET availability = @Availability;";

            var parameters = new
            {
                Availability = itemToUpdate.Availability
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            
            await connection.ExecuteAsync(commandDefinition);
            
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }

        public async Task<MerchItem> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchItem> CreateMerchItemAsync(MerchItem merchItem, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO merch_items (id, sku, availability)
                VALUES (@Id, @Sku, @Availability);";

            var parameters = new
            {
                Id = merchItem.Id,
                Sku = merchItem.Sku.Value,
                Availability = merchItem.Availability
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            
            var insertId = await connection.ExecuteScalarAsync(commandDefinition);
            
            // по идее так как у нас запрос на добавление и возврат один,
            // то при неудачной вставке нужный экзепшн остановит все раньше
            merchItem.SetId(int.Parse(insertId.ToString()));
            
            _changeTracker.Track(merchItem);
            return merchItem;
        }
    }

}