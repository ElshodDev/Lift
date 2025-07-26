# Lift Management System

Bu loyiha oddiy lift tizimini yaratishga qaratilgan bo‘lib, u foydalanuvchi so‘rovlarini qabul qiladi, ularni bajaradi va joriy holatini PostgreSQL ma'lumotlar bazasida saqlaydi.

##  Vazifa Tavsifi

Lift:
- Qavatlar orasida harakatlanadi (1-10).
- Harakatlanish bo‘yicha so‘rovlarni qayta ishlaydi.
- Har bir harakat holatini saqlab boradi.
- Docker yordamida ishga tushadi.

##  Talablar

###  API funksionalligi

- `GET /status` - Liftning joriy qavatini va holatini olish
- `POST /request` - Liftni ma'lum bir qavatga yuborish
- `GET /requests` - Kutilayotgan barcha so‘rovlar ro‘yxati

###  Lift qoidalari

- Faqat 1 dan 10 gacha bo‘lgan qavatlarda ishlaydi
- So‘rovlar FIFO asosida bajariladi
- Lift band bo‘lsa, yangi so‘rovlar navbatga qo‘shiladi

## Ma'lumotlar Bazasi Tuzilishi

- `ElevatorStatus` jadvali — joriy qavat va yo‘nalish
- `ElevatorRequest` jadvali — kutilayotgan so‘rovlar

##  Docker va Compose

Tizim quyidagi konteynerlarda ishlaydi:

- **Lift API** (`C# .NET`)
- **PostgreSQL** (ma'lumotlar bazasi)

Docker bilan ishga tushirish:

```bash
docker-compose up --build
