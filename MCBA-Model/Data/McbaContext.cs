using MCBA_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Model.Data;

// Code sourced and adapted from:
// Week 7 Lectorial - McbaContext.cs
// https://rmit.instructure.com/courses/102750/files/24426714?wrap=1

public class McbaContext : DbContext
{
    public McbaContext()
    {
    }

    public McbaContext(DbContextOptions<McbaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer>? Customers { get; set; }
    public virtual DbSet<Login>? Logins { get; set; }
    public virtual DbSet<Account>? Accounts { get; set; }
    public virtual DbSet<Transaction>? Transactions { get; set; }
    public virtual DbSet<BillPay>? BillPays { get; set; }
    public virtual DbSet<Payee>? Payees { get; set; }
}