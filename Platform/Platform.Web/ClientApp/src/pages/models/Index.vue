<template>
  <q-page class="flex flex-center">
    {{ Route }}
  </q-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class ModelIndex extends Vue {
  get Route() {
    return this.$router.currentRoute.params
  }
}
</script>

