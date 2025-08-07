[README.md](https://github.com/user-attachments/files/21672501/README.md)
# 🚗 Car Sales API

API REST desarrollada en **.NET 8.0** para gestionar ventas de automóviles y generar reportes.

## 🚀 Tecnologías Utilizadas

- **.NET 8.0** / **ASP.NET Core Web API**
- **Swagger** (documentación interactiva)
- **xUnit** + **Moq** + **FluentAssertions** (pruebas unitarias)
- **Clean Architecture**, **Repository Pattern**, **Inyección de dependencias**
- **Datos mockeados en memoria**
- **Medición de tiempos de ejecución por endpoint**

---

## ▶️ Cómo Ejecutar

### Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Pasos

```bash
git clone [url-del-repo]
cd CarSalesApi

dotnet restore
dotnet run --project src/CarSalesApi
```

### Acceder a Swagger

```
http://localhost:5xxx/swagger
```

---

## 📬 Endpoints Principales

### 1. Insertar una venta

`POST /api/sales`

```json
{
  "carType": 1,             // 1=Sedan, 2=SUV, 3=OffRoad, 4=Sport
  "distributionCenterId": 2 // ID del centro (1-4)
}
```

---

### 2. Obtener volumen total de ventas

`GET /api/sales/volume/total`

---

### 3. Obtener volumen por centro

`GET /api/sales/volume/center/{centerId}`

---

### 4. Obtener porcentaje de modelos vendidos por centro

`GET /api/sales/percentage/center`

---

## 🧪 Pruebas Unitarias

```bash
dotnet test tests/CarSalesApi.Tests
```

---

## 🧠 Lógica Clave

- El modelo **"Sport"** incluye un impuesto extra del **7%**
- Se mockean datos de venta al iniciar
- Se mide tiempo de ejecución por endpoint en el log

---

> Sin base de datos, sin frontend, arquitectura limpia, comentarios clave y pruebas incluidas.
