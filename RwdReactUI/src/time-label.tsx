import * as React from "react";
import { Button, List } from "semantic-ui-react";
import bridgeManager, { Bridge } from "./bridge-manager";
import { Provider, observer, inject } from "mobx-react";
import { observable, action, computed } from "mobx";
import { TimeBridge } from "./bridges";
import { AppStore } from "./store";

interface TimeLabelProps {
  store?: AppStore;
  timeBridge?: TimeBridge;
}

@inject("store", "timeBridge")
@observer
export class TimeLabel extends React.Component<TimeLabelProps> {
  componentDidMount() {
    // This code could also be put in a non component class, in App etc, up to you
    const timeBridge = this.props.timeBridge as TimeBridge;
    timeBridge.addEventListener("timeUpdated", this._timeUpdated);
  }

  _timeUpdated = arg => {
    (this.props.store as AppStore).time = arg.time;
  };

  render() {
    debugger;
    return (
      <div>
        The time is now: <b>{(this.props.store as AppStore).time}</b>, this is coming from C# 🎉
      </div>
    );
  }
}
