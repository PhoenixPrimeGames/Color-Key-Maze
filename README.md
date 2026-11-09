# Color-Key Maze

**Top-down 2D maze prototype | Unity 6.3 | C# | Personal project, 2026**

Color-Key Maze is a playable Unity prototype focused on movement, collection and interaction systems. The player navigates a maze, collects color-coded keys, unlocks matching barriers, and attempts to reach the exit before the countdown expires. Each newly unlocked color halves the time remaining, adding a time-management tradeoff to progression.

## Gameplay features

- **Movement:** WASD or arrow-key movement using `Rigidbody2D` and normalized directional input.
- **Sprint:** Hold Left Shift while moving for a configurable speed multiplier. A separate UI component displays the sprinting state.
- **Keys and barriers:** Collect colored keys and use them to unlock barriers. A color unlock opens matching barriers and removes remaining pickups of that color.
- **Interaction:** Press **E** to interact with a nearby selected barrier; use **Tab** to cycle among nearby barriers.
- **Game state:** Reach the exit to win; running out of time ends the attempt. End screens and restart behavior are managed separately from movement.
- **HUD:** Displays remaining time, collected key colors, unlocked colors, and sprint status.

The color system defines seven color values (red, orange, yellow, green, blue, indigo and violet). This describes what the code supports, not necessarily the number of colors placed in a particular scene.

## Controls

| Input | Action |
| --- | --- |
| `W`, `A`, `S`, `D` or arrow keys | Move |
| Left Shift + movement | Sprint |
| `E` | Interact with selected nearby barrier |
| `Tab` | Cycle among nearby barriers |

## How to open the project

1. Clone this repository:
   ```bash
   git clone https://github.com/PhoenixPrimeGames/Prototype-Maze-Game-V1.git
   ```
2. Open the cloned project folder in **Unity Hub**. The project was saved with **Unity Editor 6000.3.24f1** (Unity 6.3).
3. Open `Assets/Scenes/MovementTest.unity`. This is the scene enabled in the project's build settings.
4. Press **Play** in the Unity Editor.

The repository contains Unity project source and assets. It does not currently provide a downloadable packaged game build.

## Architecture and implementation

| Script | Responsibility |
| --- | --- |
| `PlayerMovement.cs` | Input, normalized direction, 2D rigidbody velocity, sprint-state changes |
| `PlayerInventory.cs` | Tracks currently held key colors in a `HashSet<KeyColor>` and emits collection/consumption events |
| `KeyPickup.cs` | Handles collection and removes pickups when their color is unlocked |
| `PlayerInteraction.cs` and `BarrierInteractionZone.cs` | Track nearby barriers, handle selection and interactions |
| `ColorUnlockManager.cs` | Stores permanently unlocked colors, changes the timer, broadcasts unlock events |
| `ColorBarrier.cs` | Checks key requirements and opens barriers when their color is unlocked |
| `GameTimer.cs` and `GameManager.cs` | Countdown, win/loss transitions, and movement shutdown |
| `KeyInventoryUI.cs`, `ColorUnlockedUI.cs`, `TimerUI.cs`, `SprintIndicatorUI.cs` | Present player inventory, unlocks, time and sprint status |

The game separates **temporary inventory** (`collectedKeys`) from **persistent unlock state** (`unlockedColors`). C# events notify other components when a key is collected, consumed, or unlocked, or when sprinting starts or stops. Components that need existing unlock information can query the saved state rather than relying solely on having received an earlier event.

## Recent iteration: sprinting

The movement system was updated to normalize diagonal input and support a serialized `sprintMultiplier` (default **1.5**). A `SprintingChanged` event fires on state transitions, and the `SprintIndicatorUI` subscriber shows or hides the sprint label. Gameplay tests in Unity confirmed the change to movement speed and indicator behavior.

## Screenshots

Gameplay screenshots are in the screenshots folder. Captures include an overview of the maze and HUD, key/barrier interaction, unlocked barrier state, sprint indicator, and a completed run.

## Development process and AI assistance

This is a personal learning project developed with AI assistance, including ChatGPT and Codex for programming guidance, troubleshooting, and code review. Implementation details were reviewed and gameplay behavior tested in Unity. During the documented sprint iteration, I implemented and tested changes to movement, persistent sprint state, event notifications, and the separate UI subscriber following design discussions and feedback.

This repository is a **gameplay prototype**, not a finished commercial game. Source files document the current implementation; packaged binaries, a complete automated test suite, and production readiness are not claimed.
