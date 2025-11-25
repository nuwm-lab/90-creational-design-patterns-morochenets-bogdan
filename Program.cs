using System;

namespace AbstractFactoryHospital
{
    // =================================================================
    // 1. АБСТРАКТНІ ПРОДУКТИ (Публічні Інтерфейси)
    // =================================================================

    /// <summary>
    /// Абстрактний Продукт А: Інтерфейс для Будівель лікарень.
    /// </summary>
    public interface IHospitalBuilding
    {
        /// <summary>
        /// Повертає інформацію про тип будівлі.
        /// </summary>
        /// <returns>Рядок з описом будівлі.</returns>
        string GetBuildingInfo();
    }

    /// <summary>
    /// Абстрактний Продукт Б: Інтерфейс для Персоналу лікарень.
    /// </summary>
    public interface IHospitalStaff
    {
        /// <summary>
        /// Повертає інформацію про склад персоналу.
        /// </summary>
        /// <returns>Рядок з описом персоналу.</returns>
        string GetStaffInfo();

        /// <summary>
        /// Демонструє взаємодію персоналу з будівлею.
        /// </summary>
        /// <param name="building">Об'єкт будівлі, з яким відбувається взаємодія.</param>
        /// <returns>Рядок, що описує взаємодію.</returns>
        string InteractWithBuilding(IHospitalBuilding building);
    }

    // =================================================================
    // 2. АБСТРАКТНА ФАБРИКА (Публічний Інтерфейс)
    // =================================================================

    /// <summary>
    /// Абстрактна Фабрика: Інтерфейс для створення сімейства продуктів (Лікарня).
    /// </summary>
    public interface IHospitalFactory
    {
        /// <summary>
        /// Створює об'єкт будівлі лікарні.
        /// </summary>
        /// <returns>Об'єкт, що реалізує IHospitalBuilding.</returns>
        IHospitalBuilding CreateBuilding();

        /// <summary>
        /// Створює об'єкт персоналу лікарні.
        /// </summary>
        /// <returns>Об'єкт, що реалізує IHospitalStaff.</returns>
        IHospitalStaff CreateStaff();
    }

    // =================================================================
    // 3. КОНКРЕТНІ ПРОДУКТИ (Internal Реалізації)
    // =================================================================

    // --- Польова Лікарня (Field Hospital) ---

    internal class FieldHospitalBuilding : IHospitalBuilding
    {
        public string GetBuildingInfo()
        {
            return "Будівля: Намет/тимчасова споруда для польового госпіталю (Field).";
        }
    }

    internal class FieldHospitalStaff : IHospitalStaff
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

    // --- Капітальна Лікарня (Capital Hospital) ---

    internal class CapitalHospitalBuilding : IHospitalBuilding
    {
        public string GetBuildingInfo()
        {
            return "Будівля: Багатоповерхова капітальна будівля з відділеннями (Capital).";
        }
    }

    internal class CapitalHospitalStaff : IHospitalStaff
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
    // 4. КОНКРЕТНІ ФАБРИКИ (Internal Реалізації)
    // =================================================================

    /// <summary>
    /// Конкретна Фабрика для створення компонентів Польової Лікарні.
    /// </summary>
    internal class FieldHospitalFactory : IHospitalFactory
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
    internal class CapitalHospitalFactory : IHospitalFactory
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
    // 5. КЛІЄНТ (Використання Dependency Injection/IoC)
    // =================================================================

    /// <summary>
    /// Клієнтський клас, який використовує Абстрактну Фабрику для створення лікарні.
    /// Залежність (фабрика) впроваджується через конструктор (Dependency Injection).
    /// </summary>
    public class HospitalClient
    {
        private readonly IHospitalBuilding _building;
        private readonly IHospitalStaff _staff;

        /// <summary>
        /// Конструктор клієнта, який приймає абстрактну фабрику (Dependency Injection).
        /// </summary>
        /// <param name="factory">Об'єкт, що реалізує IHospitalFactory.</param>
        public HospitalClient(IHospitalFactory factory)
        {
            // Клієнт використовує фабрику для створення своїх компонентів
            _building = factory.CreateBuilding();
            _staff = factory.CreateStaff();
        }

        /// <summary>
        /// Запускає демонстраційний сценарій роботи лікарні.
        /// </summary>
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
    // 6. ГОЛОВНА ПРОГРАМА (Точка входу та IoC Контейнер)
    // =================================================================

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=================================================");
            Console.WriteLine("    Демонстрація Абстрактної Фабрики + DI (C#)   ");
            Console.WriteLine("=================================================");
            
            // 💡 Демонстрація IoC / Dependency Injection
            // Замість того, щоб клієнт сам створював фабрику (new FieldHospitalFactory()),
            // ми (головна програма, яка виступає в ролі IoC-контейнера) створюємо
            // потрібну фабрику і ПЕРЕДАЄМО її клієнту.

            // 1. Сценарій: Польова Лікарня
            Console.WriteLine("\n✅ Сценарій 1: Створення Польової Лікарні (Field Hospital):");
            // Створюємо залежність (конкретну фабрику)
            IHospitalFactory fieldFactory = new FieldHospitalFactory(); 
            // Впроваджуємо залежність в клієнта
            HospitalClient fieldHospital = new HospitalClient(fieldFactory); 
            fieldHospital.RunHospitalScenario();

            // 2. Сценарій: Капітальна Лікарня
            Console.WriteLine("\n✅ Сценарій 2: Створення Капітальної Лікарні (Capital Hospital):");
            // Створюємо іншу залежність
            IHospitalFactory capitalFactory = new CapitalHospitalFactory();
            // Впроваджуємо нову залежність, не змінюючи код клієнта HospitalClient
            HospitalClient capitalHospital = new HospitalClient(capitalFactory);
            capitalHospital.RunHospitalScenario();

            Console.WriteLine("\n=================================================");
            Console.WriteLine("  Конкретні фабрики та продукти є Internal.      ");
            Console.WriteLine("  Зовнішній код бачить лише інтерфейси та Client.  ");
            Console.WriteLine("=================================================");
        }
    }
}
