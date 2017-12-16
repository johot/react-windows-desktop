`/* tslint:disable */`;
import * as React from "react";
import "./App.css";
import { Button, List } from "semantic-ui-react";
import bridgeManager, { Bridge } from "./bridge-manager";
import { Provider, observer } from "mobx-react";
import { observable, action, computed } from "mobx";
import { TimeLabel } from "./time-label";
import { DialogBridge, FileBridge, TimeBridge } from "./bridges";
import { AppStore } from "./store";

const logo = require("./logo.svg");

@observer
class App extends React.Component<{}> {
  _fileBridge: FileBridge;
  _dialogBridge: DialogBridge;
  _timeBridge: TimeBridge;

  _store: AppStore = new AppStore();

  constructor(props: {}) {
    super(props);

    this._store = new AppStore();
  }

  componentWillMount() {
    this._loadBridges();
  }

  private async _loadBridges() {
    this._fileBridge = await bridgeManager.getBridge<FileBridge>("fileBridge"); // true means we will automatically json parse all data returned by this class
    this._dialogBridge = await bridgeManager.getBridge<DialogBridge>("dialogBridge");
    this._timeBridge = await bridgeManager.getBridge<TimeBridge>("timeBridge");
    this._fileBridge.addEventListener("testEvent", arg => {
      alert("testEvent, " + arg);
    });

    this._store.initialized = true;
  }

  private _getFiles = async () => {
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
    return this._store.initialized ? (
      <Provider store={this._store} timeBridge={this._timeBridge}>
        <div className="App">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>Welcome to React in Windows!</h2>
          <p className="App-intro">This is a Windows desktop app created in React!</p>
          <p className="App-intro">
            <b>Click the button below to run some C# code 🚀</b>
            <br />
          </p>
          <div style={{ marginBottom: "10px" }}>
            <TimeLabel />
          </div>
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
      </Provider>
    ) : (
      // Show a loading text
      <div style={{ width: "100%", position: "absolute", height: "100%" }}>
        <div style={{ height: "100%", display: "flex", alignItems: "center", justifyContent: "center" }}>Loading...</div>
      </div>
    );
  }
}

export default App;
