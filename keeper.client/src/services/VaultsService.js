import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { keepsService } from "./KeepsService"

class VaultsService {
  async createVault(body) {
    const res = await api.post("api/vaults", body)
    logger.log("creating a vault", res.data)
    AppState.profileVaults.push(res.data)
    AppState.accountVaults.push(res.data)
  }

  async deleteVault(id) {
    const res = await api.delete("api/vaults/" + id)
    logger.log("deleting this vault", res.data)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id != id)
    const findvk = AppState.profileVaultKeeps.filter(p => p.vaultId == id)
    findvk = findvk.forEach(f => f.kept -= 1)
    AppState.accountVaults = AppState.accountVaults.filter(v => v.id != id)
  }

  async getById(id) {
    const res = await api.get("api/vaults/" + id)
    logger.log("this is an active vault", res.data)
    AppState.activeVault = res.data
  }

  async getAccountVaults() {
    const res = await api.get('/account/vaults')
    logger.log("All of my vaults", res.data)
    AppState.accountVaults = res.data
  }

}

export const vaultsService = new VaultsService()