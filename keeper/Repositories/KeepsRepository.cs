using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, shares, kept, creatorId)
      VALUES 
      (@Name, @Description, @Img, @Views, @Shares, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      UPDATE keeps
      SET
      views = views + 1
      WHERE id = @id;
      SELECT 
        k.*,
        p.*
        FROM keeps k
        JOIN accounts p ON p.id = k.creatorId
        WHERE k.id = @id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;

      }, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetAll(string search)
    {
      search = "%" + search + "%";
      string sql = @"
      SELECT
      k.*, 
      p.*
      FROM keeps k
      JOIN accounts p ON p.id = k.creatorId
      WHERE k.name LIKE @search
      ;

      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { search }).ToList();
    }

    internal List<Keep> GetKeepsByUserId(string id)
    {
      string sql = @"
      SELECT 
      k.*, 
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).ToList();
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id LIMIT 1;
      ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "Keep is deleted";
      }
      throw new Exception("there is no rows effected for deleting");
    }

    internal List<VaultViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT 
        k.*, 
        a.*, 
        kv.*
        FROM vaultkeeps kv
        JOIN keeps k ON k.id = kv.keepId
        JOIN accounts a ON a.id = k.creatorId
        WHERE kv.vaultId = @id;
      ";
      List<VaultViewModel> vaults = _db.Query<VaultViewModel, Account, VaultKeep, VaultViewModel>(sql, (viewmodel, a, vaultkeep) =>
      {
        viewmodel.VaultKeepId = vaultkeep.Id;
        viewmodel.Creator = a;
        return viewmodel;
      }, new { id }).ToList<VaultViewModel>();
      return vaults;
    }

    internal Keep Update(Keep original)
    {
      string sql = @"
      UPDATE keeps
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
  }
}