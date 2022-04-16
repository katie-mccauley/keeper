import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async createVault(body) {
    const res = await api.post("api/vaults", body)
    logger.log("creating a vault", res.data)
    AppState.profileVaults.push(res.data)
  }

  async deleteVault(id) {
    const res = await api.delete("api/vaults/" + id)
    logger.log("deleting this vault", res.data)
    AppState.profileVaults.filter(v => v.id != id)
  }

}

export const vaultsService = new VaultsService()