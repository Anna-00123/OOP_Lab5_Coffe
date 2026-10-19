using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeApp
{
    internal class Program
    {
        static List<Coffee> coffees = new List<Coffee>();
        static int maxCount;
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            maxCount = ReadInt("Введіть максимальну кількість об'єктів N: ", 1, 100);

            while (true)
            {
                PrintMenu();
                int choice = ReadInt("Ваш вибір: ", 0, 6);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: AddCoffee(); break;
                    case 2: ShowAll(); break;
                    case 3: FindCoffee(); break;
                    case 4: DemonstrateBehavior(); break;
                    case 5: DeleteCoffee(); break;
                    case 6: DemonstrateStatic(); break;
                    case 0:
                        Console.WriteLine("Роботу програми завершено. До побачення!");
                        return;
                }
                Console.WriteLine();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("========== МЕНЮ ==========");
            Console.WriteLine("1 - Додати об'єкт");
            Console.WriteLine("2 - Переглянути всі об'єкти");
            Console.WriteLine("3 - Знайти об'єкт");
            Console.WriteLine("4 - Продемонструвати поведінку");
            Console.WriteLine("5 - Видалити об'єкт");
            Console.WriteLine("6 - Продемонструвати static-методи");
            Console.WriteLine("0 - Вийти з програми");
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Error: введіть ціле число від {min} до {max}!");
            }
        }

        static void AddCoffee()
        {
            if (coffees.Count >= maxCount)
            {
                Console.WriteLine($"Error: досягнуто максимальної кількості об'єктів ({maxCount})!");
                return;
            }

            Console.WriteLine("Режим додавання:");
            Console.WriteLine("1 - через рядок");
            Console.WriteLine("2 - випадковий конструктор");
            Console.Write("Ваш вибір: ");
            string mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Формат: код,назва,тип,ціна,дата,декаф,об'єм");
                Console.WriteLine("Приклад: AB1234,Ranok Blend,Latte,85.50,15.03.2024,Ні,250");
                Console.Write("Введіть рядок: ");
                string line = Console.ReadLine();

                if (Coffee.TryParse(line, out Coffee coffee))
                {
                    coffees.Add(coffee);
                    Console.WriteLine("Об'єкт успішно додано!");
                }
                else
                {
                    Console.WriteLine("Object isn't created.");
                }
            }
            else if (mode == "2")
            {
                try
                {
                    coffees.Add(CreateRandomCoffee());
                    Console.WriteLine("Об'єкт успішно додано!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Object isn't created.");
                }
            }
            else
            {
                Console.WriteLine("Error: невірний режим додавання!");
                Console.WriteLine("Object isn't created.");
            }
        }

        static Coffee CreateRandomCoffee()
        {
            string[] names = { "Ranok Blend", "Milk Dream", "Dark Roast", "Sweet Mocha", "Morning Star" };
            string code = random.Next(100000, 1000000).ToString();
            string name = names[random.Next(names.Length)];
            CoffeeType type = (CoffeeType)random.Next(Enum.GetValues(typeof(CoffeeType)).Length);
            double price = Math.Round(30 + random.NextDouble() * 70, 2);

            switch (random.Next(4))
            {
                case 0:
                    Console.WriteLine("  [Конструктор Coffee(string, string, CoffeeType, double, DateTime, bool, int)]");
                    DateTime date = DateTime.Today.AddDays(-random.Next(0, 365));
                    int volume = random.Next(150, 401);
                    return new Coffee(code, name, type, price, date, random.Next(2) == 1, volume);
                case 1:
                    Console.WriteLine("  [Конструктор Coffee(string, string, CoffeeType, double price)]");
                    return new Coffee(code, name, type, price);
                case 2:
                    Console.WriteLine("  [Конструктор Coffee(string orderCode, string name, CoffeeType type)]");
                    return new Coffee(code, name, type);
                default:
                    Console.WriteLine("  [Конструктор Coffee(string orderCode, string name)]");
                    return new Coffee(code, name);
            }
        }

        static void PrintHeader()
        {
            Console.WriteLine($"{"№",-4}{"Код",-8}{"Назва",-20}{"Тип",-12}{"Ціна",-10}{"Обжарка",-14}{"Декаф",-7}{"Об'єм",-8}{"t°C"}");
            Console.WriteLine(new string('-', 86));
        }

        static void ShowAll()
        {
            Console.WriteLine($"Кількість створених об'єктів (Coffee.Counter): {Coffee.Counter}");

            if (coffees.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            PrintHeader();
            for (int i = 0; i < coffees.Count; i++)
                Console.WriteLine($"{i + 1,-4}{coffees[i].ToTableRow()}");
        }

        static void FindCoffee()
        {
            if (coffees.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.WriteLine("Пошук за характеристикою:");
            Console.WriteLine("1 - тип кави");
            Console.WriteLine("2 - наявність кофеїну");
            int mode = ReadInt("Ваш вибір: ", 1, 2);

            List<int> found = new List<int>();

            if (mode == 1)
            {
                Array types = Enum.GetValues(typeof(CoffeeType));
                for (int i = 0; i < types.Length; i++)
                    Console.WriteLine($"{i} - {types.GetValue(i)}");
                CoffeeType type = (CoffeeType)ReadInt("Тип кави: ", 0, types.Length - 1);

                for (int i = 0; i < coffees.Count; i++)
                    if (coffees[i].Type == type) found.Add(i);
            }
            else
            {
                Console.WriteLine("1 - без кофеїну");
                Console.WriteLine("2 - з кофеїном");
                bool decaf = ReadInt("Ваш вибір: ", 1, 2) == 1;

                for (int i = 0; i < coffees.Count; i++)
                    if (coffees[i].IsDecaf == decaf) found.Add(i);
            }

            if (found.Count == 0)
            {
                Console.WriteLine("Об'єктів не знайдено.");
                return;
            }

            PrintHeader();
            foreach (int i in found)
                Console.WriteLine($"{i + 1,-4}{coffees[i].ToTableRow()}");
        }

        static void DemonstrateBehavior()
        {
            if (coffees.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            ShowAll();
            int index = ReadInt("Оберіть номер об'єкта: ", 1, coffees.Count) - 1;
            Coffee c = coffees[index];

            try
            {
                c.Reheat();
                c.Reheat(5);
                c.Cool(15);
                c.ApplyDiscount(10);
                c.ApplyDiscount(20, 30);
                c.AddVolume(0);
                c.AddVolume(30, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Поточний стан (ToString): {c}");
            Console.WriteLine($"Ціна за 100 мл: {c.PricePer100Ml:F2} {c.Currency}");
        }

        static void DeleteCoffee()
        {
            if (coffees.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.WriteLine("Видалення:");
            Console.WriteLine("1 - за порядковим номером");
            Console.WriteLine("2 - за кодом замовлення");
            int mode = ReadInt("Ваш вибір: ", 1, 2);

            if (mode == 1)
            {
                Console.Write("Введіть номер об'єкта: ");
                if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= coffees.Count)
                {
                    coffees.RemoveAt(number - 1);
                    Console.WriteLine("Об'єкт успішно видалено!");
                }
                else
                {
                    Console.WriteLine("Error: невірний номер об'єкта!");
                }
            }
            else
            {
                Console.Write("Введіть код замовлення: ");
                string code = Console.ReadLine();
                int removed = coffees.RemoveAll(c => c.OrderCode == code);
                if (removed > 0)
                    Console.WriteLine("Об'єкт успішно видалено!");
                else
                    Console.WriteLine("Error: об'єкт з таким кодом не знайдено!");
            }
        }

        static void DemonstrateStatic()
        {
            Console.WriteLine("Static-методи класу Coffee:");
            Console.WriteLine("1 - Coffee.CelsiusToFahrenheit");
            Console.WriteLine("2 - Coffee.CalculatePrice");
            Console.WriteLine("3 - Coffee.IsValidOrderCode");
            Console.WriteLine("4 - Coffee.DefaultTemperatureC і Coffee.Counter");
            Console.WriteLine("5 - Coffee.Parse / Coffee.TryParse");
            int choice = ReadInt("Ваш вибір: ", 1, 5);

            switch (choice)
            {
                case 1:
                    int celsius = ReadInt("Введіть температуру в °C: ", -100, 200);
                    Console.WriteLine($"{celsius}°C = {Coffee.CelsiusToFahrenheit(celsius)}°F");
                    break;

                case 2:
                    Console.Write("Ціна за 100 мл: ");
                    if (!double.TryParse(Console.ReadLine(), out double per100) || per100 <= 0)
                    {
                        Console.WriteLine("Error: ціна має бути додатним числом!");
                        break;
                    }
                    int volume = ReadInt("Об'єм, мл: ", 30, 1000);
                    Console.WriteLine($"Вартість: {Coffee.CalculatePrice(per100, volume):F2} грн");
                    break;

                case 3:
                    Console.Write("Введіть код замовлення: ");
                    string code = Console.ReadLine();
                    Console.WriteLine(Coffee.IsValidOrderCode(code)
                        ? "Код коректний."
                        : "Код некоректний (потрібно 6 літер або цифр).");
                    break;

                case 4:
                    Console.WriteLine($"Coffee.DefaultTemperatureC = {Coffee.DefaultTemperatureC}°C");
                    Console.WriteLine($"Coffee.Counter = {Coffee.Counter}");
                    int newTemp = ReadInt("Нова температура за замовчуванням [20; 95]: ", -100, 200);
                    Coffee.DefaultTemperatureC = newTemp;
                    Console.WriteLine($"Coffee.DefaultTemperatureC = {Coffee.DefaultTemperatureC}°C");
                    break;

                case 5:
                    Console.Write("Введіть рядок для Parse: ");
                    string s = Console.ReadLine();
                    try
                    {
                        Coffee parsed = Coffee.Parse(s);
                        Console.WriteLine($"Parse: {parsed}");
                        Coffee.DecreaseCounter(1);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Parse: {ex.Message}");
                    }

                    if (Coffee.TryParse(s, out Coffee tried))
                    {
                        Console.WriteLine($"TryParse: true, {tried}");
                        Coffee.DecreaseCounter(1);
                    }
                    else
                    {
                        Console.WriteLine("TryParse: false");
                    }
                    break;
            }
        }
    }
}