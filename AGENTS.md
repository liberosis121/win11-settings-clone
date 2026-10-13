# Agent guidelines

## Project overview

- HW1012: Windows 11 Settings Home Clone, a Windows Programming homework assignment.
- Use WinUI 3, XAML, C#, and the Windows App SDK in Visual Studio 2026 on Windows.
- This project simulates the Windows Settings interface; it must not change real Windows settings.
- Solution: `SettingsClone.slnx`. Project: `SettingsClone.csproj` at the repository root.
- Current baseline: .NET target `net8.0-windows10.0.19041.0`, Windows App SDK `2.5.1`, and Windows SDK BuildTools `10.0.28000.2705`. Preserve these unless a task requires a change.
- HomePage and a minimal SystemPage are implemented following the user's approved UI task. Keep Home visual fidelity the priority; add other pages only when requested. Always follow the latest user instruction about analysis, implementation, commits, and pushes.

## Technology constraints

- Do not switch to WPF, UWP, MAUI, Uno, Avalonia, Electron, React, or a web frontend without an explicit request.
- Do not add a database, backend, API, or cloud service unless requested.
- Do not upgrade SDKs or NuGet packages unless necessary for the authorized task; explain any required upgrade first.
- Do not introduce MVVM, dependency injection, the Repository Pattern, or other complex architecture without a request.
- Prefer techniques already taught: Grid, StackPanel, Border, ScrollViewer, basic controls, Frame, Page, and code-behind.

## Coding conventions

- Use PascalCase for C# classes, public methods, and properties; use camelCase for local variables and parameters.
- Keep XAML clear and readable, and give controls meaningful names.
- Keep code-behind limited to basic UI behavior.
- Avoid unnecessary abstractions. Reuse controls when there is a clear benefit.
- Avoid building the entire interface with hard-coded absolute coordinates.
- Never use a full-screen screenshot as a substitute for implementing the interface with controls.
- Prefer simple code the student can explain to the instructor.

## UI requirements

- When UI work is requested, consult `References/settings-reference.png` if it exists.
- Prioritize similarity in layout, spacing, typography, alignment, colors, and cards.
- Use Grid for the main layout and StackPanel for horizontal or vertical control groups.
- Use ScrollViewer when content can exceed the available space.
- Do not force every component into fixed absolute dimensions; allow the layout to adapt.
- Use Frame/Page navigation when needed, including simple data passing between pages.
- Prefer mock data; do not integrate real Windows Settings APIs.

## Workflow rules

- Before a major change, present a plan and wait for user approval. A detailed task already approved by the user authorizes the work within its scope.
- Divide work into small, verifiable steps.
- Edit only files within the task scope; do not delete existing code unnecessarily.
- Do not run destructive Git commands.
- Do not automatically commit or push feature work unless the user requests it.
- For this initial repository setup, the user permits the initial commit. Before creating the GitHub repository or making the first push, present its name, visibility, remote URL, and exact file list and wait for confirmation.
- Report which files were created or changed after editing.
- Build or perform suitable checks when the environment supports them. Prefer Visual Studio MSBuild for this WinUI project, for example:
  `& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SettingsClone.slnx /restore /t:Build /p:Configuration=Debug /p:Platform=x64 /verbosity:minimal /nologo`
- If a build cannot run or fails, explain the actual reason; never hide errors or claim success without evidence.
- Explain changes and results to the user in Vietnamese.

## Git safety rules

- Never use `git reset --hard`, `git clean -fd`, or force push.
- Do not change global Git identity without permission.
- Never commit secrets or credentials. Review the staged file list and relevant contents before committing.
- Keep `.vs/`, `bin/`, `obj/`, build artifacts, IDE personal files, logs, temporary files, and `.env` files out of Git.
- Keep source C#, XAML, `.csproj`, `.sln`/`.slnx`, required assets, `References/`, `AGENTS.md`, and README eligible for tracking.
- The three checked-in `Properties/PublishProfiles/win-*.pubxml` files are non-secret build profiles used by the project. Preserve them; never put credentials in them.
- Do not make the repository public without explicit permission. The requested GitHub repository is private and named `settings-clone`.
- Do not overwrite an existing `origin` without user confirmation.
- Do not amend old commits or rewrite history without permission.
- Preserve user files and existing configuration; review and extend existing setup instead of replacing it blindly.
