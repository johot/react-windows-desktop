export default class BridgeManager {
  get<T>(bridgeType: string, automaticallyJsonParse: boolean = false): T {
    let originalBridge = (window as any)[bridgeType] as any;

    if (automaticallyJsonParse) {
      let wrappedBrige: any = {};

      for (var key in originalBridge) {
        if (originalBridge.hasOwnProperty(key)) {
          let v = originalBridge[key];

          if (typeof v === "function") {
            let keyCopy = key;
            let originalFn = originalBridge[keyCopy];
            // Let's rewrite it slightly
            wrappedBrige[keyCopy] = this._parseJsonWrapper(originalFn);
          }
        }
      }
      return wrappedBrige;
    } else {
      return originalBridge;
    }
  }

  _parseJsonWrapper = function(fn: any) {
    return function() {
      let args = arguments;

      const promise = new Promise((resolve, reject) => {
        fn.apply(fn as any, args).then(finalResult => {
          resolve(JSON.parse(finalResult));
        });
      });

      return promise;
    };
  };
}
