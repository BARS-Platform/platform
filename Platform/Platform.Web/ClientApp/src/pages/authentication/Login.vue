<template>
  <q-page padding class="flex flex-center">
    <q-card square style="width: 400px; padding:50px">
      <q-card-section>
        <div class="text-h6">
          Авторизация
        </div>
      </q-card-section>
      <q-card-section>
        <q-input id="login" v-model.trim="form.login" type="text" label="Логин" bottom-slots required autofocus />
        <q-input id="password" v-model="form.password" type="password" label="Пароль" bottom-slots required />
      </q-card-section>
      <q-card-actions class="justify-center">
        <q-btn color="primary" class="full-width" @click="login">
          Авторизоваться
        </q-btn>
        <router-link :to="`/register`" style="cursor: pointer" class="q-mt-lg primary" tag="span">Регистрация</router-link>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

import { getModule, Action } from 'vuex-module-decorators'
import LoginModule from '@/store/modules/Login'

import { User } from '@/models/user'

@Component
export default class RegisterPage extends Vue {
  public loginStore = getModule(LoginModule)

  public form = {
    login: '',
    password: ''
  }

  clearForm() {
    this.form = {
      login: '',
      password: ''
    }
  }

  async login() {
    const user: User = { login: this.form.login, email: '', password: this.form.password }
    this.$q.loading.show()
    let response = await this.loginStore.authenticate(user)

    if (response.success) {
      this.$q.notify({
        message: 'Добро пожаловать',
        color: 'positive',
        timeout: 3000
      })
      this.$router.push('/')
    } else {
      this.$q.notify({
        message: response.message,
        color: 'negative',
        timeout: 3000
      })
    }
    this.$q.loading.hide()
    this.clearForm()
  }
}
</script>