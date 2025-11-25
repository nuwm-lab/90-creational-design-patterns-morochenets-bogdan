using System;

// =================================================================
// 1. АБСТРАКТНІ ПРОДУКТИ (Інтерфейси)
// =================================================================

/// <summary>
/// Абстрактний Продукт А: Інтерфейс для Будівель лікарень.
/// </summary>
public interface IHospitalBuilding
{
    string GetBuildingInfo();
}

/// <summary>
/// Абстрактний Продукт Б: Інтерфейс для Персоналу лікарень.
/// </summary>
public interface IHospitalStaff
{
    string GetStaffInfo();
    string InteractWithBuilding(IHospitalBuilding building);
}

// =================================================================
// 2. АБСТРАКТНА ФАБРИКА (Інтерфейс)
// =================================================================

/// <summary>
/// Абстрактна Фабрика: Інтерфейс для створення сімейства продуктів (Лікарня).
/// </summary>
public interface IHospitalFactory
{
    IHospitalBuilding CreateBuilding();
    IHospitalStaff CreateStaff();
}

// =================================================================
// 3. КОНКРЕТНІ ПРОДУКТИ (Польова Лікарня)
// =================================================================

public class FieldHospitalBuilding : IHospitalBuilding
{
    public string GetBuildingInfo()
    {
        return "Будівля: Намет/тимчасова споруда для польового госпіталю (Field).";
    }
}

public class FieldHospitalStaff : IHospitalStaff
{
    public string GetStaffInfo()
    {
        return "Персонал: Польові хірурги та медсестри (швидке розгортання) (Field).";
    }

    public string InteractWithBuilding(IHospitalBuilding building)
    {
        string buildingInfo = building.GetBuildingInfo();
        return $"Персонал польового госпіталю працює в умовах: ({buildingInfo})";
    }
}

// =================================================================
// 4. КОНКРЕТНІ ПРОДУКТИ (Капітальна Лікарня)
// =================================================================

public class CapitalHospitalBuilding : IHospitalBuilding
{
    public string GetBuildingInfo()
    {
        return "Будівля: Багатоповерхова капітальна будівля з відділеннями (Capital).";
    }
}

public class CapitalHospitalStaff : IHospitalStaff
{
    public string GetStaffInfo()
    {
        return "Персонал: Вузькоспеціалізовані лікарі та постійний медперсонал (Capital).";
    }

    public string InteractWithBuilding(IHospitalBuilding building)
    {
        string buildingInfo = building.GetBuildingInfo();
        return $"Персонал капітальної лікарні працює в умовах: ({buildingInfo})";
    }
}

// =================================================================
// 5. КОНКРЕТНІ ФАБРИКИ
// =================================================================

/// <summary>
/// Конкретна Фабрика для створення компонентів Польової Лікарні.
/// </summary>
public class FieldHospitalFactory : IHospitalFactory
{
    public IHospitalBuilding CreateBuilding()
    {
        return new FieldHospitalBuilding();
    }

    public IHospitalStaff CreateStaff()
    {
        return new FieldHospitalStaff();
    }
}

/// <summary>
/// Конкретна Фабрика для створення компонентів Капітальної Лікарні.
/// </summary>
public class CapitalHospitalFactory : IHospitalFactory
{
    public IHospitalBuilding CreateBuilding()
    {
        return new CapitalHospitalBuilding();
    }

    public IHospitalStaff CreateStaff()
    {
        return new CapitalHospitalStaff();
    }
}

// =================================================================
// 6. КЛІЄНТ
// =================================================================

/// <summary>
/// Клієнтський клас, який використовує Абстрактну Фабрику для створення лікарні.
/// </summary>
public class HospitalClient
{
    private readonly IHospitalBuilding _building;
    private readonly IHospitalStaff _staff;

    // Клієнт взаємодіє лише з абстракціями
    public HospitalClient(IHospitalFactory factory)
    {
        _building = factory.CreateBuilding();
        _staff = factory.CreateStaff();
    }

    public void RunHospitalScenario()
    {
        Console.WriteLine("--- Конфігурація Лікарні ---");
        Console.WriteLine(_building.GetBuildingInfo());
        Console.WriteLine(_staff.GetStaffInfo());
        Console.WriteLine(_staff.InteractWithBuilding(_building));
        Console.WriteLine("-----------------------------");
    }
}

// =================================================================
// 7. ГОЛОВНА ПРОГРАМА (Точка входу)
// =================================================================

public class Program
{
    public static void Main()
    {
        // Встановлення кодування для коректного відображення українських символів
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=================================================");
        Console.WriteLine("    Демонстрація Абстрактної Фабрики (C#)        ");
        Console.WriteLine("=================================================");

        // Сценарій 1: Створення Польової Лікарні
        Console.WriteLine("\n✅ Сценарій 1: Створення Польової Лікарні (Field Hospital):");
        IHospitalFactory fieldFactory = new FieldHospitalFactory();
        HospitalClient fieldHospital = new HospitalClient(fieldFactory);
        fieldHospital.RunHospitalScenario();

        // Сценарій 2: Створення Капітальної Лікарні
        Console.WriteLine("\n✅ Сценарій 2: Створення Капітальної Лікарні (Capital Hospital):");
        IHospitalFactory capitalFactory = new CapitalHospitalFactory();
        HospitalClient capitalHospital = new HospitalClient(capitalFactory);
        capitalHospital.RunHospitalScenario();
    }
}
