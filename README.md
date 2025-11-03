# Small Ball, Big Plane

A small physics-driven ball navigates floating platforms. Collect coins, avoid falling, and reach the finish to win.

---

## How to Play

- Objective:
  - Collect coins scattered in the level and reach the finish without falling off the platforms.
- Controls (Unity Input System):
  - Keyboard: WASD or Arrow Keys to roll the ball.
  - Gamepad: Left Stick to roll the ball.
  - Restart: The game restarts automatically after a fall. You can also trigger restart via UI when prompted.
- Rules and Feedback:
  - Falling off the level triggers a lose state, shows a Lose window, then restarts after a short delay.
  - Reaching the finish triggers a Win window; input is disabled while the win screen is shown.
  - Movement sound volume scales with speed; sound stops when airborne.
  - Your session coin counter resets on restart; the best (max) collected coin count is tracked.

---

## Implemented Features

- Core Gameplay
  - Physics-based rolling movement using `Rigidbody` with continuous force application.
  - Ground detection via downward raycast with a grace timer; exceeding time in air counts as a fall.
  - Coins system: collecting notifies listeners and updates counters; all coins reset on restart.
  - Finish logic: reaching finish triggers win flow and UI.
- Game Flow & UI
  - Central `GameManager` handling win/lose/restart events and coordinating UI windows.
  - Win and Lose windows (`WinLevelWindow`, `LooseWindow`) shown/hidden via a window manager.
  - Scene loading utilities (loading scene + scene loader) for clean transitions.
- Audio & Feel
  - Rolling SFX with volume controlled by speed via an animation curve.
- Camera & Presentation
  - Virtual camera setup (Cinemachine) with support utilities (e.g., virtual camera warp).
  - Universal Render Pipeline (URP) for lightweight rendering.
  - Text rendering with TextMesh Pro.
- Architecture & Tech
  - Input handling via the new Unity Input System with `PlayerInput`.
  - Dependency Injection using Reflex for clean separation and testability.
  - Asynchronous flows with UniTask (e.g., timed restart delays, async window transitions).
  - Tweening utilities available via PrimeTween for smooth UI/FX transitions.
  - Simple save binding for best coin count tracking (`CoinData` via a savable interface).

---

## Development Time

- Total time spent: 40 hours

---

## Frameworks, Packages, and Assets Used

- Engine & Pipeline
  - Unity 2022.3.62f2 (LTS)
  - Universal Render Pipeline (URP) `com.unity.render-pipelines.universal@14.0.12`
- Input & UI
  - Unity Input System `com.unity.inputsystem@1.14.2`
  - Unity UI (UGUI) `com.unity.ugui@1.0.0`
  - TextMesh Pro `com.unity.textmeshpro@3.0.9`
- Camera & Presentation
  - Cinemachine `com.unity.cinemachine@2.10.5`
- Code & Patterns
  - Reflex (Dependency Injection) `com.gustavopsantos.reflex@13.0.3`
  - UniTask (async/await for Unity) `com.cysharp.unitask`
  - PrimeTween (tweening) `com.kyrylokuzyk.primetween`
- Unity Built-in Modules
  - Physics, Audio, Particles, etc. (standard Unity modules)

Notes:
- Package versions are from `Packages/manifest.json` at commit time.
- Some content (audio, materials, prefabs) is project-specific or Unity-provided defaults; no third-party art assets are required to run.

---

## Project Paths and Key Scripts

- Player
  - `Assets/Scripts/Player/PlayerMoveController.cs` — movement and rolling SFX.
  - `Assets/Scripts/Player/PlayerGroundChecker.cs` — ground check and fall handling.
- Gameplay
  - `Assets/Scripts/GameManager.cs` — win/lose/restart orchestration.
  - `Assets/Scripts/Collectables/CoinManager.cs` — coin collection and best count tracking.
- UI & Flow
  - `Assets/Scripts/UI/Windows/WinLevelWindow.cs`, `LooseWindow.cs`, `WindowBase.cs` — in-game windows.
  - `Assets/Scripts/Scene/SceneLoader.cs`, `LoadingSceneHandler.cs` — scene loading utilities.
  - `Assets/Scripts/VirtualCameraWarp.cs` — helper for camera adjustments.

---

## Running the Project

- Open with Unity 2022.3.62f2.
- Ensure packages resolve (manifest lists all dependencies).
- Open the main scene and press Play.

If you need platform-specific builds, use the `Builds` folder targets or create a new build via `File → Build Settings`.