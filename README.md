# 📬 MarketingMailer API

A lightweight and scalable .NET API for sending marketing emails asynchronously via a background queue. This pattern ensures non-blocking performance, ideal for handling high-volume email workloads.

## 🚀 Features

- ✅ Minimal API using ASP.NET Core  
- ✅ FluentValidation for clean validation  
- ✅ Background service using `IHostedService`  
- ✅ In-memory queue with DI abstraction  
- ✅ Easy to extend for real-world queues (RabbitMQ, Azure, etc.)

## 🧱 Project Structure

```
MarketingMailer/
├── API/
│   ├── Endpoints/
│   ├── Requests/
│   ├── Responses/
│   ├── Services/
├── Application/
│   ├── Interfaces/
│   ├── Messaging/
├── Infrastructure/
│   ├── Services/
```

## 💡 How It Works

1. A request is posted to `/api/email/send`
2. It's validated via FluentValidation
3. The payload is enqueued in an in-memory queue
4. A background service dequeues and processes the email asynchronously

## ▶️ Running the Project

Clone the repo and run:

```bash
dotnet run --project MarketingMailer.API
```
Visit Swagger UI:

```text
http://localhost:{port}/swagger
```

## 🔧 Use Case

Perfect for:

- Email campaigns
- Bulk email processing
- Notification dispatching
- Any async operation that shouldn’t block API response

## 🛠 Tech Stack

- .NET 8  
- C#  
- Minimal API  
- FluentValidation  
- Hosted BackgroundService

---

📌 Clone, contribute, and customize to fit your needs!
