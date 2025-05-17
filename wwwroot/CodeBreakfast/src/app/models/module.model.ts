export interface ModuleAdd {
  name: string;
}
export interface ModuleUpdate extends ModuleAdd {
  id: string;
}
export interface ModuleDetail extends ModuleUpdate {

}
