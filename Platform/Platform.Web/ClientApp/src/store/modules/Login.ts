import { User } from '@/models/user'
import * as api from '../api/api'
import * as jwtHelpers from '../helpers/jwtHelpers'
import { Cookies } from 'quasar'
import VueJwtDecode from 'vue-jwt-decode'

import axios from 'axios'
import { VuexModule, Module, MutationAction, Mutation, Action } from 'vuex-module-decorators'
import Store from '@/store/index'

@Module({
  dynamic: true,
  name: 'login',
  namespaced: true,
  store: Store
})
export default class LoginModule extends VuexModule {
  user: User | null = null

  get User() {
    return this.user
  }

  get isAuthenticated() {
    return this.user != null
  }

  @Action
  async authenticate(user: User) {
    let answer = { success: false, message: '', parameterName: '' }
    await axios({ method: 'get', url: `/api/Users/LogIn?login=${user.login}&password=${user.password}` })
      .then(response => {
        this.setUser(response.data.data)
        answer.success = true
      })
      .catch(error => {
        let data = error.response.data
        answer.success = false
        answer.message = data.message
        answer.parameterName = data.parameterName
      })
    return answer
  }

  @MutationAction
  async setUser(token: string) {
    var user = jwtHelpers.parseToken(token)
    api.setJWT(token)
    Cookies.set('authorization_token', token, {
      expires: 1
    })
    return {
      user: user
    }
  }

  @MutationAction
  async authenticateWithToken() {
    if (Cookies.has('authorization_token')) {
      const jwt = Cookies.get('authorization_token')
      api.setJWT(jwt)
      const user = jwtHelpers.parseToken(jwt)
      return { user: user }
    } else {
      return false
    }
  }

  @Action({ commit: 'CLEAR_USER' })
  async logOut() {
    Cookies.remove('authorization_token')
    api.clearJWT()
    return null
  }

  @Mutation
  CLEAR_USER(user: User | null) {
    this.user = user
  }

  @Action
  async register(user: User) {
    await axios({ method: 'post', url: `/api/Users/Register?login=${user.login}&email=${user.email}&password=${user.password}` })
  }

  @Action
  async checkLogin(login: string) {
    const response = await axios({ method: 'get', url: `/api/Users/CheckLoginUsed?login=${login}` })
    return response.data
  }

  @Action
  async checkEmail(email: string) {
    const response = await axios({ method: 'get', url: `/api/Users/CheckEmailUsed?email=${email}` })
    return response.data
  }
}
