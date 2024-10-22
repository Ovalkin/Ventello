# Vintello
Основаня концепция - CMS система с ролями и категориями.

Это проект в который я добавляю всё интересное и нужное, ради практического опыта. 
* JWT
* DTOs (мапппинг в статических метода)

## Стек
* Entity Framework Core
* ASP.NET Core
* PostgreSql

## Для миграций
База данных
```pwd
dotnet ef database update --connection "host=localhost; port=5432; database=vintello;  username=postgres;  password=7878;"
```
База данных для интеграционных тестов
```pwd
dotnet ef database update --connection "host=localhost; port=5432; database=vintello_tests;  username=postgres;  password=7878;"
```
*строки подключения могут изменится в процессе разработки, находяться [здесь](./Vintello.Web.Api/appsettings.json).
