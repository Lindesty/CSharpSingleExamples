using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniEFCoreWithSqliteTest.Models;

public class Book
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long BookId { get; set; }
    [StringLength(100)]
    public string BookName { get; set; } = string.Empty;
    [StringLength(100)]
    public string AuthorName { get; set; } = string.Empty;
    [StringLength(100)]
    public string Publisher { get; set; } = string.Empty;
    public long PublicationYearUtcTick { get; set; }
}