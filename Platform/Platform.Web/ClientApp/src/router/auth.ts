import { getModule } from 'vuex-module-decorators'
import LoginModule from '@/store/modules/Login'

export default (to: any, from: any, next: any) => {
  let loginStore = getModule(LoginModule)
  if (loginStore.isAuthenticated) {
    next()
  } else {
    loginStore.authenticateWithToken().then(() => {
      let user = loginStore.User
      if (user) {
        next()
      } else {
        next('/login')
      }
    })
  }
}
