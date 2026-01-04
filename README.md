# 🪵 Log

Logging Library for C# applications and libraries.

---

<p align="center">
<img alt="RedOwl.Log" src="icon.png" width="200">
</p>

## 📦 Installation

```sh
dotnet add package RedOwl.Log
```

## 🌱 Usage

```c#
ILogger logger = new Logger();
logger.Level = LogLevel.Warn;
// By default the `Logger` instance will automatically write to the console
logger.AddFileSink("./path/to/file.log"); // Additionally write logs to a file

logger.Print("hello world!"); // Will always log
logger.Info("This is information"); // Will not log due to log level settings
logger.Warn("This is a warning");
logger.Error("This is an error");
```
