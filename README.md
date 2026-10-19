# OOP Lab-5 — Модульне тестування (MSTest) класу Coffee

Практична робота №5 з дисципліни «Об'єктно-орієнтоване програмування».  
Виконала: Велигодська А.С., група 621п.

## Структура Solution

| Проєкт | Призначення |
|---|---|
| `OOP_Lab5_Coffe` | основний проєкт: клас `Coffee`, перелік `CoffeeType`, консольне меню (`Program.cs`) |
| `OOP_Lab5_Coffe.Tests` | MSTest-проєкт, тест-клас `CoffeeTests` |

Платформа: .NET 10, фреймворк тестування: MSTest.

## Що протестовано

- **Конструктори**: усі 5 варіантів, збільшення лічильника `Counter`, лічильник не змінюється у разі помилки.
- **Властивості (секції set з перевірками)**: `OrderCode`, `Name`, `Type`, `Price`, `RoastDate`, `VolumeMl`, `IsDecaf`, static `DefaultTemperatureC`, обчислювана `PricePer100Ml`.
- **Static-методи**: `IsLettersOrDigits`, `IsValidOrderCode`, `CelsiusToFahrenheit`, `CalculatePrice`, `DecreaseCounter`, `Parse`, `TryParse`.
- **Нестатичні методи**: `Reheat()`, `Reheat(int)`, `Cool`, `ApplyDiscount(double)`, `ApplyDiscount(double, double)`, `AddVolume(int)`, `AddVolume(int, bool)`, `ToString`, `ToTableRow`.

Усі тест-методи побудовані за шаблоном **AAA** (Arrange, Act, Assert).

**Атрибути:** `[TestClass]`, `[TestMethod]`, `[DataRow(...)]`, `[TestInitialize]`, `[TestCleanup]`, `[ClassInitialize]`, `[ClassCleanup]`.

**Методи перевірки:** `Assert.AreEqual`, `AreNotEqual`, `IsTrue`, `IsFalse`, `IsNull`, `IsNotNull`, `AreNotSame`, `IsInstanceOfType`, `Throws`, `ThrowsExactly`, `StringAssert.Contains / StartsWith / EndsWith`, `CollectionAssert.AreEqual`.

## Результати

- 71 тест-метод, 167 тестових випадків (з урахуванням `DataRow`) — **усі пройдені**.
- Покриття коду: клас `Coffee` — **98,1 %** блоків, **96,5 %** рядків.

### Помилки, знайдені unit-тестами та виправлені

1. `ApplyDiscount(percent)` і `ApplyDiscount(percent, minPrice)` — від'ємна знижка **підвищувала** ціну. Додано перевірку `percent ∈ (0; 100)`, інакше генерується `ArgumentOutOfRangeException`.
2. `DecreaseCounter(count)` — від'ємне значення **збільшувало** лічильник. Тепер для `count < 0` генерується `ArgumentOutOfRangeException`.

## Формат рядка для Parse / TryParse

`код,назва,тип,ціна,дата,декаф,об'єм`

Приклад: `AB1234,Ranok Blend,Latte,85.50,15.03.2024,Ні,250`

## Запуск

- Програма: **Ctrl+F5** у Visual Studio.
- Тести: **Тест → Виконати всі тести**.
- Покриття коду: **Тест → Аналіз покриття коду для всіх тестів**.
