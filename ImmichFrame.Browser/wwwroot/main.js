import { dotnet } from './_framework/dotnet.js'

const is_browser = typeof window != "undefined";
if (!is_browser) throw new Error(`Expected to be running in a browser`);

const dotnetRuntime = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = dotnetRuntime.getConfig();
// Function to save data to local storage
function saveToLocalStorage(key, data) {
    localStorage.setItem(key, data);
}

// Function to get data from local storage
function getFromLocalStorage(key) {
    return localStorage.getItem(key);
}
await dotnetRuntime.runMain(config.mainAssemblyName, [globalThis.location.href]);
