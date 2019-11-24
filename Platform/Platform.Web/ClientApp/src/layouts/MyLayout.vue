<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated="">
      <q-toolbar>
        <router-link to="/" tag="q-toolbar-title" style="cursor: pointer">Platform</router-link>
        <q-btn-dropdown
          v-for="menuItem in menuItems"
          :key="menuItems.title"
          stretch="stretch"
          flat="flat"
          :label="menuItem.title"
          v-if="isAuthenticated"
        >
          <q-list dense style="min-width: 100px">
            <q-item
              clickable
              v-for="children in menuItem.children"
              :key="children.title"
              clickable
              v-ripple
              :to="`${menuItem.link}${children.link}`"
              exact
              style="min-height: 50px"
            >
              <q-item-section avatar>
                <q-icon color="primary" :name="children.icon" />
              </q-item-section>
              <q-item-section>{{ children.title }}</q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
        <q-btn stretch="stretch" flat="flat" v-if="!isAuthenticated" label="Войти" to="/login" />
        <q-btn stretch="stretch" flat="flat" v-else="v-else" label="Выйти" @click="logOut" />
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

@Component({})
export default class MyLayout extends Vue {
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
