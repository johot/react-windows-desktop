# React Windows Desktop

**Create native Windows applications using React + TypeScript for the UI and C# .NET for native things (file system, OS access etc)!**

## How does it work?

* You build the UI of your app just like building a regular React webpage (this project is even setup using `create-react-app` with the Microsoft TypeScript starter).

* Your UI is hosted in a native Windows (WinForms) app using CefSharp (a Chromium wrapper for .NET).

  > **Why not WPF?**  
  > When using CefSharp WinForms is a lot faster than WPF.

* To run C# code for access to the native features like the file system, OS etc you create a Bridge class that get's called from TypeScript, but runs C# in the Windows app (outside the web sandbox). It's really simple!

### What's the difference between this and Electron?

Basically that it uses .NET and C# for the bridge instead of Node.js (JavaScript). Also this is still a pretty small project and you have to build the bridge classes yourselves (in Electron a file system API is available right away and so on).

However this is the perfect project for those of us coming from a WinForms, WPF or just .NET backend background. It's also perfect if you already have an existing C# library that you want to use in your app.

### Preloaded libraries

To make it really fast and easy getting your started the react project comes preloaded with these great libraries:

* [TypeScript](https://www.typescriptlang.org/) for a better developer experience especially together with Visual Studio Code.
* [MobX](https://mobx.js.org/) for state management.
* [Semantic-UI React](https://react.semantic-ui.com/introduction) for some great looking React components like buttons, lists etc.

> Of course you can easily remove these and use something else like Redux, Bootstrap and so on.

## Launching

* For fist time use: In the `RwdReactUI` folder do a `npm install`.

* In the `RwdReactUI` do a `npm start` to load the UI and make it available for live reload.

* Launch the Windows app (`RwdApp` folder) solution in Visual Studio in `Debug`+`x64` mode.
  > **Very important!** You must launch using the `x64` mode. Using `Any CPU` will cause CefSharp to not launch.

The app should now show the UI you built using React!

> You are free to rename these projects as you see fit.

## Editing

* All native / C# code goes into the `RwdApp` solution (use Visual Studio).

* All UI code goes into the `RwdReactUI` project. Use you favorite editor (we use VS Code).

### TypeScript <-> C# Bridges

A Bridge class is a regular .NET C# Class. From TypeScript you will be able to invoke methods on these classes and get back results. While the default CefSharp solution only allows simple data types to be returned (`string`, `int`, `boolean` etc) we have built our own layer on top of this that will **automatically** serialize complex objects into Json! So no more limitations to how advanced data your bridge classes can return, it just works.

We have also built an event system that makes it really easy to run TypeScript code when an event is fired from the C# side.

You can see some sample bridges in the `Bridges` folder in the `RwdApp` solution.

> **Important limitations:**
>
> On the C# side your bridge classes can currently **not** use `async` methods. It will be run in it's own thread by CefSharp though so the GUI should not lock up.

#### Registering a new Bridge class

Edit the `Bridges.cs` file in the root of the `RwdApp` project.

#### Calling a bridge class from TypeScript and return values

```ts
import bridgeManager from "./bridge-manager";

// For convenience create a TypeScript interface that mirrors the C# interface
interface FileBridge {
  getDesktopFiles(): Promise<string[]>;
}

// Get bridge instance
const fileBridge = await bridgeManager.getBridge<FileBridge>("fileBridge"); // The name should be that of the bridge in camel-case

const files = await fileBridge.getDesktopFiles();
```

#### Listening to Bridge events

To fire an event from C# just create a regular event of type `Action` or `Action<T>`:

```cs
public event Action<TimeBridgeEventArg> TimeUpdated;

// Raise event
TimeUpdated(new TimeBridgeEventArg() { Time = DateTime.Now.ToString("HH:mm:ss") });
```

> Note: Your event args should be an object, primitives like `int` or other value types are not yet supported (but might be in a future update).

```ts
const timeBridge = await bridgeManager.getBridge<TimeBridge>("timeBridge");
timeBridge.addEventListener("timeUpdated", this._timeUpdated);

_timeUpdated = arg => {
  console.log("Time updated! The time is now: " + arg.time);
};
```

> **Note:** Because of the asynchronous nature of JavaScript the Bridge classes will be asynchronous, therefor use `async` `await` and `Promise<T>` on the TypeScript side.

## Building for production

* Build the `RwdApp` as `Release`, `x64` (in Visual Studio).
* In the `RwdReactUI` project run `npm run build`
* Copy the files from the `./build` (`RwdReactUI`) folder to the `bin\x64\Release\` (`RwdApp`) folder.
* Launch and test your `.exe` file, all done.

<!-- _Good luck!_ -->

## Help and resources

To modify how the CefSharp browser instance behaves read about it here:

* https://github.com/cefsharp/CefSharp/wiki/General-Usage

A great article about different UI Framework options:

* https://hackernoon.com/the-coolest-react-ui-frameworks-for-your-new-react-app-ad699fffd651
