using System;
using System.Globalization;
using System.Windows.Data;

namespace _4_EFCoreWithSqliteInWPF.Converters;


public class UtcTickToDateTimeConverter : IValueConverter
{
    /// <summary>
    /// 将 UTC 毫微秒 (long) 转换为 DateTime。
    /// </summary>
    /// <param name="value">要转换的值，期望为 long 类型的 UTC 毫微秒。</param>
    /// <returns>转换后的 DateTime 对象，如果输入无效则返回 DateTime.MinValue。</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not long utcTick)
        {
            // 如果值不是 long 类型，或者为 null，则返回 DateTime 的最小值
            return DateTime.MinValue;
        }

        // DateTime.FromBinary(long)
        // DateTime.FromFileTime(long)
        // DateTime.FromOADate(double)

        // 关键：DateTime 的构造函数或 Ticks 属性使用的是毫微秒。
        // Ticks 属性表示自 0001 年 1 月 1 日午夜 12:00:00 以来所经过的 100 纳秒间隔数。
        // 这通常是您要转换的“UtcTick”。

        try
        {
            // 直接使用 long 类型的 Ticks 来创建新的 DateTime 对象
            return new DateTime(utcTick, DateTimeKind.Utc);
        }
        catch (ArgumentOutOfRangeException)
        {
            // 如果 utcTick 的值超出 DateTime Ticks 的有效范围
            return DateTime.MinValue;
        }
    }

    /// <summary>
    /// 将 DateTime 转换回 UTC 毫微秒 (long)。
    /// </summary>
    /// <param name="value">要转换的值，期望为 DateTime。</param>
    /// <returns>转换后的 long (毫微秒) 值，如果输入无效则返回 DateTime.MinValue.Ticks。</returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
        {
            // 如果值不是 DateTime 类型，或者为 null，则返回 Ticks 的最小值
            return DateTime.MinValue.Ticks;
        }

        // 关键：返回 DateTime 对象的 Ticks 属性
        // Ticks 是一个 long 值，表示 100 纳秒的间隔数
        return dateTime.Ticks;
    }
}