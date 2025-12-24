# 🔐 SSE-Based Session Management

A full-stack demo of **session expiry tracking and extension** using **Server-Sent Events (SSE)**.

The backend pushes real-time session state to the frontend without polling.

---

## ✨ Features

- Real-time session expiry notifications (SSE)
- Countdown warning before expiration
- Session extension without reconnecting
- Automatic logout on expiry
- Encrypted signup & login payloads (RSA)
- Hashed passwords (SHA-256)
- Backend-driven session state (single source of truth)

---

## 🔐 Security

- **RSA encryption**
  - Sensitive fields encrypted on frontend
  - Decrypted on backend

- **Password hashing**
  - SHA-256 hash generated before storage
  - Database never stores plain-text passwords

> Note: SHA-256 is used for learning purposes. Production systems should use bcrypt / Argon2.

---

## 🔁 Session Flow

1. User logs in
2. Backend sets session expiry time
3. Frontend opens SSE connection on dashboard
4. Backend emits warning events near expiry
5. User extends session or is auto-logged out

---

## 🧩 Tech Stack

**Backend**
- ASP.NET Core Web API
- Server-Sent Events
- In-memory session store
- SQL Server

**Frontend**
- Angular
- Native `EventSource`
- Service-based SSE handling

---

## 🛠️ Future Enhancements

- Multiple concurrent user sessions
- Redis-based session store
- Heartbeat events
- JWT + refresh tokens
- Idle detection
- Horizontal scaling

---
