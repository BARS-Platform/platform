import { Notify } from 'quasar'
import * as permissionHelper from './helpers/permissionHelper'

export default async (to: any, from: any, next: any) => {
  let check = await permissionHelper.Check(to.params.name)
  console.log('test')
  if (check) {
    console.log(check)
    next()
  } else {
    console.log(check)
    next('/')
    Notify.create({
      message: 'Доступ запрещен',
      color: 'negative',
      timeout: 2000
    })
  }
}
