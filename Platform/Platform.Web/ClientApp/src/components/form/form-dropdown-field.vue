<template>
  <q-select
    outlined
    :value="value"
    :options="options"
    :label="label"
    @filter="filterFn"
    @input="$emit('input', $event)"
    :disable="disable"
    :option-label="refModel.propertyName"
    option-value="id"
  >
    <template v-slot:no-option>
      <q-item>
        <q-item-section class="text-grey">
          Данные не найдены
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import DropdownModule from '@/store/modules/Dropdown'

import { ListParam } from '@/models/data/listParam'
import { RefModel } from '@/models/refModel'
import { Property } from '@/models/property'

@Component({})
export default class FormDropdownField extends Vue {
  private dropdownStore = getModule(DropdownModule)
  @Prop() label!: string
  @Prop() refModel!: RefModel
  @Prop() value!: string
  @Prop() disable!: Boolean
  @Prop() field!: Property
  @Prop() createFilters!: Function
  options: any[] = []

  async filterFn(val: any, update: any, abort: any) {
    let filters = this.createFilters(this.field)

    let listParam = new ListParam(
      this.refModel.modelName,
      {
        page: 1,
        rowsNumber: 0,
        rowsPerPage: 0
      },
      filters
    )

    let res = await this.dropdownStore.getData(listParam)
    if (res) {
      update(() => {
        this.options = res
      })
    }
  }
}
</script>