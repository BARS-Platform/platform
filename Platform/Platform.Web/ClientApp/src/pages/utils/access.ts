import * as permissionHelper from '@/router/helpers/permissionHelper'
import * as notify from '@/pages/utils/notify'
import VueRouter from 'vue-router'

export function check(currentParam: string, router: VueRouter) {
  if (!permissionHelper.Check(currentParam)) {
    notify.error('Доступ запрещен')
    router.push('/')
  }
}
