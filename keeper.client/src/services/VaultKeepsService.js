import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsKeepsService {
  async createVaultKeep(body) {
    const res = await api.post("api/vaultkeeps", body)
    logger.log("created vaultkeep", res.data)
    AppState.profileVaultKeeps.push(res.data)
    logger.log("usersvaultskeps", AppState.profileVaultKeeps)
  }

  async getVaultKeeps(id) {
    const res = await api.get("api/vaults/" + id + "/keeps")
    logger.log("all users vks", res.data)
    AppState.profileVaultKeeps = res.data
  }
}

export const vaultKeepsService = new VaultsKeepsService()