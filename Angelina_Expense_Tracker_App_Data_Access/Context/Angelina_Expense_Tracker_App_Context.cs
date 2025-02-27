using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Angelina_Expense_Tracker_App_Data_Access.Models;


namespace Angelina_Expense_Tracker_App_Data_Access.Context
{
    public partial class Angelina_Expense_Tracker_App_Context : DbContext
    {
        protected Angelina_Expense_Tracker_App_Context()
        {

        }

        public Angelina_Expense_Tracker_App_Context(DbContextOptions options) : base(options)
        {
        }
       

        public virtual DbSet<Models.ExpenseModel> Expense { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseModel>(entity =>
            {
                entity.HasKey(e => e.ExpenseID);

                entity.Property(e => e.ExpenseID).HasColumnName("ExpenseID");

                // entity.Property(e => e.ExpenseAccountBalance).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Category);

                entity.Property(e => e.Amount).HasColumnType("decimal(14, 2)")
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.Date).IsRequired(required: false);

                entity.Property(e => e.Notes);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
