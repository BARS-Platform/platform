<template>
  <q-select
    filled
    v-model="model"
    use-chips
    label="Lazy load opts"
    :options="options"
    @filter="filterFn"
    @filter-abort="abortFilterFn"
  >
	<template v-slot:no-option>
          <q-item>
            <q-item-section class="text-grey">
              No results
            </q-item-section>
          </q-item>
        </template>
      </q-select>
  </q-select>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from 'vue-property-decorator'

@Component({})
export default class FormDropdownField extends Vue {
  stringOptions: string[] = ['Google', 'Facebook', 'Twitter', 'Apple', 'Oracle']
  model: any = null
  options: any = null

  filterFn(val: any, update: any, abort: any) {
    if (this.options !== null) {
      // already loaded
      update()
      return
    }

    setTimeout(() => {
      update(() => {
        this.options = this.stringOptions
      })
    }, 2000)
  }

  abortFilterFn() {
    // console.log('delayed filter aborted')
  }
}
</script>