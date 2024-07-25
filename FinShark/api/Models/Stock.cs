using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Stock
    {
        public int Id {get; set;}
        public string Symbol {get; set;} = string.Empty;
        public string CompanyName {get; set;} = string.Empty;

        // Column: specify the properties of the column that will be created in the database
        // TypeName: defines the exact data type of the column in the database.
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase {get; set;}
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv {get; set;}
        public string Industry {get; set;} = string.Empty;

        // can be up to trillion dollar => use long
        public long MarketCap {get; set;}

        // Collection navigation containing dependents
        public List<Comment> Comments {get; set;} = new List<Comment>();
    }
}