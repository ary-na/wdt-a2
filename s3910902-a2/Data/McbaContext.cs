using Microsoft.EntityFrameworkCore;
using s3910902_a2.Models;

namespace s3910902_a2.Data;

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
    
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Login> Logins { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<BillPay> BillPays { get; set; }
    public virtual DbSet<Payee> Payees { get; set; }
}