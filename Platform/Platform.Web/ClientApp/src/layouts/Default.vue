<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated="">
      <q-toolbar>
        <router-link to="/" tag="q-toolbar-title" style="cursor: pointer">Platform</router-link>
        <dropdown-menu-item v-for="menuItem in menuItems" :key="menuItems.title" v-if="isAuthenticated" :menuItem="menuItem" />
        <q-btn stretch flat v-if="!isAuthenticated" label="Войти" to="/login" />
        <q-btn stretch flat v-else="v-else" label="Выйти" @click="logOut" />
      </q-toolbar>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { openURL } from 'quasar'
import { Component, Vue } from 'vue-property-decorator'

import { getModule, Action } from 'vuex-module-decorators'
import LoginModule from '@/store/modules/Login'
import MenuModule from '@/store/modules/Menu'

import DropdownMenuItem from '@/components/menu/dropdown-menu-item.vue'

@Component({
  components: {
    DropdownMenuItem
  }
})
export default class DefaultLayout extends Vue {
  public loginStore = getModule(LoginModule)
  public menuStore = getModule(MenuModule)

  get isAuthenticated() {
    return this.loginStore.IsAuthenticated
  }

  get menuItems() {
    return this.menuStore.MenuItems.children
  }

  logOut() {
    this.loginStore.logOut()
    this.$router.push('/login')
  }

  mounted() {
    this.$store.dispatch('menu/getMenuItems')
  }
}
</script>

<style lang="stylus"></style>
