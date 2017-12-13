`/* tslint:disable */`;
import * as React from "react";
import "./App.css";
import { Button, List } from "semantic-ui-react";
import BridgeManager from "./bridge-manager";
import { Provider, observer } from "mobx-react";
import { observable, action, computed } from "mobx";

const logo = require("./logo.svg");

class AppStore {
  @observable files: string[] = [];
}

interface FileBridge {
  // Here you would mirror your C# interface
  getDesktopFiles(): Promise<string[]>;
}

interface DialogBridge {
  // Here you would mirror your C# interface
  showDialog(title: string, msg: string, showCancel: boolean): Promise<string>;
}

@observer
class App extends React.Component<{}> {
  _fileBridge: FileBridge;
  _dialogBridge: DialogBridge;
  _store: AppStore = new AppStore();

  constructor(props: {}) {
    super(props);

    this._store = new AppStore();
    this._fileBridge = new BridgeManager().get<FileBridge>("FileBridge", true); // true means we will automatically json parse all data returned by this class
    this._dialogBridge = new BridgeManager().get<DialogBridge>("DialogBridge");
  }

  _getFiles = async () => {
    // Note: This could also be moved into the mobx store as an @action if you want to
    let result = await this._fileBridge.getDesktopFiles();
    this._store.files = result;

    // Show a real Windows dialog!
    const dialogResult = await this._dialogBridge.showDialog(
      "Found some files!",
      "I found " + this._store.files.length + " files, and look i'm showing this in a native Windows dialog 😊! Click any button below.",
      true
    );

    await this._dialogBridge.showDialog("Click!", "You clicked: " + dialogResult, false);
  };

  render() {
    return (
      <div className="App">
        <img src={logo} className="App-logo" alt="logo" />
        <h2>Welcome to React in Windows!</h2>
        <p className="App-intro">This is a Windows desktop app created in React!</p>
        <p className="App-intro">
          <b>Click the button below to run some C# code 🚀</b>
          <br />
        </p>
        <Button onClick={() => this._getFiles()}>List some files...</Button>
        <List divided relaxed style={{ margin: 50 }}>
          {this._store.files.map((item, index) => {
            return (
              <List.Item key={index}>
                <List.Icon name="file" size="large" verticalAlign="middle" />
                <List.Content>
                  <List.Header>{item}</List.Header>
                </List.Content>
              </List.Item>
            );
          })}
        </List>
      </div>
    );
  }
}

export default App;
