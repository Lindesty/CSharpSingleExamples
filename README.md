# CSharpSingleExamples

`src` 目录下包含一组独立的 C# 示例项目。下面给出每个项目的说明文档入口，以及它们的主要作用。

## 项目列表

### 1. [_1_计时器](src/_1_计时器/README.md)

用于对比 `Task.Delay`、`System.Threading.Timer` 和 `System.Threading.PeriodicTimer` 三种计时实现方式，重点展示精度和并发执行差异。

### 2. [_2_Linq基本使用](src/_2_Linq基本使用/README.md)

使用 CSV 数据演示 LINQ 的基础查询能力，包括分组、排序、关联、投影、聚合和集合判断。

### 3. [_3_MiniEFCoreWithSqlite](src/_3_MiniEFCoreWithSqlite/README.md)

演示控制台程序中如何集成 SQLite 与 EF Core，并在启动时迁移数据库、写入数据、查询结果。

### 4. [_4_EFCoreWithSqliteInWPF](src/_4_EFCoreWithSqliteInWPF/README.md)

演示 WPF + SQLite + EF Core 的桌面 CRUD 示例，包含图书列表展示、搜索、编辑和持久化。

### 5. [_5_WpfWithAspNetCore](src/_5_WpfWithAspNetCore/README.md)

演示在 WPF 桌面应用中宿主 ASP.NET Core Web API，并通过客户端服务调用接口。
