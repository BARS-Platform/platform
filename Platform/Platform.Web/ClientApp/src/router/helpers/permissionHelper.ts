import { getModule } from 'vuex-module-decorators'
import MenuModule from '@/store/modules/Menu'

export async function Check(param: string) {
  let menuStore = getModule(MenuModule)
  let check = await menuStore.checkAccess(param)
  return check
}
