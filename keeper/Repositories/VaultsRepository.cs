using System;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;
using Microsoft.AspNetCore.Mvc;

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

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
        v.*, 
        p.*
        FROM vaults v
        JOIN accounts p ON p.id = v.creatorId
        WHERE v.id = @id;
      ";
      return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal Vault Update(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name, 
      description = @Description, 
      img = @Img
      WHERE id = @Id;
      ";
      int rows = _db.Execute(sql, original);
      if (rows > 0)
      {
        return original;

      }
      throw new Exception("there is no rows effected for editing");
    }

    internal ActionResult<string> Remove(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id LIMIT 1;
      ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "Keep is deleted";
      }
      throw new Exception("there is no rows effected for deleting");
    }
  }
}