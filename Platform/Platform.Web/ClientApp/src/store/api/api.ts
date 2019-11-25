import axios from 'axios'

export const api = axios.create({
  baseURL: '/api'
})

export function setJWT(jwt: string) {
  api.defaults.headers.common['Authorization'] = `Bearer ${jwt}`
}

export function clearJWT() {
  delete api.defaults.headers.common['Authorization']
}
