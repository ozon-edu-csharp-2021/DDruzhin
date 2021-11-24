using FluentMigrator;
namespace OzonEdu.MerchandiseApi.Migrator.Migrations
{
    [Migration(1)]
    public class MerchItemTable:Migration 
    {
        public override void Up()
        {
            
            Execute.Sql(@"
                CREATE TABLE if NOT EXISTS merch_items(
                    id BIGSERIAL PRIMARY KEY,
                    sku BIGINT NOT NULL,
                    availability BOOLEAN NOT NULL);"
            );
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if EXISTS merch_items;");
        }
    }
}