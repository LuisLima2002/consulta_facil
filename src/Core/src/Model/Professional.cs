
namespace VozAmiga.Core.Data.Model;

public class Profissional
{
    public Profissional(Guid id){
        Id = id;
    }
    public Profissional(){ }

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Phone { get; set; }
    public required string JobPosition { get; set; }
    public required string Permission { get; set; }
    public string? Salt { get; set; }
}
