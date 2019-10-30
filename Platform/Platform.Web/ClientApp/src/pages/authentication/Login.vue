<template>
  <q-page padding class="flex flex-center">
    <q-card square style="width: 400px; padding:50px">
      <q-card-section>
        <div class="text-h6">
          Вход
        </div>
      </q-card-section>
      <q-card-section>
        <q-input
          id="login"
          v-model.trim="login"
          type="text"
          label="Логин"
          @blur="$v.login.$touch"
          @input="$v.login.$touch"
          bottom-slots
          required
          autofocus
        />
        <q-input
          id="password"
          v-model="password"
          type="password"
          @blur="$v.password.$touch"
          @input="$v.password.$touch"
          label="Пароль"
          bottom-slots
          required
        />
      </q-card-section>
      <q-card-actions class="justify-center">
        <q-btn color="primary" :disabled="$v.$invalid" class="full-width" label="Войти" @click="signIn" />
        <router-link :to="`/register`" style="cursor: pointer" class="q-mt-lg primary" tag="span">Регистрация</router-link>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

import { getModule, Action } from 'vuex-module-decorators'
import LoginModule from '@/store/modules/Login'

import { Validate } from 'vuelidate-property-decorators'
import { required, minLength, maxLength, email, sameAs } from 'vuelidate/lib/validators'

import { User } from '@/models/user'

@Component
export default class RegisterPage extends Vue {
  public loginStore = getModule(LoginModule)

  @Validate({ required })
  login = ''

  @Validate({ required })
  password = ''

  clearForm() {
    this.$v.$reset()
    this.login = ''
    this.password = ''
  }

  async signIn() {
    const user: User = { login: this.login, email: '', password: this.password }
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