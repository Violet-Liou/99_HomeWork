using Microsoft.EntityFrameworkCore;

namespace Homework_1.Models
{
    public class MessageBoardContext : DbContext //須繼承DbContext類別
    {
        //撰寫依賴注入用的建構子
        public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
           : base(options)
        {
        }

        public virtual DbSet<MainContent> MainContent { get; set; } //MainContent資料表
        public virtual DbSet<Response> Response { get; set; } //Response資料表

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //描述資料庫裡面的資料表
            modelBuilder.Entity<MainContent>(entity =>
            {
                entity.HasKey(m => m.MainID).HasName("PK_MainID"); //設定主鍵

                entity.Property(m => m.MainID)
                .HasMaxLength(36)  //BookID長度為36
                .IsUnicode(false); //不使用Unicode編碼 

                entity.Property(m => m.MTitle)
               .HasMaxLength(30);

                entity.Property(m => m.MContent)
               .HasColumnType("nvarchar(max)");

                entity.Property(m => m.MPhoto)
               .HasMaxLength(40);

                entity.Property(m => m.MPhotoType)
               .HasMaxLength(30);

                entity.Property(m => m.NAuthor)
              .HasMaxLength(20);

                entity.Property(m => m.CreatedDate)
                    .HasColumnType("datetime"); // 設定為datetime型別
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(r => r.ResponseID); //設定主鍵

                entity.Property(r => r.ResponseID)
                .HasMaxLength(36)  //長度為36
                .IsUnicode(false);  //ReBookID預設值為GUID

                entity.Property(r => r.RContent)
                .HasColumnType("nvarchar(max)");

                entity.Property(r => r.RAuthor)
                .HasMaxLength(20);

                entity.Property(r => r.CreatedDate)
                    .HasColumnType("datetime"); // 設定為datetime型別

            });
        }

    }
}
