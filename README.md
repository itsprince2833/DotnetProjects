# 📘 gRPC Overview

gRPC (gRPC Remote Procedure Call) is a high-performance, open-source RPC framework developed by Google. It enables services to communicate directly by calling methods on remote systems as if they were local.

---

## 🧩 Core Concepts

### 🔹 Remote Procedure Calls (RPC)
- Services communicate by calling remote methods instead of sending raw HTTP requests.
- Enables structured, strongly-typed, and contract-based communication.

### 🔹 Protocol Buffers (Protobuf)
- gRPC uses Protobuf to define service contracts and data structures.
- Benefits: efficient binary serialization, schema enforcement, and cross-language support.

### 🔹 Service Definition
- Services are defined in `.proto` files.
- These files generate client and server code [know as stubs] in multiple programming languages.

---

## 🔁 Communication Patterns

| Pattern                 | Description                           |
|-------------------------|---------------------------------------|
| Unary RPC               | One request, one response             |
| Server Streaming RPC    | One request, stream of responses      |
| Client Streaming RPC    | Stream of requests, one response      |
| Bidirectional Streaming | Stream of requests and responses      |

---

## 🚀 Benefits of gRPC

- ✅ High performance via HTTP/2 and Protobuf
- 🌍 Cross-language compatibility
- 📡 Native support for real-time streaming
- ⚙️ Strongly-typed APIs with code generation

---

## 📦 Common Use Cases

- Internal service-to-service communication (microservices)
- Real-time apps (chat, IoT telemetry, video streaming)
- Cross-platform or multi-language systems
- Backend communication in mobile/web apps (with gRPC-Web)

---

## ⚠️ When Not to Use gRPC

- Public-facing RESTful APIs (better browser compatibility)
- Environments without HTTP/2 support
- Cases needing human-readable formats (e.g., JSON)

---
## 🧠 Rule of Thumb

- Use gRPC for service-to-service internal communication, especially when performance, type safety, or streaming is important.
- Use REST when simplicity, browser support, or widespread tool compatibility is the priority.

---

## 📚 Resources

- [Official gRPC Docs](https://grpc.io/docs/)
- [Protocol Buffers](https://developers.google.com/protocol-buffers)

---

MIT License © 2025
