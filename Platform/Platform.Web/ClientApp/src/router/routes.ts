import { RouteConfig } from 'vue-router'
import multiguard from 'vue-router-multiguard'

import auth from './auth'
import permission from './permission'

const routes: Array<RouteConfig> = [
  {
    path: '/',
    component: () => import('@/layouts/Default.vue'),
    children: [
      {
        path: '',
        component: () => import('@/pages/Index.vue'),
        beforeEnter: auth
      },
      {
        path: '/models/:name',
        component: () => import('@/pages/models/Index.vue'),
        name: 'models',
        props: true,
        beforeEnter: multiguard([auth, permission])
      },
      {
        path: '/administration/:name',
        component: () => import('@/pages/models/Index.vue'),
        name: 'administration',
        props: true,
        beforeEnter: multiguard([auth, permission])
      },
      {
        path: '/dictionaries/:name',
        component: () => import('@/pages/models/Index.vue'),
        name: 'dictionaries',
        props: true,
        beforeEnter: multiguard([auth, permission])
      },
      { path: '/register', component: () => import('@/pages/authentication/Register.vue') },
      { path: '/login', component: () => import('@/pages/authentication/Login.vue') }
    ]
  }
]

if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('@/pages/Error404.vue')
  })
}

export default routes
