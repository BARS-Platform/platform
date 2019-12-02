import { MenuItem } from '@/models/menuItem'
import * as api from '@/store/api/Menu'
import { Module, VuexModule, MutationAction, Action } from 'vuex-module-decorators'
import Store from '@/store/index'

@Module({
  dynamic: true,
  name: 'menu',
  namespaced: true,
  store: Store
})
export default class MenuModule extends VuexModule {
  public menuItems: MenuItem = {} as MenuItem

  get MenuItems() {
    return this.menuItems
  }

  @Action({ rawError: true })
  async checkAccess(modelName: string) {
    const isAccess = await api.checkAccess(modelName)

    return isAccess
  }

  @MutationAction
  async getMenuItems() {
    const menuItems = await api.getMenuItems()
    return { menuItems: menuItems }
  }
}
