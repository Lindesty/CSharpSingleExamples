# _2_Linq基本使用

这个示例通过车辆和厂商两份 CSV 数据，演示 LINQ 在查询、分组、排序和关联中的基本用法。

## 项目作用

- 演示如何把 CSV 文件读取并投影为对象集合
- 演示 `GroupBy`、`GroupJoin`、`Join`、`SelectMany` 等常见 LINQ 操作
- 演示 `Average`、`Max`、`Min`、`Any`、`All`、`Contains` 等聚合和判断方法

## 代码入口

- `Program.cs`

## 数据文件

- `fuel.csv`：车辆数据
- `manufacturers.csv`：厂商数据

## 说明

当前 `LinqExample.Test()` 中通过多段示例代码展示不同写法，按需取消 `return;` 即可查看对应片段的输出结果。
