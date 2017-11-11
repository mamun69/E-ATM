namespace MVC_EATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModels1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionHistories", "accountNo", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionHistories", "account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionHistories", "account_Id", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionHistories", "accountNo");
        }
    }
}
