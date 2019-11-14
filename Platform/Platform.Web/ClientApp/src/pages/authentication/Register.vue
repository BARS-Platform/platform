<template>
  <q-page padding class="flex flex-center">
    <q-card square style="width: 400px; padding:50px">
      <q-card-section>
        <div class="text-h6">
          Регистрация
        </div>
      </q-card-section>
      <q-card-section>
        <q-form class="q-gutter-md">
          <q-input
            id="login"
            v-model.trim="login"
            type="text"
            label="Логин"
            counter
            debounce="500"
            bottom-slots
            :error="$v.login.$error"
            @blur="$v.login.$touch"
            @input="$v.login.$touch"
            lazy-rules
            autofocus
            :error-message="`${checkLoginError($v.login)}`"
          />
          <q-input
            id="email"
            v-model.trim="email"
            type="email"
            label="Почта"
            debounce="500"
            bottom-slots
            :error="$v.email.$error"
            @blur="$v.email.$touch"
            @input="$v.email.$touch"
            lazy-rules
            :error-message="`${checkEmailError($v.email)}`"
          />
          <q-input
            id="password"
            v-model.trim="password"
            @blur="$v.password.$touch"
            @input="$v.password.$touch"
            :error="$v.password.$error"
            type="password"
            label="Пароль"
            counter
            bottom-slots
            :error-message="`${checkPasswordError($v.password)}`"
          />
          <q-input
            id="repeatPassword"
            v-model.trim="repeatPassword"
            :error="$v.repeatPassword.$error"
            type="password"
            @blur="$v.repeatPassword.$touch"
            @input="$v.repeatPassword.$touch"
            @keyup.enter="register"
            label="Повторите пароль"
            counter
            bottom-slots
            :error-message="`${checkRepeatPasswordError($v.repeatPassword)}`"
          />
        </q-form>
      </q-card-section>
      <q-card-actions class="justify-center">
        <q-btn color="primary" :disabled="$v.$invalid" class="full-width" label="Зарегистрироваться" @click="register" />
        <router-link :to="`/login`" style="cursor: pointer" class="q-mt-lg primary" tag="span">Войти</router-link>
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
//TODO: Refactor
function loginExistRule(login: string) {
  let loginStore = getModule(LoginModule)

  return new Promise((resolve, reject) => {
    loginStore
      .checkLogin(login)
      .then(response => {
        resolve(response.success)
      })
      .catch(error => {
        resolve(false)
      })
  })
}
//TODO: Refactor
function emailExistRule(email: string) {
  let loginStore = getModule(LoginModule)

  return new Promise((resolve, reject) => {
    loginStore
      .checkEmail(email)
      .then(response => {
        resolve(response.success)
      })
      .catch(error => {
        resolve(false)
      })
  })
}

@Component({})
export default class RegisterPage extends Vue {
  public loginStore = getModule(LoginModule)

  @Validate({ required, maxLength: maxLength(15), minLength: minLength(6), loginExistRule })
  login = ''
  @Validate({ required, emailExistRule, email })
  email = ''
  @Validate({ required, minLength: minLength(6) })
  password = ''
  @Validate({ required, sameAs: sameAs('password') })
  repeatPassword = ''

  clearForm() {
    this.$v.$reset()
    this.login = ''
    this.email = ''
    this.password = ''
    this.repeatPassword = ''
  }

  //TODO: Refactor
  checkLoginError(val: any) {
    if (!val.$error) {
      return ''
    }
    if (!val.required) {
      return `Это поле обязательное для заполнения`
    }
    if (val.hasOwnProperty('maxLength') && !val.maxLength) {
      return `Поле должно быть короче ${val.$params.maxLength.max} символов`
    }
    if (val.hasOwnProperty('minLength') && !val.minLength) {
      return `Поле должно быть длиннее ${val.$params.minLength.min} символов`
    }
    if (val.hasOwnProperty('loginExistRule') && !val.loginExistRule) {
      return `Данный логин уже используется`
    }
  }
  //TODO: Refactor
  checkEmailError(val: any) {
    if (!val.$error) {
      return ''
    }
    if (!val.required) {
      return `Это поле обязательное для заполнения`
    }
    if (val.hasOwnProperty('email') && !val.email) {
      return `Введите корректую почту`
    }
    if (val.hasOwnProperty('emailExistRule') && !val.emailExistRule) {
      return `Данная почта уже используется`
    }
  }
  //TODO: Refactor
  checkPasswordError(val: any) {
    if (!val.$error) {
      return ''
    }
    if (!val.required) {
      return `Это поле обязательное для заполнения`
    }
    if (val.hasOwnProperty('minLength') && !val.minLength) {
      return `Поле должно быть длиннее ${val.$params.minLength.min} символов`
    }
  }
  //TODO: Refactor
  checkRepeatPasswordError(val: any) {
    if (!val.$error) {
      return ''
    }
    if (!val.required) {
      return `Это поле обязательное для заполнения`
    }
    if (val.hasOwnProperty('sameAs') && !val.sameAs) {
      return `Пароли должны совпадать`
    }
  }

  register() {
    const user: User = { login: this.login, email: this.email, password: this.password }
    this.$v.$touch()
    if (!this.$v.$invalid) {
      this.$q.loading.show()
      this.loginStore
        .register(user)
        .then(() => {
          this.$q.notify({
            message: 'Пользователь успешно зарегистрирован',
            color: 'positive',
            timeout: 2000
          })
          this.$router.push('/login')
        })
        .catch(() => {
          this.$q.notify({
            message: 'Ошибка при регистрации пользователя',
            color: 'negative',
            timeout: 2000
          })
        })
        .finally(() => {
          this.$q.loading.hide()
          this.clearForm()
        })
    } else {
      this.$q.notify({
        message: 'Введите корректные данные',
        color: 'negative',
        timeout: 2000
      })
    }
  }
}
</script>

