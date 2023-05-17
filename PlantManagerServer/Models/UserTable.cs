using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantManagerServer.Models;

[Table("user_table", Schema = "public")]
public class UserTable
{
    [Column("user_id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserId { get; set; }
    [Column("email")]
    public string Email { get; set; } = "";
    [Column("user_name")]
    public string UserName { get; set; } = "";
    [Column("password")]
    public string Password { get; set; } = "";
}