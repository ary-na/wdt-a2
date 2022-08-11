using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Model.Models;

// Code sourced and adapted from:
// Week 4 Tutorial - Movie.cs
// https://rmit.instructure.com/courses/102750/files/24425150?wrap=1

// Code sourced and adapted from:
// https://regexr.com
// https://regex101.com/
// https://stackoverflow.com/questions/5034948/regex-for-a-string-of-length-0-2
// https://stackoverflow.com/questions/4816822/int-or-number-datatype-for-dataannotation-validation-attribute

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None), Required]
    [Range(1000, 9999, ErrorMessage = "Customer ID must be 4 digits.")]
    public int CustomerID { get; set; }

    [Required, MaxLength(50)] public string? Name { get; set; }
    
    [StringLength(11), RegularExpression(@"^[1-9]{3}\s[1-9]{3}\s[1-9]{3}$", ErrorMessage = "TFN must be of the format: XXX XXX XXX.")]
    public string? TFN { get; set; }

    [MaxLength(50)] public string? Address { get; set; }

    [MaxLength(40)] public string? City { get; set; }
    
    [MaxLength(3), RegularExpression(@"^[A-Z,a-z]{2,3}$", ErrorMessage = "State must be a 2 or 3 lettered Australian state.")]
    public string? State { get; set; }
    
    [StringLength(4), RegularExpression(@"^\d{4}$", ErrorMessage = "Postcode Must be 4 digits.")]
    public string? Postcode { get; set; }
    
    [StringLength(12), RegularExpression(@"^04[0-9]{2}\s[0-9]{3}\s[0-9]{3}$", ErrorMessage = "Mobile must be of the format: 04XX XXX XXX.")]
    public string? Mobile { get; set; }

    public virtual List<Account>? Accounts { get; set; }
}