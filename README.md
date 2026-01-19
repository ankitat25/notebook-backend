# 📓 Notebook – Full Stack Application

**Notebook** is a full-stack personal notebook application where users can securely register, log in, create posts with optional images, manage their posts, and save favorite posts.  
The project is built using **ASP.NET Core Web API** for the backend and **React** for the frontend.

---

## ✨ Features

### 🔐 Authentication
- User Registration
- User Login
- JWT-based Authentication
- Secure APIs with Authorization

### 📝 Posts
- Create post with text
- Upload image with post
- View only your own posts
- Edit post (owner only)
- Delete post (owner only)

### ❤️ Favorites
- Mark posts as favorite
- Remove from favorites
- View all favorite posts in a separate section

### 🎨 UI
- Notebook-themed warm & clean design
- Card-based post layout
- Click image to open full post view
- Consistent theme across pages
- Responsive layout

---

## 🛠 Tech Stack

### Backend
- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Swagger**

### Frontend
- **React (Create React App)**
- **Axios**
- **CSS (custom styling)**
- **JWT stored in localStorage**

---

## 📂 Project Structure

Notebook
│
├── notebook-backend
│ ├── Controllers
│ ├── Models
│ ├── DTOs
│ ├── Data
│ ├── Migrations
│ ├── Uploads
│ └── Program.cs
│
├── notebook-frontend
│ ├── src
│ ├── public
│ └── package.json
│
└── README.md

yaml
Copy code

---

## 🔐 Authentication Flow

1. User registers  
2. User logs in  
3. Backend returns JWT token  
4. Token is stored in `localStorage`  
5. Token is sent in request header:

Authorization: Bearer <token>
6. Backend validates token for protected routes

---

## 🖼 Image Upload

- Images are saved locally in:

Uploads/images

- Image path is stored in database  
- Images are served using static files middleware

---

## ▶️ How to Run the Project

### Backend
1. Open backend folder  
2. Update `appsettings.json` with SQL Server connection  
3. Run migrations:

```bash
Add-Migration InitialCreate
Update-Database
Run API:
dotnet run
Swagger UI:

bash
Copy code
https://localhost:7260/swagger


👩‍💻 Author
Ankita Tiwari
Full Stack Developer
ASP.NET Core | React | SQL Server
