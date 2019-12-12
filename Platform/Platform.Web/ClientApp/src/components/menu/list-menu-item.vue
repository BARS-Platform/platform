<template>
  <q-item clickable v-ripple :to="`${section.link}${menuItem.link}`" exact style="min-height: 50px">
    <q-item-section avatar>
      <q-icon color="primary" :name="menuItem.icon" />
    </q-item-section>
    <q-item-section>{{ menuItem.title }}</q-item-section>
  </q-item>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { MenuItem } from '@/models/menuItem'

@Component({})
export default class ListMenuItem extends Vue {
  @Prop() readonly section!: MenuItem
  @Prop() readonly menuItem!: MenuItem
}
</script>

<style>
</style>