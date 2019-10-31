<template lang="pug">
q-layout(view='lHh Lpr lFf')
  q-header(elevated='')
    q-toolbar
      q-toolbar-title Platform
      q-btn(stretch, flat, v-if="!isAuthenticated", label='Войти', to="/login")
      q-btn(stretch, flat, v-else, label='Выйти', @click="logOut")
  q-page-container
    router-view
</template>

<script lang="ts">
import { openURL } from 'quasar'
import { Component, Vue } from 'vue-property-decorator'

import { getModule, Action } from 'vuex-module-decorators'
import LoginModule from '@/store/modules/Login'

@Component({})
export default class MyLayout extends Vue {
  public loginStore = getModule(LoginModule)

  get isAuthenticated() {
    return this.loginStore.isAuthenticated
  }

  logOut() {
    this.loginStore.logOut()
    this.$router.push('/login')
  }
}
</script>

<style lang="stylus">
</style>
