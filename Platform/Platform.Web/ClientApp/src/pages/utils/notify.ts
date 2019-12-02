import { Notify } from 'quasar'

export function error(message: string) {
  Notify.create({
    message: message,
    color: 'negative',
    timeout: 2000
  })
}

export function success(message: string) {
  Notify.create({
    message: message,
    color: 'positive',
    timeout: 2000
  })
}
