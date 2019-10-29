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
      <q-card-actions>
        <q-btn color="primary" class="full-width" @click="login">
          Авторизоваться
        </q-btn>
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

  login() {
    const user: User = { login: this.form.login, email: '', password: this.form.password }
    this.$q.loading.show()
    this.loginStore
      .authenticate(user)
      .then(response => {
        this.$q.notify({
          message: 'Добро пожаловать',
          color: 'positive',
          timeout: 3000
        })
        this.$router.push('/')
      })
      .catch(response => {
        this.$q.notify({
          message: 'Ошибка при авторизации пользователя',
          color: 'negative',
          timeout: 3000
        })
      })
      .finally(() => {
        this.$q.loading.hide()
        this.clearForm()
      })
  }
}
</script>