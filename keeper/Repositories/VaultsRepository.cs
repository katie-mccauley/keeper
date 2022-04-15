using System.Data;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults 
      (name, description, img, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @Img, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }
  }
}