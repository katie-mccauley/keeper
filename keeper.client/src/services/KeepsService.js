import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {


  async getAll() {
    const res = await api.get('api/keeps')
    logger.log("all keeps post", res.data)
    AppState.keeps = res.data
  }

  async getById(id) {
    const res = await api.get("api/keeps/" + id)
    logger.log("this one keep", res.data)
    AppState.activeKeep = res.data
  }

  async deleteKeep(id) {
    const res = await api.delete("api/keeps/" + id)
    logger.log("delete this ugly post", res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }

  async createKeep(body) {
    const res = await api.post("api/keeps", body)
    logger.log("created this keep", res.data)
    AppState.profileKeeps.push(res.data)
  }
}
export const keepsService = new KeepsService()