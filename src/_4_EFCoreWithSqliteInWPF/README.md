# _4_EFCoreWithSqliteInWPF

这个示例演示如何在 WPF 应用中结合 SQLite、EF Core 和 MVVM 完成一个简单的图书管理界面。

## 项目作用

- 在桌面界面中展示数据库中的图书列表
- 支持新增、删除、刷新和提交修改
- 支持按书名关键字过滤搜索
- 演示仓储、ViewModel 和界面之间的基本协作方式

## 代码入口

- `App.xaml`
- `MainView.xaml`
- `MainViewModel.cs`

## 主要技术

- WPF
- `CommunityToolkit.Mvvm`
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Mapster`
- `HandyControl`
