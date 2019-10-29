import VueJwtDecode from 'vue-jwt-decode'
import { User } from '@/models/user'

export function parseToken(jwtToken: string): User {
  const emailGetter = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
  const loginGetter = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
  var jwtDecode = VueJwtDecode.decode(jwtToken)
  var user: User = { login: jwtDecode[loginGetter], email: jwtDecode[emailGetter], password: '' }
  return user
}
