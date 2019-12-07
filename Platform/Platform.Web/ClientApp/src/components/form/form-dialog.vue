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
          v-for="field in Fields"
          :key="field.propertyName"
          :is="'q-input'"
          :label="field.label"
          @input="setValues(field.propertyName, $event)"
          :value="getValue(field.propertyName)"
        ></component>
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
import { ListParam } from '../../models/data/listParam'

@Component({})
export default class FormDialog extends Vue {
  private modelStore = getModule(ModelModule)
  @Prop() dialog: Boolean = false
  @Prop() model!: Model
  @Prop() modelName!: string
  modelValues: FormField[] = []
  dto: ModelDto = {
    modelName: ''
  }

  get Fields() {
    let usualFields = this.model.properties.filter(x => x.displayIn.grid === true)
    return usualFields
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
}
</script>