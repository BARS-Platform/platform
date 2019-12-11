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
          :refModel="field.refModel"
          :mask="field.type === 'integer' ? '###########################' : ''"
          @input="setValues(field, $event)"
          :value="getValue(field.propertyName)"
          :disable="isDisabled(index)"
          :field="field"
          :createFilters="createFilters"
        >
        </component>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn outline label="Сохранить" color="positive" @click="onSaveClick" :disable="isSaveEnabled()" />
        <q-btn outline label="Закрыть" color="negative" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from 'vue-property-decorator'
import { getModule } from 'vuex-module-decorators'
import * as notify from '@/utils/notify'

import { Model } from '@/models/model'
import ModelModule from '@/store/modules/Model'

import { ModelDto } from '@/models/modelDto'
import { FormField } from '@/models/formField'
import FormDropdownField from '@/components/form/form-dropdown-field.vue'
import { ListParam } from '@/models/data/listParam'
import { Filtration } from '@/models/data/filtration'
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
  @Prop() modelValues!: FormField[]
  @Prop() isForCreate!: Boolean
  dto: ModelDto = {
    modelName: ''
  }

  get Fields() {
    return this.model.properties.filter(x => x.displayIn.form === true)
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
      if (property.refModel) {
        let name = field.fieldName
        let property = this.Fields.find(x => x.propertyName === name)
        if (property) {
          let fieldIndex = this.Fields.indexOf(property)
          let fields = this.Fields.filter(x => this.Fields.indexOf(x) > fieldIndex)
          if (fields) {
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
        isRefField: property.refModel ? true : false,
        value: fieldValue
      })
    }
  }

  getValue(fieldName: string) {
    let field = this.ModelValues.find(x => x.fieldName == fieldName)
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
    this.ModelValues.forEach(x => {
      this.dto[x.fieldName] = x.isRefField ? x.value.id : x.value
    })
    this.dto.modelName = this.modelName
    if (this.isForCreate) {
      this.modelStore.createModel(this.dto).then(() => {
        this.ModelValues = []
        this.modelStore.getData(this.modelStore.ListResult.listParam)
        notify.success('Добавлено')
        this.Dialog = false
      })
    } else {
      this.modelStore.updateModel(this.dto).then(() => {
        this.ModelValues = []
        this.modelStore.getData(this.modelStore.ListResult.listParam)
        notify.success('Изменено')
        this.Dialog = false
      })
    }
  }

  isDisabled(index: number) {
    if (index === 0) return false
    return this.getValue(this.Fields[index - 1].propertyName) ? false : true
  }

  isSaveEnabled() {
    let result = false
    this.Fields.forEach(x => {
      let value = this.ModelValues.find(y => y.fieldName === x.propertyName)
      if (value) {
        if (!value.value) {
          result = true
        }
      } else {
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
        if (value && x.refModel) {
          filters.push({
            columnName: value.fieldName,
            columnValue: value.value.id
          })
        } else if (value) {
          filters.push({
            columnName: value.fieldName,
            columnValue: value.value
          })
        }
      })
    }
    return filters
  }
}
</script>