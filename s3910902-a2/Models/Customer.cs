using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3910902_a2.Models;

// Code sourced and adapted from:
// Week 4 Tutorial - Movie.cs
// https://rmit.instructure.com/courses/102750/files/24425150?wrap=1

// Code sourced and adapted from:
// https://regexr.com
// https://stackoverflow.com/questions/4816822/int-or-number-datatype-for-dataannotation-validation-attribute

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None), Required]
    [Range(1000, 9999, ErrorMessage = "Customer ID must be 4 digits.")]
    public int CustomerID { get; set; }

    [Required, StringLength(50)] public string? Name { get; set; }

    [StringLength(11), RegularExpression(@"^[1-9]{3}\\s[1-9]{3}\\s[1-9]{3}$")]
    public string? TFN { get; set; }

    [StringLength(50)] public string? Address { get; set; }

    [StringLength(40)] public string? City { get; set; }

    [StringLength(3)] public string? State { get; set; }

    [StringLength(4)] public string? PostCode { get; set; }

    [StringLength(12), RegularExpression(@"^04[0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$")]
    public string? Mobile { get; set; }

    public virtual List<Account>? Accounts { get; set; }

    public virtual Login? Login { get; set; }
}