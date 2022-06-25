namespace Makale.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Begeniler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kullanici_Id = c.Int(),
                        Makale_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicilar", t => t.Kullanici_Id)
                .ForeignKey("dbo.Makaleler", t => t.Makale_Id, cascadeDelete: true)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Makale_Id);
            
            CreateTable(
                "dbo.Kullanicilar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(maxLength: 25),
                        Soyadi = c.String(maxLength: 25),
                        KullaniciAdi = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 25),
                        ProfilResmi = c.String(maxLength: 30),
                        Admin = c.Boolean(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        AktifGuid = c.Guid(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Makaleler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 60),
                        Icerik = c.String(nullable: false, maxLength: 1000),
                        Taslak = c.Boolean(nullable: false),
                        BegeniSayisi = c.Int(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                        Kullanici_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategoriler", t => t.KategoriId, cascadeDelete: true)
                .ForeignKey("dbo.Kullanicilar", t => t.Kullanici_Id)
                .Index(t => t.KategoriId)
                .Index(t => t.Kullanici_Id);
            
            CreateTable(
                "dbo.Kategoriler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(maxLength: 150),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorumlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YorumText = c.String(nullable: false, maxLength: 300),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                        Kullanici_Id = c.Int(),
                        Makale_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicilar", t => t.Kullanici_Id)
                .ForeignKey("dbo.Makaleler", t => t.Makale_Id, cascadeDelete: true)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Makale_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorumlar", "Makale_Id", "dbo.Makaleler");
            DropForeignKey("dbo.Yorumlar", "Kullanici_Id", "dbo.Kullanicilar");
            DropForeignKey("dbo.Makaleler", "Kullanici_Id", "dbo.Kullanicilar");
            DropForeignKey("dbo.Makaleler", "KategoriId", "dbo.Kategoriler");
            DropForeignKey("dbo.Begeniler", "Makale_Id", "dbo.Makaleler");
            DropForeignKey("dbo.Begeniler", "Kullanici_Id", "dbo.Kullanicilar");
            DropIndex("dbo.Yorumlar", new[] { "Makale_Id" });
            DropIndex("dbo.Yorumlar", new[] { "Kullanici_Id" });
            DropIndex("dbo.Makaleler", new[] { "Kullanici_Id" });
            DropIndex("dbo.Makaleler", new[] { "KategoriId" });
            DropIndex("dbo.Begeniler", new[] { "Makale_Id" });
            DropIndex("dbo.Begeniler", new[] { "Kullanici_Id" });
            DropTable("dbo.Yorumlar");
            DropTable("dbo.Kategoriler");
            DropTable("dbo.Makaleler");
            DropTable("dbo.Kullanicilar");
            DropTable("dbo.Begeniler");
        }
    }
}
