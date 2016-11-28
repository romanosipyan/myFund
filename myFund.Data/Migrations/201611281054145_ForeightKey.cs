namespace myFund.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeightKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockEntities", "StockTypeEntity_Id", "dbo.StockTypeEntities");
            DropIndex("dbo.StockEntities", new[] { "StockTypeEntity_Id" });
            DropColumn("dbo.StockEntities", "StockTypeId");
            RenameColumn(table: "dbo.StockEntities", name: "StockTypeEntity_Id", newName: "StockTypeId");
            AlterColumn("dbo.StockEntities", "StockTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StockEntities", "StockTypeId");
            AddForeignKey("dbo.StockEntities", "StockTypeId", "dbo.StockTypeEntities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockEntities", "StockTypeId", "dbo.StockTypeEntities");
            DropIndex("dbo.StockEntities", new[] { "StockTypeId" });
            AlterColumn("dbo.StockEntities", "StockTypeId", c => c.Int());
            RenameColumn(table: "dbo.StockEntities", name: "StockTypeId", newName: "StockTypeEntity_Id");
            AddColumn("dbo.StockEntities", "StockTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StockEntities", "StockTypeEntity_Id");
            AddForeignKey("dbo.StockEntities", "StockTypeEntity_Id", "dbo.StockTypeEntities", "Id");
        }
    }
}
