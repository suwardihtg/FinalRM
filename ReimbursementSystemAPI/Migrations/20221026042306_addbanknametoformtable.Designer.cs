﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReimbursementSystemAPI.Models;

namespace ReimbursementSystemAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20221026042306_addbanknametoformtable")]
    partial class addbanknametoformtable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Account", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departements");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int?>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Salary")
                        .HasColumnType("real");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("JobId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee_Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employee_Attachments");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentFinance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentManager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SubmittedDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.HasKey("ExpenseId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.ExpenseHistory", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseHistories");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Form", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("Attachments")
                        .HasColumnType("int");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.HasKey("FormId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Account", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Employee", "Employee")
                        .WithOne("Accounts")
                        .HasForeignKey("ReimbursementSystemAPI.Models.Account", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Department", "Departments")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("ReimbursementSystemAPI.Models.Job", "Jobs")
                        .WithMany("Employees")
                        .HasForeignKey("JobId");

                    b.Navigation("Departments");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Employee", "Employees")
                        .WithMany("Expenses")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.ExpenseHistory", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Expense", "Expenses")
                        .WithMany("ExpenseHistories")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Form", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Expense", "Expenses")
                        .WithMany("Forms")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.Navigation("ExpenseHistories");

                    b.Navigation("Forms");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Job", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
