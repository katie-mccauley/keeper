import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {

  async getUser(id) {
    const res = await api.get("api/profiles/" + id)
    logger.log("get the user", res.data)
    AppState.profile = res.data
  }

  async getUserKeeps(id) {
    const res = await api.get("api/profiles/" + id + "/keeps")
    logger.log("users keeps", res.data)
    AppState.profileKeeps = res.data
  }

  async getUserVaults(id) {
    const res = await api.get("api/profiles/" + id + "/keeps")
    logger.log("users vaults", res.data)
    AppState.profileVaults = res.data
  }

}

export const profilesService = new ProfilesService()