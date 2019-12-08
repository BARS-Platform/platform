<template>
  <q-select outlined :value="value" :options="options" :label="label" @filter="filterFn" @input="$emit('input', $event)" :disable="disable">
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
import { ListParam } from '../../models/data/listParam'
import { Pagination } from '../../models/data/pagination'
import { getModule } from 'vuex-module-decorators'
import DropdownModule from '@/store/modules/Dropdown'
import { RefModel } from '../../models/refModel'
import { Filtration } from '../../models/data/filtration'

@Component({})
export default class FormDropdownField extends Vue {
  private dropdownStore = getModule(DropdownModule)
  @Prop() label!: string
  @Prop() refModel!: RefModel
  @Prop() value!: string
  @Prop() disable!: Boolean
  @Prop() filters!: Filtration[]
  options: string[] = []

  async filterFn(val: any, update: any, abort: any) {
    let listParam = new ListParam(
      this.refModel.modelName,
      {
        page: 1,
        rowsNumber: 0,
        rowsPerPage: 100
      },
      this.filters
    )
    let res = await this.dropdownStore.getData(listParam)
    update(() => {
      this.options = res.map(x => x[this.refModel.propertyName])
    })
  }
}
</script>