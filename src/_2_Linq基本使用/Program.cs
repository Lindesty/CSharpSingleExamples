namespace _2_Linq基本使用;

internal class Program
{
    static void Main(string[] args)
    {
        LinqExample.Test();
    }
}


public static class LinqExample
{
    public static void Test()
    {
        var cars = GetCarsFromCsv();
        var manufacturers = GetManufacturersFromCsv();

        #region 输出各个品牌的平均，最大、最小油耗(Average,Max,Min)

        {
            var query = cars
                .GroupBy(c => c.Manufacturer)
                .Select((group, index) => new
                {
                    Manufacturer = group.Key,
                    AvgOilComsumption = group.Average(car => car.CombinedOilConsumption),
                    MaxOilComsumption = group.Max(car => car.CombinedOilConsumption),
                    MinOilComsumption = group.Min(car => car.CombinedOilConsumption),
                })
                .OrderBy(group => group.AvgOilComsumption);
            foreach (var info in query)
            {
                Console.WriteLine($"品牌：{info.Manufacturer}" +
                                  $"\n\t平均油耗{info.AvgOilComsumption:##.###}" +
                                  $"\n\t最大油耗{info.MaxOilComsumption}" +
                                  $"\n\t最小油耗{info.MinOilComsumption}");
            }
        }
        return;
        #endregion


        #region GroupJoin 将所有车按厂家分组并输出并拼接厂家信息(GroupJoin)

        {
            var query = manufacturers.GroupJoin(
                cars,
                manufacturer => manufacturer.Name,
                car => car.Manufacturer,
                (m, c) =>
                    new { Cars = c, Manufacturer = m }
            ).OrderByDescending(info => info.Manufacturer.Name);
            foreach (var info in query)
            {
                Console.WriteLine(
                    $"{info.Manufacturer.Name}-{info.Manufacturer.Headquarters}-{info.Manufacturer.Phone}");
                var res = info.Cars.OrderByDescending(c => c.CombinedOilConsumption).Take(5)
                    .Select((car, index) => ($"\t{index + 1,3}.{car.CombinedOilConsumption,3} {car.Model}"));
                Console.WriteLine(string.Join('\n', res));
            }
        }
        return;

        #endregion


        #region 将所有车按厂家分组并输出(GroupBy)

        {
            {
                var query2 = from car in cars
                             group car by car.Manufacturer
                    into manufacturerGroup
                             orderby manufacturerGroup.Key descending
                             select manufacturerGroup;
            }


            var query = cars
                .GroupBy(car => car.Manufacturer)
                .OrderByDescending(g => g.Key);
            foreach (var group in query)
            {
                Console.WriteLine($"{group.Key} 有 {group.Count()} 辆车");
                foreach (var car in group)
                {
                    Console.WriteLine($"\t {car.Model} {car.CombinedOilConsumption}");
                }
            }
        }
        return;

        #endregion


        #region 输出油耗排序前10的厂家并接上厂家信息(OrderBy,ThenBy,Take,Join)

        {
            var query = cars
                .OrderByDescending(car => car.CombinedOilConsumption)
                .ThenByDescending(car => car.Model)
                .Take(10)
                .Join(manufacturers,
                    car => car.Manufacturer,
                    manufacturer => manufacturer.Name,
                    (car, m) => new { Name = car.ToString(), m.Headquarters, m.Phone, car.CombinedOilConsumption });
            Console.WriteLine(
                string.Join('\n', query.Select((o, index) =>
                    $"{index + 1,3}{o.CombinedOilConsumption,3}{o.Name} -> {o.Headquarters}-{o.Phone}"
                )));
        }

        #endregion

        #region  输出子集中的集合(SelectMany)

        {
            (int id, (int stuId, string stuName)[] stus)[] rooms =
            [
                (0, [(0, "Jack"), (1, "Mary"), (2, "Tom")]),
                (1, [(3, "Json"), (4, "July"), (5, "Jar")]),
            ];
            // 输出所有班里的学生
            Console.WriteLine(string.Join(',',
                rooms.SelectMany(room => room.stus)
                    .Select(stu => stu.stuName)
            ));
        }
        return;

        #endregion

        #region 根据油耗排序，再根据名称排序，显示排名前10的车辆(OrderBy ThenBy FirstOrDefault)

        {
            {
                //函数式写法
                var query1 = cars
                    /*先根据油耗排序*/.OrderByDescending(car => car.CombinedOilConsumption)
                    /*后根据名称排序*/.ThenByDescending(c => c.Model);
            }
            // 声名式结构代码
            var query = from car in cars
                        orderby car.CombinedOilConsumption descending, car.Model descending
                        select new
                        {
                            //数据投影重构数据
                            Name = car.ToString(),
                            car.CombinedOilConsumption
                        };

            // 打印排名前10的车辆
            Console.WriteLine(
                string.Join('\n',
                    query
                        .Take(10)
                        .Select((car, index) =>
                            $"{index + 1,3}. {car.CombinedOilConsumption,3} {car.Name}"
                        )));

            // 打印排名第一的车辆的名称
            Console.WriteLine($"排名第一的车：{query.FirstOrDefault()}");
        }
        return;

        #endregion

        #region 检查表中是否存在(Any All Contains)

        {
            // 是存在大众这个品牌的车
            var manufacturer = "Volkswagen";
            var query = cars.Any(car => car.Manufacturer == manufacturer);
            var result = query ? "存在" : "不存在";
            Console.WriteLine($"{result}{manufacturer}的车");

            // 用All对所有数据进行判断
            Console.WriteLine($"所有车的综合油耗都大于1? {cars.All(car => car.CombinedOilConsumption > 1)}");

            // 显示列表第一辆车
            Console.WriteLine(cars.FirstOrDefault());

            // Contains查找车是否存在，需要重写Equals方法
            var carToFind = new Car()
            {
                Year = "2016",
                Manufacturer = "ALFA ROMEO",
                Model = "4C"
            };
            Console.WriteLine(cars.Contains(carToFind));
        }
        return;

        #endregion
    }

    private static List<Car> GetCarsFromCsv()
    {
        var cars = File.ReadAllLines(@"./fuel.csv")
            .Skip(1) //跳过标题
            .Where(line => line.Length > 1) //去除空行
            .ToCars()
            /*.Select(line => // 转换数据为对象
            {
                var columns = line.Split(",");
                var car = new Car()
                {
                    Year = columns[0],
                    Manufacturer = columns[1],
                    Model = columns[2],
                    Displacement = double.Parse(columns[3]),
                    CylindersCount = int.Parse(columns[5]),
                    HighWayOilConsumption = int.Parse(columns[6]),
                    CombinedOilConsumption = int.Parse(columns[7]),
                };
                return car;
            })*/
            .ToList(); // IEnumerable 转成 List


        return cars;
    }

    private static List<Manufacturer> GetManufacturersFromCsv()
    {
        var manufacturers = File.ReadAllLines("./manufacturers.csv")
            .Where(line => line.Length > 1)
            .ToManufacturers()
            .ToList();
        return manufacturers;
    }

    private static IEnumerable<Car> ToCars(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(",");
            var car = new Car()
            {
                Year = columns[0],
                Manufacturer = columns[1],
                Model = columns[2],
                Displacement = double.Parse(columns[3]),
                CylindersCount = int.Parse(columns[5]),
                HighWayOilConsumption = int.Parse(columns[6]),
                CombinedOilConsumption = int.Parse(columns[7]),
            };
            yield return car;
        }
    }

    private static IEnumerable<Manufacturer> ToManufacturers(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(",");
            var manufacturer = new Manufacturer(
                name: columns[0],
                headquarters: columns[1],
                phone: columns[2]
            );
            yield return manufacturer;
        }
    }
}


public class Manufacturer
{
    /// <summary>
    /// 生产厂家名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 生产厂家地址
    /// </summary>
    public string Headquarters { get; }

    /// <summary>
    /// 生产厂家电话
    /// </summary>
    public string Phone { get; }

    public Manufacturer(string name, string headquarters, string phone)
    {
        Name = name;
        Headquarters = headquarters;
        Phone = phone;
    }

    public override bool Equals(object obj)
    {
        return Equals((Manufacturer)obj);
    }

    protected bool Equals(Manufacturer other)
    {
        return Name == other.Name && Headquarters == other.Headquarters && Phone == other.Phone;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Headquarters, Phone);
    }

    public override string ToString()
    {
        return $"{Name}-{Headquarters}-{Phone}";
    }
}

public class Car
{
    /// <summary>
    /// 生产年份
    /// </summary>
    public string Year { get; set; }

    /// <summary>
    /// 生产商
    /// </summary>
    public string Manufacturer { get; set; }

    /// <summary>
    /// 汽车型号
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// 排量
    /// </summary>
    public double Displacement { get; set; }

    /// <summary>
    /// 汽缸数量
    /// </summary>
    public int CylindersCount { get; set; }

    /// <summary>
    /// 城市油耗
    /// </summary>
    public int CityOilConsumption { get; set; }

    /// <summary>
    /// 高速油耗
    /// </summary>
    public int HighWayOilConsumption { get; set; }

    /// <summary>
    /// 综合油耗
    /// </summary>
    public int CombinedOilConsumption { get; set; }

    public override string ToString()
    {
        return $"{Manufacturer}-{Model}-{Year}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        var other = (Car)obj;
        return Equals(Year, other.Year) && Equals(Manufacturer, other.Manufacturer) && Equals(Model, other.Model);
    }
}







