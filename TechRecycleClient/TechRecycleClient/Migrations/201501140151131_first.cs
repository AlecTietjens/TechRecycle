namespace TechRecycleClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BulkTicketDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TicketNumber = c.String(nullable: false),
                        BinQuantity = c.Int(nullable: false),
                        BinLocation1 = c.String(),
                        BinLocation2 = c.String(),
                        BinLocation3 = c.String(),
                        BinLocation4 = c.String(),
                        BinLocation5 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HBITicketDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TicketNumber = c.String(nullable: false),
                        NeedsSecureBins = c.Boolean(nullable: false),
                        SecureBinQuantity = c.Int(nullable: false),
                        DestructionLocation = c.String(),
                        WitnessType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TicketNumber = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        AlternateName = c.String(nullable: false),
                        AlternatePhone = c.String(nullable: false),
                        AlternateEmail = c.String(nullable: false),
                        Building = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        ComputerQuantity = c.Int(nullable: false),
                        MonitorQuantity = c.Int(nullable: false),
                        MiscQuantity = c.Int(nullable: false),
                        PickupSize = c.String(nullable: false),
                        IsHBIRequest = c.Boolean(nullable: false),
                        AdditionalNotes = c.String(),
                        CurrentStatus = c.String(),
                        LastModifiedBy = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        CloseDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ticket");
            DropTable("dbo.HBITicketDetails");
            DropTable("dbo.BulkTicketDetails");
        }
    }
}
