using _1labka;
using System;

class Program
{
    static void Main(string[] args)
    {
        var tanks = GetTanks();
        var units = GetUnits();
        var factories = GetFactories();
        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        var foundUnit = FindUnit(units, tanks, "Резервуар 2");
        if (foundUnit == null)
        {
            Console.WriteLine("Установка не найдена для указанного резервуара.");
            return; 
        }

        var factory = FindFactory(factories, foundUnit);
        if (factory == null)
        {
            Console.WriteLine("Завод не найден для указанной установки.");
            return; 
        }

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
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
        foreach (var tank in tanks)
        {
            if (tank.Name == unitName)
            {
                foreach (var unit in units)
                {
                    if (unit.Id == tank.UnitId)
                    {
                        return unit;
                    }
                }
            }
        }
        return null;
    }

    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        if (unit == null)
        {
            return null; 
        }

        foreach (var factory in factories)
        {
            if (factory.Id == unit.FactoryId)
            {
                return factory;
            }
        }
        return null;
    }

    public static int GetTotalVolume(Tank[] tanks)
    {
        int totalVolume = 0;
        foreach (var tank in tanks)
        {
            totalVolume += tank.Volume;
        }
        return totalVolume;
    }
}
