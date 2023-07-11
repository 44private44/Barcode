using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Barcoad_Entities.DataModels;

public partial class ImagesdbContext : DbContext
{
    public ImagesdbContext()
    {
    }

    public ImagesdbContext(DbContextOptions<ImagesdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BirthdayUser> BirthdayUsers { get; set; }

    public virtual DbSet<FriendsDatum> FriendsData { get; set; }

    public virtual DbSet<ImgRecord> ImgRecords { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PCA172\\SQL2017;Database=imagesdb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=Tatva@123;Integrated Security=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BirthdayUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__birthday__206A9DF8C5287B49");

            entity.ToTable("birthday_user");

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<FriendsDatum>(entity =>
        {
            entity.HasKey(e => e.FriendId).HasName("PK__Friends___3FA1E155827D0BAD");

            entity.ToTable("Friends_Data");

            entity.Property(e => e.FriendId).HasColumnName("friend_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<ImgRecord>(entity =>
        {
            entity.HasKey(e => e.Rno).HasName("PK__imgRecor__C2B7F59BA7656BBF");

            entity.ToTable("imgRecords");

            entity.Property(e => e.Rno).HasColumnName("rno");
            entity.Property(e => e.Path)
                .HasColumnType("text")
                .HasColumnName("path");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("((0))")
                .HasColumnName("rating");
            entity.Property(e => e.Userno).HasColumnName("userno");
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userData__B9BE370FE61B6211");

            entity.ToTable("userData", tb =>
                {
                    tb.HasTrigger("deleteData");
                    tb.HasTrigger("insertData");
                });

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ExpirationTime).HasColumnType("datetime");
            entity.Property(e => e.RequestToken).HasColumnName("Request_token");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(200)
                .HasColumnName("user_password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
