import { Bridge } from "./bridge-manager";

export interface FileBridge extends Bridge {
  // Here you would mirror your C# interface
  getDesktopFiles(): Promise<string[]>;
}

export interface DialogBridge extends Bridge {
  // Here you would mirror your C# interface
  showDialog(title: string, msg: string, showCancel: boolean): Promise<string>;
}

export interface TimeBridge extends Bridge {}
