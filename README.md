# Blog
Простое REST API для блога на ASP.NET с использованием чистой архитектуры.

## Содержание
- [Технологии](#технологии)
- [Функционал](#функционал)
- [Использование](#использование)

## Технологии
- .NET 7.0
- ASP.NET
- EntityFramework
- FluentValidation
- PostgreSQL

## Функционал
- Jwt авторизация
- Верификация пользователя по email
- CRUD постов
- CRUD категорий
- CRUD тегов
- Возможность комментирования постов
- Возможность лайка постов

## Использование
- Склонируйте репозиторий
```sh
$ git clone https://github.com/Troggdorrr/Blog/
```
- Заполните [настройки](Blog.Web/appsettings.json)

**ConnectionStrings.Default**: PostgreSQL connection string

**EmailConfiguration**: Данные от SMTP
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "Default": "string"
  },
  "EmailConfiguration": {
    "From": "string",
    "SmtpServer": "string",
    "Port": "int",
    "Username": "string",
    "Password": "string"
  }
}
```
- Запустите сервер
```sh
[Blog.Web] $ dotnet run
