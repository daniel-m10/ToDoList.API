# 📝 ToDoListApi

Una API REST construida con .NET para manejar tareas (To-Do list) con soporte para autenticación de usuarios y operaciones CRUD.

## 📌 Características principales

- Registro y autenticación de usuarios.
- CRUD de tareas por usuario.
- Filtro de tareas por estado (`Pending`, `Completed`, etc.).
- Diseño modular y profesional con principios SOLID.
- Enfoque TDD desde el inicio.
- Integración continua con GitHub Actions.

## 🧱 Arquitectura del Proyecto

```bash
ToDoListApi/
├── ToDoListApi.sln                # Solución principal
├── ToDoListApi.Api/              # Proyecto Web API (controladores)
├── ToDoListApi.Application/      # Lógica de negocio (dominio, casos de uso)
└── ToDoListApi.Tests/            # Tests unitarios (NUnit)

<!-- Trigger CI for status check -->
