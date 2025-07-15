# 🚀 Liftni-Bohqarish-Tizimi

Liftni boshqarish uchun yaratilgan .NET Core Web API loyihasi. Tizim foydalanuvchi so‘rovlarini qayta ishlaydi, lift holatini PostgreSQL ma’lumotlar bazasida saqlaydi va Docker orqali ishga tushiriladi.

Loyihaning Tuzilishi

### Lift.API/
├── Controllers/                // API endpointlar
├── Data/                       // Kontekst va konfiguratsiyalar
├── Models/                     // Ma'lumotlar bazasi modellari
├── Services/                   // Lift mantiqiy servisi
├── Migrations/                 // EF Core migratsiyalar
├── Program.cs                  // Dastur boshlanishi
├── appsettings.json            // Sozlamalar
└── Dockerfile                  // API uchun Docker fayli

docker-compose.yml             // Tizimni ishga tushuruvchi fayl


 📌 Funksionallik

- 🔼 Liftning joriy qavatini olish
- ⬆️ Liftni ma’lum bir qavatga yuborish
- 📥 So‘rovlar ro‘yxatini ko‘rish (navbatdagi chaqiriqlar)
- 🧠 FIFO (birinchi kelgan birinchi bajariladi) mantiqiy ishlov berish
- 📦 PostgreSQL bilan integratsiya
- 🐳 Docker Compose bilan to‘liq ishga tushirish


 🗂 Texnologiyalar

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- Swagger UI

---

## 🧱 Ma'lumotlar bazasi struktura

### 1. `ElevatorStatuses` jadvali
| Maydon          | Tip     | Tavsif                     |
|------------------|----------|-----------------------------|
| `Id`            | int      | Primary Key                |
| `CurrentFloor`  | int      | Lift joriy qavati          |
| `Direction`     | string   | Harakat yo'nalishi         |
| `IsBusy`        | bool     | Lift bandmi yoki yo‘q      |

### 2. `Requests` jadvali
| Maydon         | Tip     | Tavsif                        |
|----------------|----------|-------------------------------|
| `Id`           | int      | Primary Key                   |
| `RequestedFloor`| int     | So‘ralgan qavat               |
| `CreatedAt`    | DateTime | So‘rov vaqti                  |

---

## 🔗 API Endpoints

| Metod | URL                            | Tavsif                        |
|-------|--------------------------------|-------------------------------|
| GET   | `/api/elevator/status`         | Joriy lift holatini olish     |
| POST  | `/api/elevator/request/{floor}`| Qavatga chaqiriq yuborish     |
| GET   | `/api/elevator/requests`       | So‘rovlar navbatini ko‘rish   |

---

## ▶️ Ishga tushirish (Docker bilan)

```bash
git clone https://github.com/ElshodDev/Lift.git
cd LIFT
docker-compose up --build

```Swagger UI
http://localhost:8085/swagger  -->API
http://localhost:5051/browser  -->pgadmin4

📧 Email: admin@lift.com
🔐 Parol: mypassword
📂 Host name: lift_db
🗃 Database: LIFTDB



