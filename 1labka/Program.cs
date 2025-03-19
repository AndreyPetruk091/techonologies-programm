using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace _1labka
{
    class Program
    {
        static void Main(string[] args)
        {
            var tanks = GetTanks();
            var units = GetUnits();
            var factories = GetFactories();
            Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

            var foundUnit = FindUnit(units, tanks, "Резервуар 2");
            var factory = FindFactory(factories, foundUnit);

            Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

            var totalVolume = GetTotalVolume(tanks);
            Console.WriteLine($"Общий объем резервуаров: {totalVolume}");

            // Чтение сделок из JSON
            var deals = LoadDealsFromJson("JSON_sample_1.json");
            var dealIds = GetNumbersOfDeals(deals);
            var sumsByMonth = GetSumsByMonth(deals);

            Console.WriteLine($"Количество найденных сделок: {dealIds.Count}");
            Console.WriteLine($"Идентификаторы сделок: {string.Join(", ", dealIds)}");
            foreach (var sum in sumsByMonth)
            {
                Console.WriteLine($"Месяц: {sum.Month.ToString("MMMM yyyy")}, Сумма: {sum.Sum}");
            }
        }

        public static Tank[] GetTanks()
        {
            return new Tank[]
            {
                new Tank(1, "Резервуар 1", "Надземный - вертикальный", 1500, 2000, 1),
                new Tank(2, "Резервуар 2", "Надземный - горизонтальный", 2500, 3000, 1),
                new Tank(3, "Дополнительный резервуар 24", "Надземный - горизонтальный", 3000, 3000, 2),
                new Tank(4, "Резервуар 35", "Надземный - вертикальный", 3000, 3000, 2),
                new Tank(5, "Резервуар 47", "Подземный - двустенный", 4000, 5000, 2),
                new Tank(6, "Резервуар 256", "Подводный", 500, 500, 3)
            };
        }

        public static Unit[] GetUnits()
        {
            return new Unit[]
            {
                new Unit(1, "ГФУ-2", "Газофракционирующая установка", 1),
                new Unit(2, "АВТ-6", "Атмосферно-вакуумная трубчатка", 1),
                new Unit(3, "АВТ-10", "Атмосферно-вакуумная трубчатка", 2)
            };
        }

        public static Factory[] GetFactories()
        {
            return new Factory[]
            {
                new Factory(1, "НПЗ№1", "Первый нефтеперерабатывающий завод"),
                new Factory(2, "НПЗ№2", "Второй нефтеперерабатывающий завод")
            };
        }

        public static Unit FindUnit(Unit[] units, Tank[] tanks, string unitName)
        {
            return tanks
                .Where(tank => tank.Name == unitName)
                .Select(tank => units.FirstOrDefault(unit => unit.Id == tank.UnitId))
                .FirstOrDefault();
        }

        public static Factory FindFactory(Factory[] factories, Unit unit)
        {
            return factories.FirstOrDefault(factory => factory.Id == unit.FactoryId);
        }

        public static int GetTotalVolume(Tank[] tanks)
        {
            return tanks.Sum(tank => tank.Volume);
        }

        // JSON
        public static IEnumerable<Deal> LoadDealsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<IEnumerable<Deal>>(json);
        }


        public static IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
        {
            return deals
                .Where(d => d.Sum >= 100)
                .OrderBy(d => d.Date)
                .Take(5)
                .OrderByDescending(d => d.Sum)
                .Select(d => d.Id)
                .ToList();
        }


        public static IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
        {
            return deals
                .GroupBy(d => new { d.Date.Year, d.Date.Month })
                .Select(g => new SumByMonth(new DateTime(g.Key.Year, g.Key.Month, 1), g.Sum(d => d.Sum)))
                .ToList();
        }
    }

    public class Deal
    {
        public int Sum { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
    }

    public record SumByMonth(DateTime Month, int Sum);
}