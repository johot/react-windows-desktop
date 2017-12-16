import { observable } from "mobx";

export class AppStore {
  @observable time: string;
  @observable initialized: boolean;
  @observable files: string[] = [];
}
