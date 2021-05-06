using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI_Form.Models
{
    public partial class SofTrust_dbContext : DbContext
    {
        public SofTrust_dbContext()
        {
        }

        public SofTrust_dbContext(DbContextOptions<SofTrust_dbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<AllMessage> AllMessages { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=YURY-OKHRIMENKO\\SQLEXPRESS;Database=SofTrust_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AllMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__allMessa__0BBF6EE6C01B11E9");

                entity.ToTable("allMessages");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.MessageText)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("message_text");

                entity.Property(e => e.RfContactId).HasColumnName("rf_contact_id");

                entity.Property(e => e.RfTopicId).HasColumnName("rf_topic_id");

                entity.HasOne(d => d.RfContact)
                    .WithMany(p => p.AllMessages)
                    .HasForeignKey(d => d.RfContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_allMessages_contacts");

                entity.HasOne(d => d.RfTopic)
                    .WithMany(p => p.AllMessages)
                    .HasForeignKey(d => d.RfTopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_allMessages_topics");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contacts");

                entity.Property(e => e.ContactId).HasColumnName("contact_id");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contact_email");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contact_phone");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topics");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("topic_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
