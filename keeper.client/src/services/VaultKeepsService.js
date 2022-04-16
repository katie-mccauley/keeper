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
}

export const vaultKeepsService = new VaultsKeepsService()