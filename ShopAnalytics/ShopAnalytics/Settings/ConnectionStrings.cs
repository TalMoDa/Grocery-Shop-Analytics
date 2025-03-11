using System.ComponentModel.DataAnnotations;

namespace ShopAnalytics.Settings;

public class ConnectionStrings
{
    [Required] public string DefaultConnection { get; set; }
}