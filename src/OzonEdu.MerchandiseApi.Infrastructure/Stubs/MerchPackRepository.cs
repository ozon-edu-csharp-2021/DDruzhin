using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Repositories.Infrastructure.Interfaces;
using OzonEdu.MerchandiseApi.Infrastructure.Repositories.Models;

namespace OzonEdu.MerchandiseApi.Infrastructure.Stubs
{
    public class MerchPackRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;
        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"UPDATE merch_packs 
                                    SET worker = @Worker,
                                        status = @Status,
                                        merch_type = @MerchType,
                                        merch_items = @MerchItems,
                                        request_date = @RequestDate,
                                        delivery_date = @DeliveryDate;";
            
            var parameters = new
            {
                Worker = itemToUpdate.Worker,
                Status = itemToUpdate.Status,
                MerchType = itemToUpdate.Type,
                MerchItems = itemToUpdate.MerchItems,
                RequestDate = itemToUpdate.RequestDate,
                DeliveryDate = itemToUpdate.DeliveryDate
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

        public async Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MerchPack>> FindByWorkerEmailAsync(string email, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT worker, status, merch_type, merch_items, request_date, delivery_date FROM merch_packs WHERE worker = @Email";
            var parameters = new
            {
                Email = email
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchPacks = await connection.QueryAsync<MerchPackDto>(commandDefinition);

            return merchPacks.Select(model => new MerchPack(
                new MerchType(model.MerchType,
                    Enumeration.GetAll<MerchType>().FirstOrDefault(type => type.Id == model.MerchType)?.Name),
                model.Items.Select(l => new MerchItem(new Sku(l))),
                new Worker(new Email(model.WorkerEmail)),
                requestDate: model.RequestDate,
                deliveryDate: model.DeliveryDate,
                status:new Status(model.Status,Enumeration.GetAll<Status>().FirstOrDefault(status => status.Id == model.Status)?.Name)
            ));
        }

        public async Task<MerchPack> CreateMerchPackAsync(MerchPack merchPack, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO merch_packs (worker, status, merch_type, merch_items, request_date, delivery_date)
                VALUES (@Worker, @Status, @MerchType, @MerchItems, @RequestDate, @DeliveryDate) RETURNING id;";

            var parameters = new
            {
                Worker = merchPack.Worker.Email.Value,
                Status = merchPack.Status.Id,
                MerchType = merchPack.Type.Id,
                RequestDate = merchPack.RequestDate,
                DeliveryDate = merchPack.DeliveryDate,
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
            merchPack.SetId(int.Parse(insertId.ToString()));
            
            _changeTracker.Track(merchPack);
            
            return merchPack;
        }
    }
}