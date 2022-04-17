using System.Data;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      UPDATE keeps
      SET
      kept = kept + 1
      WHERE id = @KeepId;
      INSERT INTO vaultkeeps
      (keepId, vaultId, creatorId)
      VALUES 
      (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
      string sql = @"
      SELECT * FROM vaultkeeps WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE 
      FROM vaultkeeps 
      WHERE id = @id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}