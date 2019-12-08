<template>
  <q-dialog v-model="Dialog">
    <q-card class="q-pa-md" style="width:500px">
      <q-card-section>
        <div class="text-h6">{{ model.modelName }}</div>
      </q-card-section>

      <q-card-section>
        <component
          class="q-mt-md"
          outlined
          v-for="(field, index) in Fields"
          :key="field.propertyName"
          :is="getFieldType(field)"
          :label="field.label"
          :refModel="field.refModel"
          @input="setValues(field.propertyName, $event)"
          @dropdownClick="checkFilters($event)"
          :value="getValue(field.propertyName)"
          :disable="isDisabled(index)"
          :field="field"
          :dtoValues="modelValues"
        >
        </component>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn label="Сохранить" color="primary" @click="onSaveClick" v-close-popup />
        <q-btn label="Закрыть" color="primary" @click="modelValues = []" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from 'vue-property-decorator'
import { getModule } from 'vuex-module-decorators'

import { Model } from '@/models/model'
import ModelModule from '@/store/modules/Model'

import { ModelDto } from '@/models/modelDto'
import { FormField } from '@/models/formField'
import FormDropdownField from '@/components/form/form-dropdown-field.vue'
import { ListParam } from '../../models/data/listParam'
import { Filtration } from '../../models/data/filtration'
import { Property } from '@/models/property'

@Component({
  components: {
    FormDropdownField
  }
})
export default class FormDialog extends Vue {
  private modelStore = getModule(ModelModule)
  @Prop() dialog: Boolean = false
  @Prop() model!: Model
  @Prop() modelName!: string
  modelValues: FormField[] = []
  dto: ModelDto = {
    modelName: ''
  }
  filters: Filtration[] = []

  get Fields() {
    return this.model.properties.filter(x => x.displayIn.form === true)
  }

  get Dialog() {
    return this.dialog
  }

  set Dialog(value: Boolean) {
    this.$emit('update:dialog', value)
  }

  setValues(fieldName: string, fieldValue: string) {
    let field = this.modelValues.find(x => x.fieldName == fieldName)
    if (field) {
      field.value = fieldValue
    } else {
      this.modelValues.push({
        fieldName: fieldName,
        value: fieldValue
      })
    }
  }

  getValue(fieldName: string) {
    let field = this.modelValues.find(x => x.fieldName == fieldName)
    if (field) {
      return field.value
    } else {
      return ''
    }
  }

  getFieldType(field: Property) {
    return field.refModel ? 'form-dropdown-field' : 'q-input'
  }

  onSaveClick() {
    this.modelValues.forEach(x => {
      this.dto[x.fieldName] = x.value
    })
    this.dto.modelName = this.modelName
    this.modelStore.createModel(this.dto).then(() => {
      this.modelValues = []
      this.modelStore.getData(this.modelStore.ListResult.listParam)
    })
  }

  isDisabled(index: number) {
    if (index === 0) return false
    return this.getValue(this.Fields[index - 1].propertyName) ? false : true
  }

  checkFilters(field: Property) {
    let fieldIndex = this.Fields.indexOf(field)
    let fields = this.Fields.filter(x => this.Fields.indexOf(x) > fieldIndex)
    if (fields) {
      fields.forEach(x => {
        let val = this.modelValues.find(y => y.fieldName === x.propertyName)
        if (val) {
          val.value = ''
        }
      })
    }
  }
}
</script>