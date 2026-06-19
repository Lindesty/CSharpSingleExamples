# _5_WpfWithAspNetCore

这个示例演示如何在一个 WPF 桌面程序中承载 ASP.NET Core，并通过 HTTP 调用本地或远程 Web API。

## 项目作用

- 在 WPF 进程中按配置启用 ASP.NET Core Web API
- 通过客户端服务调用 `GreetingController`
- 演示桌面端、服务端和共享契约的分层结构
- 支持单实例自带服务端，也支持连接另一份已启动的服务实例

## 项目结构

- `_5_WpfWithAspNetCore.Desktop`：WPF 桌面入口，负责宿主启动与界面交互
- `_5_WpfWithAspNetCore.Server`：ASP.NET Core 控制器和服务逻辑
- `_5_WpfWithAspNetCore.Client`：基于 Refit 的 API 调用封装
- `_5_WpfWithAspNetCore.Share`：前后端共享的数据契约

## 代码入口

- `_5_WpfWithAspNetCore.Desktop/Program.cs`
- `_5_WpfWithAspNetCore.Desktop/MainWindowViewModel.cs`
- `_5_WpfWithAspNetCore.Server/Controllers/GreetingController.cs`

## 运行说明

程序启动时会读取 `appsettings.json`：

- `ServerOption:Enabled=true` 时，当前实例会启动内置 Web API
- `ClientOption:Endpoint` 用于指定客户端调用的目标地址
