export interface IModelDto {
  [key: string]: string
  modelName: string
}

export class ModelDto implements IModelDto {
  [key: string]: string
  public modelName: string = ''

  constructor(modelName?: string) {
    this.modelName = modelName || this.modelName
  }
}
