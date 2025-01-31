# GitHub Activity CLI 🚀

¡Una herramienta de línea de comandos para espiar la actividad de usuarios de GitHub! 👀💻

## Características Principales ✨
- ✅ Obtén eventos recientes de cualquier usuario de GitHub
- 📦 Soporta los principales tipos de eventos:
  - `PushEvent` (Commits)
  - `WatchEvent` (Stars)
  - `IssuesEvent` (Issues)
  - Y más...
- 🎨 Formato de salida legible con símbolos descriptivos
- ⏰ Marca de tiempo en formato local
- 🚨 Manejo de errores con estilo (¡incluye mensajes divertidos!)

## Cómo Usar 🛠️

### Requisitos
- [.NET 6+](https://dotnet.microsoft.com/download)

### Instalación
```bash
git clone https://github.com/tu-usuario/github-activity-cli.git
cd github-activity-cli
```

###Ejecución
####Forma básica:

```bash
Copy
dotnet run -- <username>
# Ejemplo:
dotnet run -- Init0ne
```

```bash
Ejemplo de salida:

Copy
Actividad reciente de Init0ne:
===============================================

+ Hizo push de 3 commit(s) en QueHaceres-Cli (25/01/2025 19:57)
* Dio una estrella a developer-roadmap (25/01/2025 19:27)
- Abrió un issue en Init0ne (25/01/2025 18:48)
> Actividad de tipo CreateEvent en mi-super-repo (25/01/2025 17:06)

===============================================
```

## Manejo de Errores ⚠️

| HTTP  | Descripción               | ¡Traducción para Mortales!        | Emoji  |
|-------|---------------------------|------------------------------------|--------|
| 404   | Not Found                 | `WTF! ¿Quién es @${username}?`    | 🔍😱   |
| 403   | Forbidden                 | ¡Demasiado rápido, relájate!       | 🚀🐌   |
| 418   | I'm a teapot              | ¡Hora del té! 🫖                  | 🫖😂   |
| 5xx   | Server Errors             | GitHub está tomando una siesta...  | 💤😴   |

### Estructura del Código 🧩

```bash
Copy
📁 Actividad-Github-Cli/
├── 📄 Program.cs          # Lógica principal de la CLI
└── 📁 Clases/
    └── 📄 GitHubEvent.cs  # Modelos para la API de GitHub
```

### Contribuciones 🤝

¡Se aceptan mejoras con estilo! Algunas ideas:

- Añadir más tipos de eventos

- Soporte para emojis personalizados

- Modo oscuro/light para la salida

- Sistema de plugins para extensiones

### Notas Importantes 📌

La API de GitHub puede tener retrasos (hasta 5 min para eventos nuevos)

Límite de 60 peticiones/hora sin autenticación

¡Los mensajes de error son adrede divertidos! No te lo tomes personal 😉

### Licencia 📄
¡Haz lo que quieras con el código! Pero si te gustó, menciona al autor original 😊