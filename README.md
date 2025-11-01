🎶 Music Orchestra Platform

Мікросервісна система для управління оркестровим каталогом, замовленнями та відгуками.

📘 Загальний опис

Music Orchestra Platform — це розподілена мікросервісна система, яка моделює роботу оркестрової організації:
управління інструментами, виконавцями, композиціями, замовленнями та користувацькими відгуками.

Система побудована на базі трьох незалежних сервісів:

CatalogService — каталог оркестрових даних

OrderService — замовлення та клієнти

ReviewService — користувацькі рецензії й рейтинги

🧩 Архітектура
React (Frontend)
      │
      ▼
 API Gateway (Reverse Proxy)
      │
 ┌───────────────┬────────────────┬────────────────┐
 │               │                │                │
 ▼               ▼                ▼
CatalogService   OrderService     ReviewService
(PostgreSQL)     (PostgreSQL)     (MongoDB)


Зв’язок між сервісами — через HTTP REST API (можлива інтеграція через RabbitMQ).

API Gateway — центральна точка входу у систему.

Взаємодія — асинхронна політика узгодженості через публікацію подій.

⚙️ Мікросервіси
1️⃣ CatalogService

Мета: зберігати дані про інструменти, виконавців та композиції.

База: PostgreSQL
Технології: .NET 8 + EF Core (Fluent API, Seed Data, Migrations)

Основні сутності:

Instrument (ID, Name, Type, Price)

Performer (ID, Name, InstrumentID)

Composition (ID, Title, Duration, Genre)

ConcertProgram (ID, ConcertID, CompositionID)

InstrumentImage (ID, InstrumentID, Url)

Зв’язки:

1:N → Instrument → Performer

M:N → ConcertProgram ↔ Composition

1:1 → Instrument ↔ InstrumentImage

Особливості:

Перевірка Price > 0

Індекси на Name, Type

Seed дані при першому запуску

2️⃣ OrderService

Мета: зберігати інформацію про клієнтів, замовлення та склад замовлень.

База: PostgreSQL
Технології: .NET 8 + EF Core

Основні сутності:

Customer (ID, Name, Email)

Order (ID, CustomerID, OrderDate, TotalAmount)

OrderItem (ID, OrderID, ProductId, Quantity, UnitPrice)

Зв’язки:

1:N → Customer → Order

1:N → Order → OrderItem

Особливості:

Обчислення суми замовлення автоматично

Перевірка унікальності email для клієнтів

Репозиторії з асинхронними CRUD методами

3️⃣ ReviewService

Мета: управління користувацькими відгуками й рейтингами для інструментів, виконавців і композицій.

База: MongoDB
Технології: .NET 8 + MongoDB.Driver

Основні сутності (JSON-документи):

{
  "Id": "uuid",
  "EntityType": "Instrument | Performer | Composition",
  "EntityId": "string",
  "Author": "John Doe",
  "Rating": 5,
  "Comment": "Incredible performance!",
  "CreatedAt": "2025-11-01T12:00:00Z"
}


Особливості:

Гнучка документна структура (NoSQL)

Підтримка фільтрації та сортування за рейтингом

Зв’язок через EntityType + EntityId

🧠 Предметна область

Система моделює музичну екосистему оркестру:

Каталог представляє реальні ресурси (інструменти, виконавців, композиції);

Замовлення відображають взаємодію користувачів із системою (купівлі, бронювання);

Відгуки — відображення соціального аспекту (думки, рейтинги, популярність).

🪢 Bounded Contexts
Контекст	Мікросервіс	Відповідальність
Catalog Context	CatalogService	Зберігання і управління сутностями оркестру
Order Context	OrderService	Замовлення, транзакції, клієнти
Review Context	ReviewService	Відгуки, рейтинги, рекомендації
🔁 Політика узгодженості

Кожен мікросервіс має власну БД (повна незалежність даних).

Дублювання дозволене для ключових ідентифікаторів (EntityId, InstrumentId тощо).

Узгодженість даних — eventual consistency: сервіси обмінюються подіями через API або чергу.

Немає централізованої транзакції між БД — кожен сервіс гарантує локальну цілісність.

📊 База даних
CatalogService / OrderService (PostgreSQL)

Схеми створюються автоматично через EF Core Migrations

DDL-скрипти генеруються командою:

dotnet ef migrations add InitialCreate
dotnet ef database update

ReviewService (MongoDB)

Колекції створюються динамічно при вставці документів

📦 Запуск

Клонувати репозиторій

git clone https://github.com/shillxzy/MusicOrchestraPlatform.git


Налаштувати PostgreSQL і MongoDB з appsettings.json

Зібрати й запустити сервіси:

dotnet build
dotnet run --project CatalogService
dotnet run --project OrderService
dotnet run --project ReviewService

🧱 Технологічний стек

.NET 8 / C#

Entity Framework Core

PostgreSQL

MongoDB

REST API

Clean Architecture + Repository Pattern

✍️ Автор

shillxzy