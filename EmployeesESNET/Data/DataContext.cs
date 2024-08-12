﻿using System;
using System.Collections.Generic;
using EmployeesESNET.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesESNET.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentEmployee> DepartmentEmployees { get; set; }

    public virtual DbSet<DepartmentManager> DepartmentManagers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=14.225.207.73;Port=5432;Database=employees;User Id=anhdev99;Password=anhdev992024;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("employees", "employee_gender", new[] { "M", "F" });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("idx_16979_primary");

            entity.ToTable("department", "employees");

            entity.HasIndex(e => e.DeptName, "idx_16979_dept_name").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.DeptName)
                .HasMaxLength(40)
                .HasColumnName("dept_name");
        });

        modelBuilder.Entity<DepartmentEmployee>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.DepartmentId }).HasName("idx_16982_primary");

            entity.ToTable("department_employee", "employees");

            entity.HasIndex(e => e.DepartmentId, "idx_16982_dept_no");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("department_id");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentEmployees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("dept_emp_ibfk_2");

            entity.HasOne(d => d.Employee).WithMany(p => p.DepartmentEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("dept_emp_ibfk_1");
        });

        modelBuilder.Entity<DepartmentManager>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.DepartmentId }).HasName("idx_16985_primary");

            entity.ToTable("department_manager", "employees");

            entity.HasIndex(e => e.DepartmentId, "idx_16985_dept_no");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("department_id");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentManagers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("dept_manager_ibfk_2");

            entity.HasOne(d => d.Employee).WithMany(p => p.DepartmentManagers)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("dept_manager_ibfk_1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("idx_16988_primary");

            entity.ToTable("employee", "employees");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('employees.id_employee_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(14)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate }).HasName("idx_16991_primary");

            entity.ToTable("salary", "employees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("salaries_ibfk_1");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Title1, e.FromDate }).HasName("idx_16994_primary");

            entity.ToTable("title", "employees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Title1)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.Titles)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("titles_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
