namespace keeper.Models
{
  public class Vault
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public Profile Creator { get; set; }
  }

  public class VaultViewModel : Vault
  {
    public int VaultKeepId { get; set; }
  }
}