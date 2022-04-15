using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keeper.Models
{
    public class Vault
    {
        public int Id { get; set; } 
        public string CreatorId { get; set; }
        public bool IsPrivate { get; set; }
        public Account Creator { get; set; }
    }
    
    public class VaultKeeperViewModel : Vault
    {
        public int VaultKeeperId { get; set; }
    }
}