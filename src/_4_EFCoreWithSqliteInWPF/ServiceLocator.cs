using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using _4_EFCoreWithSqliteInWPF.Database;
using _4_EFCoreWithSqliteInWPF.Services;
using _4_EFCoreWithSqliteInWPF.Database.ConfigConstant;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace _4_EFCoreWithSqliteInWPF;

public class ServiceLocator
{
    private readonly IServiceProvider _sp;
    private static ServiceLocator? _current;
    

    public static ServiceLocator Current
    {
        get
        {
            if (_current is not null) return _current;

            var resource = Application.Current.TryFindResource(nameof(ServiceLocator));
            if (resource is ServiceLocator serviceLocator)
            {
                return _current = serviceLocator;
            }

            throw new Exception("理论上来讲不应该发生这种情况。");
        }
    }


    #region 注册各种窗体模型的获取

    public MainViewModel MainViewModel => _sp.GetRequiredService<MainViewModel>();

    #endregion


    #region 注册各种Service的获取(除非万不利己,不要从这里获取服务)

    /// <summary>
    /// 获取日志服务
    /// </summary>
    public ILogger<T> GetLogger<T>() => _sp.GetRequiredService<ILogger<T>>();

    #endregion

    public ServiceLocator()
    {
        var sc = new ServiceCollection();

        #region 注册日志服务

        // 添加日志服务
        sc.AddLogging(builder =>
        {
            builder.AddDebug(); // 调试输出窗口日志
            builder.SetMinimumLevel(LogLevel.Trace); // 设置最小日志级别
        });

        #endregion

        #region 注册类型映射服务

        // 注册Mapster
        sc.AddMapster();

        // 注册所有映射配置
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        #endregion

        #region 注册窗体ViewModel

        sc.AddSingleton<MainViewModel>();

        #endregion

        #region 注册EFCore仓库

        sc.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(DatabaseConstant.ConnectionString);
        });
        
        sc.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlite(DatabaseConstant.ConnectionString);
        });
        
        #endregion

        #region 注册Service

        sc.AddSingleton<IBookRepository, BookRepository>();

        #endregion


        _sp = sc.BuildServiceProvider();
        var logger = _sp.GetRequiredService<ILogger<ServiceLocator>>();

        logger.LogInformation("database init start");

        using var appDbContext = _sp.GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();

        logger.LogInformation("database init Success");



        logger.LogInformation("ServiceLocatorBuild Success");
    }
}