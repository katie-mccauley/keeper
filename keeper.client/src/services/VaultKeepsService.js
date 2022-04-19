import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsKeepsService {
  async createVaultKeep(body) {
    const res = await api.post("api/vaultkeeps", body)
    logger.log("created vaultkeep", res.data)
    AppState.profileVaultKeeps.push(res.data)
    AppState.activeKeep.kept = AppState.activeKeep.kept + 1

    logger.log("usersvaultskeps", AppState.profileVaultKeeps)
  }

  async getVaultKeeps(id) {
    const res = await api.get("api/vaults/" + id + "/keeps")
    logger.log("all users vks", res.data)
    AppState.profileVaultKeeps = res.data
  }

  async deleteVk(id) {
    const res = await api.delete("api/vaultkeeps/" + id)
    logger.log("deleting vaultkeeps", res.data)
    // AppState.profileVaultKeeps = AppState.profileVaultKeeps.filter(vk => vk.vaultKeepId != id)
    AppState.activeKeep.kept = AppState.activeKeep.kept - 1
  }
}

export const vaultKeepsService = new VaultsKeepsService()