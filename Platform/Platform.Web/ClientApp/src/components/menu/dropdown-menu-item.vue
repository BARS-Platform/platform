<template>
  <q-btn-dropdown stretch flat :label="menuItem.title">
    <q-list dense style="min-width: 100px">
      <list-menu-item v-for="children in menuItem.children" :key="children.title" :section="menuItem" :menuItem="children" />
    </q-list>
  </q-btn-dropdown>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { MenuItem } from '@/models/menuItem'

import ListMenuItem from '@/components/menu/list-menu-item.vue'

@Component({
  components: {
    ListMenuItem
  }
})
export default class extends Vue {
  @Prop() readonly menuItem!: MenuItem
}
</script>

<style>
</style>