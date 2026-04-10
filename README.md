# Controle de Gastos Residenciais

Sistema web para controle de gastos residenciais, composto por uma Web API em C# (.NET 9) e um frontend em React com TypeScript.

---

## Como executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)

### Back-end

```bash
cd ControleGastosResidenciais
dotnet run
```

A API sobe em: `http://localhost:5241`

> Em ambiente de desenvolvimento, o banco SQLite (`gastos.db`) é criado e populado automaticamente com dados de exemplo.

### Front-end

```bash
cd frontend
npm install
npm run dev
```

O frontend sobe em: `http://localhost:5173`

## Documentação interativa 

Disponível apenas em desenvolvimento:

```
http://localhost:5241/openapi/v1.json
```
