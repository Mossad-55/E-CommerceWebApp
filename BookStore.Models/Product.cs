using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    [Range(1, 5000)]
    [Display(Name = "List Price")]
    public double ListPrice { get; set; }
    [Required]
    [Range(1, 5000)]
    public double Price { get; set; }
    [Required]
    [Range(1, 5000)]
    [Display(Name = "Price For 50")]
    public double Price50 { get; set; }
    [Required]
    [Range(1, 5000)]
    [Display(Name = "Price For 100")]
    public double Price100 { get; set; }
    [Display(Name ="Image")]
    [ValidateNever]
    public string ImageUrl { get; set; }

    [Required]
    [Display(Name = "Caregory")]
    public int CategoryId { get; set; } //FK

    [ValidateNever]
    public Category Category { get; set; }

    [Required]
    [Display(Name = "Cover Type")]
    public int CoverTypeId { get; set; }
    [ValidateNever]
    public CoverType CoverType { get; set; }


}
