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

### Creating a TypeScript <-> C# Bridge

A Bridge class is a regular .NET C# Class. From TypeScript you will be able to invoke methods on these classes and get back results. You can see some sample bridges in the `Bridges` folder in the `RwdApp` solution.

> **Important limitations:**
>
> * Because of limitations of CefSharp you can only return basic values like `string`, `int`, `boolean` from your Bridge classes. To get around this limitation use `JsonConvert.Serialize(...)` to convert more advanced data (including arrays) into Json strings.
>
> * On the C# side your code should **not** be `async`. It will be run in it's own thread by CefSharp though so the GUI should not lock up.

#### Registering a new Bridge class

Edit the `Bridges.cs` file in the root of the `RwdApp` project.

#### Using a registered Bridge from TypeScript

```ts
import BridgeManager from "./bridge-manager";

// For convenience create a TypeScript interface that mirrors the C# interface
interface FileBridge {
  getDesktopFiles(): Promise<string[]>;
}

// Get bridge instance
const fileBridge = new BridgeManager().get<FileBridge>("FileBridge", true); // true means we will automatically json parse all data returned by this class

const files = await fileBridge.getDesktopFiles();
```

> **Note:** Because of the asynchronous nature of JavaScript the Bridge classes will be asynchronous, therefor use `async` `await` and `Promise<T>` on the TypeScript side.

> **Note:** By passing `true` as the second argument to the `BridgeManager.get<T>(bridgeType, automaticallyJsonParse)` method a bridge instance that automatically performs `JSON.parse(...)` is returned. Remember that the corresponding C# class must in that case return json string for all it's methods.

## Building for production

* Build the `RwdApp` as `Release`, `x64` (in Visual Studio).
* In the `RwdReactUI` project run `npm run build`
* Copy the files from the `./build` (`RwdReactUI`) folder to the `bin\x64\Release\` (`RwdApp`) folder.
* Launch and test your `.exe` file, all done.

<!-- _Good luck!_ -->

## Help and resources

To modify how the CefSharp browser instance behaves read about it here:

* https://github.com/cefsharp/CefSharp/wiki/General-Usage
