<template lang="pug">
q-layout(view='lHh Lpr lFf')
  q-header(elevated='')
    q-toolbar
      q-toolbar-title
        | Platform
      div Platform v 0.0.1  
  q-page-container
    router-view
</template>

<script lang="ts">
import { openURL } from 'quasar'
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class MyLayout extends Vue {

}

</script>

<style lang="stylus">
</style>
