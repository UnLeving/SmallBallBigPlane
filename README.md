# Small Ball, Big Plane

A small physics-driven ball navigates floating platforms. Collect coins, avoid falling, and reach the finish to win.

---

## How to Play

- Objective:
  - Collect coins scattered in the level and reach the finish without falling off the platforms.
- Controls (Unity Input System):
  - Virtual joystick
  - Keyboard: WASD or Arrow Keys to roll the ball.
  - Gamepad: Left Stick to roll the ball.
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

---

## Running the Project

- Open with Unity 2022.3.62f2.
- Ensure packages resolve (manifest lists all dependencies).
- Open the main scene and press Play.
