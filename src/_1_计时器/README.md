# _1_计时器

这个示例用于对比三种常见的 .NET 计时方式，以及它们在精度和并发行为上的差异。

## 项目作用

- 演示 `Task.Delay` 循环计时的简单写法
- 演示 `System.Threading.Timer` 在回调耗时较长时可能出现的并发回调问题
- 演示 `System.Threading.PeriodicTimer` 在周期执行场景中的更稳定行为

## 代码入口

- `Program.cs`

## 当前示例内容

- `Test1()`：使用 `Task.Delay` 循环输出时间
- `Test2()`：使用 `Timer` 定时执行异步逻辑
- `Test3()`：使用 `PeriodicTimer` 周期性等待并执行任务
