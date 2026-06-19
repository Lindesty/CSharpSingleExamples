# _3_MiniEFCoreWithSqlite

这个示例演示如何在控制台程序中集成 SQLite 与 EF Core，并完成数据库初始化和基础写入查询。

## 项目作用

- 使用依赖注入注册 `DbContext`
- 使用 `Database.Migrate()` 在启动时自动迁移数据库
- 演示插入一条 `Book` 数据并查询现有书名列表
- 演示通过 `ILogger` 输出执行结果

## 代码入口

- `Program.cs`

## 主要技术

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.Extensions.DependencyInjection`
- `Microsoft.Extensions.Logging`
