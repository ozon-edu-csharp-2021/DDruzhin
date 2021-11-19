using FluentMigrator;
namespace OzonEdu.MerchandiseApi.Migrator.Migrations
{
    [Migration(2)]
    public class MerchPackTable:Migration 
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if NOT EXISTS merch_packs(
                    id BIGSERIAL PRIMARY KEY,
                    worker TEXT NOT NULL,
                    status INT NOT NULL,
                    merch_type INT NOT NULL,
                    merch_items INTEGER[] NOT NULL,
                    request_date DATE NOT NULL,
                    delivery_date DATE);"
            );
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TABLE if EXISTS merch_packs;");
        }
    }
}