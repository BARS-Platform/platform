<template>
  <q-dialog v-model="Dialog" @hide="ModelValues = []">
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
          :refProperty="field.refProperty"
          :mask="field.type === 'integer' ? '###########################' : ''"
          @input="setValues(field, $event)"
          :value="getValue(field.propertyName)"
          :disable="isDisabled(index)"
          :field="field"
          :createFilters="createFilters"
        />
      </q-card-section>

      <q-card-actions align="right">
        <q-btn outline label="Сохранить" color="positive" @click="onSaveClick" :disable="isSaveButtonDisabled()" />
        <q-btn outline label="Закрыть" color="negative" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from 'vue-property-decorator'
import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import * as notify from '@/utils/notify'

import FormDropdownField from '@/components/form/form-dropdown-field.vue'

import { Model } from '@/models/model'
import { ListParam } from '@/models/data/listParam'
import { Filtration } from '@/models/data/filtration'

import { ModelDto } from '@/models/modelDto'
import { Property } from '@/models/property'
import { FormField } from '@/models/formField'

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
  @Prop() modelValues!: FormField[]
  @Prop() isForCreate!: Boolean
  dto: ModelDto = {
    modelName: ''
  }

  get Fields() {
    return this.model.properties.filter(x => x.displayIn.form)
  }

  get ModelValues() {
    return this.modelValues
  }

  set ModelValues(value: FormField[]) {
    this.$emit('update:modelValues', value)
  }

  get Dialog() {
    return this.dialog
  }

  set Dialog(value: Boolean) {
    this.$emit('update:dialog', value)
  }

  setValues(property: Property, fieldValue: any) {
    let field = this.ModelValues.find(x => x.fieldName == property.propertyName)
    if (field) {
      field.value = fieldValue
      if (property.refProperty) {
        let name = field.fieldName
        let property = this.Fields.find(x => x.propertyName === name)
        if (property) {
          let fieldIndex = this.Fields.indexOf(property)
          let fields = this.Fields.filter(x => this.Fields.indexOf(x) > fieldIndex)
          if (fields.length > 0) {
            fields.forEach(x => {
              let val = this.ModelValues.find(y => y.fieldName === x.propertyName)
              if (val) {
                val.value = ''
              }
            })
          }
        }
      }
    } else {
      this.ModelValues.push({
        fieldName: property.propertyName,
        isRefField: property.refProperty ? true : false,
        value: fieldValue
      })
    }
  }

  getValue(fieldName: string) {
    let field = this.ModelValues.find(x => x.fieldName == fieldName)
    return field ? field.value : ''
  }

  getFieldType(field: Property) {
    return field.refProperty ? 'form-dropdown-field' : 'q-input'
  }

  onSaveClick() {
    this.ModelValues.forEach(x => {
      this.dto[x.fieldName] = x.isRefField ? x.value.id : x.value
    })
    this.dto.modelName = this.modelName
    if (this.isForCreate) {
      this.modelStore.createModel(this.dto).then(() => {
        this.afterModelSave('Добавлено')
      })
    } else {
      this.modelStore.updateModel(this.dto).then(() => {
        this.afterModelSave('Изменено')
      })
    }
  }

  afterModelSave(mesage: string) {
    this.ModelValues = []
    this.modelStore.getData(this.modelStore.ListResult.listParam)
    notify.success(mesage)
    this.Dialog = false
  }

  isDisabled(index: number) {
    if (index === 0) return false
    return this.getValue(this.Fields[index - 1].propertyName) ? false : true
  }

  isSaveButtonDisabled() {
    let result = false
    this.Fields.forEach(x => {
      let value = this.ModelValues.find(y => y.fieldName === x.propertyName)
      if (!value || !value.value) {
        result = true
      }
    })

    return result
  }

  createFilters(field: Property) {
    let filters: Filtration[] = []
    let propIndex = this.Fields.indexOf(field)
    let props = this.Fields.filter(x => this.Fields.indexOf(x) < propIndex)

    if (props) {
      props.forEach(x => {
        let value = this.ModelValues.find(y => y.fieldName === x.propertyName)
        if (value) {
          let columnValue = x.refProperty ? value.value.id : value.value
          filters.push({
            columnName: value.fieldName,
            columnValue: columnValue
          })
        }
      })
    }
    return filters
  }
}
</script>
