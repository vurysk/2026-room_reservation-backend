# RoomReservation

## Description
RoomReservation adalah aplikasi web untuk melakukan reservasi ruangan secara online.  
Project ini dibuat untuk mempermudah proses pemesanan, approval, dan monitoring ruangan di lingkungan kampus/organisasi.

## Features
* User dapat melakukan reservasi ruangan
* Admin dapat melakukan approval reservasi
* Menampilkan daftar reservasi dan history
* Detail reservasi dengan status terkini
* Integrasi frontend React dengan backend ASP.NET Core

## Tech Stack
* Frontend: React + TypeScript + Vite
* Backend: ASP.NET Core + Entity Framework Core
* Database: SQLite
* Version Control: GitHub Project Board

## Installation
### Backend
```bash
cd 2026-room-reservation-backend
dotnet restore
dotnet ef database update
dotnet run
```
### Frontend
```bash
cd room-reserve-frontend 
npm install 
npm run dev
```

## Usage 
1. Jalankan backend dengan dotnet run.
2. Jalankan frontend dengan npm run dev.
3. Akses aplikasi di browser: menggunakan localhost yang tertera saat bagian frontend di run

## Environment Variables 
### Backend (.env)
Tambahkan file `.env` di root backend dengan isi seperti berikut:

Database
DB_HOST=localhost
DB_PORT=5432
DB_USER=your_username
DB_PASS=your_password
DB_NAME=room_reservation

ASP.NET Core
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://localhost:5000

### Frontend (.env)
Tambahkan file `.env` di root frontend dengan isi seperti berikut:

VITE_API_URL=http://localhost:5000
VITE_API_KEY=your_api_key_here

> **Catatan:** Jangan commit file `.env` ke GitHub jika berisi data sensitif (username, password, API key asli).  
> Sebagai gantinya, buat file `.env.example` yang berisi placeholder (misalnya `your_username`, `your_password`, `your_api_key_here`).  
> Developer lain bisa menyalin `.env.example` menjadi `.env` lalu mengganti nilai placeholder dengan konfigurasi asli mereka.

## License
Tidak ada lisensi khusus. Project ini digunakan untuk keperluan pembelajaran.

