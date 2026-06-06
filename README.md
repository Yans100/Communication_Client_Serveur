
# Communication Client-Serveur — INF1010

Application full-stack composée d'un serveur socket asynchrone en C# (.NET) et d'un client web en SvelteKit, avec persistance SQLite via Entity Framework Core.

## Fonctionnalités

- Serveur socket asynchrone (TCP) avec routage de requêtes par réflexion
- Injection de dépendances via `Microsoft.Extensions.DependencyInjection`
- Persistance SQLite avec Entity Framework Core et migrations automatiques
- Client web SvelteKit avec TypeScript, Tailwind CSS et DaisyUI
- Configuration centralisée via `appsettings.json`

## Concepts démontrés

- Sockets TCP asynchrones
- Architecture client-serveur
- Patron Singleton (`AsyncSocketListener`)
- Injection de dépendances (.NET)
- ORM avec Entity Framework Core

## Technologies

**Serveur :** C# / .NET 8, Entity Framework Core, SQLite

**Client :** SvelteKit, TypeScript, Tailwind CSS, DaisyUI, Vite, Bun

## Prérequis

- .NET 8+
- Node.js 18+ et Bun

## Lancer le projet

```bash
# Serveur
cd SocketServerCode
dotnet run

# Client
cd Client
bun install
bun dev
```

## Structure

```
Client/              — frontend SvelteKit (TypeScript, Tailwind, DaisyUI)
SocketServerCode/    — serveur socket C# (.NET)
  SocketServer/      — logique serveur, services, routage
  SocketerServer.Data/ — DbContext, migrations EF Core
ServerExecutable/    — binaire compilé du serveur
```

---

Projet universitaire en équipe — cours INF1010, UQTR.
