import Vue from 'vue'
import VueRouter, { RouterOptions } from 'vue-router'

import routes from './routes'

Vue.use(VueRouter)

const router = new VueRouter({
  scrollBehavior: () => ({ x: 0, y: 0 }),
  routes,

  mode: process.env.VUE_ROUTER_MODE,
  base: process.env.VUE_ROUTER_BASE
} as RouterOptions)

export default router
