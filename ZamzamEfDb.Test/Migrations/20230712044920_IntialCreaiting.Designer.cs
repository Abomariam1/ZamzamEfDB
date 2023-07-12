﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZamzamEfDb.Test.Context;

#nullable disable

namespace ZamzamEfDb.Test.Migrations
{
    [DbContext(typeof(ZamzamDbContext))]
    [Migration("20230712044920_IntialCreaiting")]
    partial class IntialCreaiting
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Zamzam.Core.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Staion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Zamzam.Core.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlackList")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProplem")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("NationalCardId")
                        .HasColumnType("bigint");

                    b.Property<string>("Notes")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Zamzam.Core.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Zamzam.Core.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Zamzam.Core.Installment", b =>
                {
                    b.Property<int>("InstallmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstallmentId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("PayedOn")
                        .HasColumnType("date");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("InstallmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("Zamzam.Core.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("InstallmentPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PurchasingPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("sellingCashPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Zamzam.Core.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMaintained")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastMaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextMaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SaleOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("Zamzam.Core.MaintenanceOrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("MaintenanceOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInstallmented")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

                    b.Property<DateOnly>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal>("Payed")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Remains")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalDiscount")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("Zamzam.Core.PurchaseOrderLine", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[Price] * [Quantity]");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("PurchaseOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnPurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInstallmented")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

                    b.Property<DateOnly>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal>("Payed")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Remains")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalDiscount")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SupplierId");

                    b.ToTable("ReturnPurchaseOrders");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnPurchaseOrderLine", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[Price] * [Quantity]");

                    b.HasKey("OrderId", "ItemId");

                    b.ToTable("ReturnPurchaseOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnSaleOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInstallmented")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("date");

                    b.Property<decimal>("Payed")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Remains")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalDiscount")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ReturnSaleOrders");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnSaleOrderLine", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReturnSaleOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[Price] * [Quantity]");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.HasIndex("ReturnSaleOrderId");

                    b.ToTable("ReturnSaleOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.SaleOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInstallmented")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

                    b.Property<DateOnly>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal>("Payed")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Remains")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalDiscount")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SaleOrders", (string)null);
                });

            modelBuilder.Entity("Zamzam.Core.SaleOrderLine", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SaleOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasComputedColumnSql("[Price] * [Quantity]");

                    b.HasKey("ItemId", "OrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("SaleOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Zamzam.Core.Customer", b =>
                {
                    b.HasOne("Zamzam.Core.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Zamzam.Core.Employee", b =>
                {
                    b.HasOne("Zamzam.Core.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Zamzam.Core.Installment", b =>
                {
                    b.HasOne("Zamzam.Core.Customer", "Customer")
                        .WithMany("Installments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.Employee", "Employee")
                        .WithMany("Installments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", "SalesOrder")
                        .WithMany("Installments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("SalesOrder");
                });

            modelBuilder.Entity("Zamzam.Core.Maintenance", b =>
                {
                    b.HasOne("Zamzam.Core.Customer", null)
                        .WithMany("Maintenances")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.Employee", null)
                        .WithMany("Maintenances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", null)
                        .WithMany("Maintenances")
                        .HasForeignKey("SaleOrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Zamzam.Core.MaintenanceOrderLine", b =>
                {
                    b.HasOne("Zamzam.Core.Item", "Item")
                        .WithMany("MaintenanceOrderLines")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.Maintenance", "Maintenance")
                        .WithMany("MaintenanceOrderLines")
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("Zamzam.Core.PurchaseOrder", b =>
                {
                    b.HasOne("Zamzam.Core.Employee", "Employee")
                        .WithMany("Purchases")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.Supplier", "Supplier")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Zamzam.Core.PurchaseOrderLine", b =>
                {
                    b.HasOne("Zamzam.Core.Item", "Item")
                        .WithMany("PurchaseOrderLines")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnPurchaseOrder", b =>
                {
                    b.HasOne("Zamzam.Core.Employee", "Employee")
                        .WithMany("ReturnPurchases")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.PurchaseOrder", null)
                        .WithOne()
                        .HasForeignKey("Zamzam.Core.ReturnPurchaseOrder", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.Supplier", "Supplier")
                        .WithMany("ReturnPurchases")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnPurchaseOrderLine", b =>
                {
                    b.HasOne("Zamzam.Core.ReturnPurchaseOrder", null)
                        .WithMany("ReturnPurchaseOrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Zamzam.Core.ReturnSaleOrder", b =>
                {
                    b.HasOne("Zamzam.Core.Customer", null)
                        .WithMany("ReturnSaleOrders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Zamzam.Core.Employee", "Employee")
                        .WithMany("ReturnSales")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", "SaleOrder")
                        .WithOne()
                        .HasForeignKey("Zamzam.Core.ReturnSaleOrder", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("SaleOrder");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnSaleOrderLine", b =>
                {
                    b.HasOne("Zamzam.Core.SaleOrderLine", "SaleOrderLine")
                        .WithOne()
                        .HasForeignKey("Zamzam.Core.ReturnSaleOrderLine", "ItemId")
                        .HasPrincipalKey("Zamzam.Core.SaleOrderLine", "ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.ReturnSaleOrder", "ReturnSaleOrder")
                        .WithMany("ReturnSaleOrderLines")
                        .HasForeignKey("ReturnSaleOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReturnSaleOrder");

                    b.Navigation("SaleOrderLine");
                });

            modelBuilder.Entity("Zamzam.Core.SaleOrder", b =>
                {
                    b.HasOne("Zamzam.Core.Customer", null)
                        .WithMany("SaleOrders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Zamzam.Core.Employee", "Employee")
                        .WithMany("Sales")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Zamzam.Core.SaleOrderLine", b =>
                {
                    b.HasOne("Zamzam.Core.Item", "Item")
                        .WithMany("OrderLines")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", "SaleOrder")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Zamzam.Core.SaleOrder", null)
                        .WithMany("SaleOrderLines")
                        .HasForeignKey("SaleOrderId");

                    b.Navigation("Item");

                    b.Navigation("SaleOrder");
                });

            modelBuilder.Entity("Zamzam.Core.Customer", b =>
                {
                    b.Navigation("Installments");

                    b.Navigation("Maintenances");

                    b.Navigation("ReturnSaleOrders");

                    b.Navigation("SaleOrders");
                });

            modelBuilder.Entity("Zamzam.Core.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Zamzam.Core.Employee", b =>
                {
                    b.Navigation("Installments");

                    b.Navigation("Maintenances");

                    b.Navigation("Purchases");

                    b.Navigation("ReturnPurchases");

                    b.Navigation("ReturnSales");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Zamzam.Core.Item", b =>
                {
                    b.Navigation("MaintenanceOrderLines");

                    b.Navigation("OrderLines");

                    b.Navigation("PurchaseOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.Maintenance", b =>
                {
                    b.Navigation("MaintenanceOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnPurchaseOrder", b =>
                {
                    b.Navigation("ReturnPurchaseOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.ReturnSaleOrder", b =>
                {
                    b.Navigation("ReturnSaleOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.SaleOrder", b =>
                {
                    b.Navigation("Installments");

                    b.Navigation("Maintenances");

                    b.Navigation("SaleOrderLines");
                });

            modelBuilder.Entity("Zamzam.Core.Supplier", b =>
                {
                    b.Navigation("PurchaseOrders");

                    b.Navigation("ReturnPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
