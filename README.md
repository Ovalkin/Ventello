# Vintello

Проект в который я добавляю всё интересное и нужно ради практического опыта.  

Основаня концепция онлайн площадки для перепродажи винтажных вещей.  
Готовый CRUD для 4 сущностей, Авторизация и аутентификация с JWT, DTOs.

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
