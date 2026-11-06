# ITU CS464 - Lab 05: Physics Forces & Collectibles (Roll-a-Ball)

**Course:** CS464 — Game Development  
**Institution:** Information Technology University (ITU)  
**Student Roll Number:** BSCS24088  
**Unity Version:** Unity 6 (6000.6.0f1)  

---

## 🎮 Project Overview

This repository contains the complete implementation of the **Roll-a-Ball** game discussed in **CS464 Lecture 06: Physics Forces & Collectibles**, along with the 3 required bonus tasks.

The player controls a physics-driven sphere collecting coins spawned randomly across the arena while managing game state, score progression, win conditions, and bonus events.

---

## 🚀 Tasks Implemented

### 1. Base Game (Lecture 06 Replication)
- **Physics-Based Movement (`PlayerController.cs`):** RigidBody-driven movement utilizing `rb.AddForce(input * moveForce, mode)` with clamped input vectors (`Vector3.ClampMagnitude`) to ensure consistent diagonal speeds.
- **Dynamic Coin Spawner (`Spawner.cs`):** Spawns collectible coins periodically using `InvokeRepeating` within arena bounds, enforcing a minimum clearance distance from the player and capping active coins.
- **Collectible Coin Mechanics (`Coin.cs`):** Continuously rotating trigger objects (`transform.Rotate`) that detect player collision via `OnTriggerEnter` and notify the game manager.
- **Game State & UI (`GameManager.cs`):** Singleton manager coordinating current score, target score, UI updates via TextMesh Pro (`TMP_Text`), win panel display, and scene restarts.

---

### 2. Bonus Tasks

#### 🎯 Task 1: Red Coin Spawn (Score 1–10)
- A special Red Coin is programmed to spawn **only once** per session at a randomized score threshold between **1 and 10**.
- Handled via `Spawner.cs` and `RedCoin.cs`. When the player reaches the randomly chosen score threshold, the red coin is instantiated in the arena safely away from the player.

#### 🔵 Task 2: 50 Blue Coins Circular Burst
- Upon the player collecting the Red Coin, a burst of **50 Blue Coins** is dynamically instantiated in a circular radius ($r = 6.0\text{m}$) surrounding the player's exact coordinates.
- Computed in `GameManager.TriggerBlueCoinBurst` using trigonometry:
  $$\text{Offset}_i = \begin{bmatrix} \cos(\theta_i) \cdot r \\ 0 \\ \sin(\theta_i) \cdot r \end{bmatrix}, \quad \text{where } \theta_i = \frac{360^\circ}{50} \times i$$

#### 📊 Task 3: Hidden Bonus Coins HUD (`BonusCoins: 0/50`)
- The Bonus Coins HUD (`bonusCoinPanel`) remains completely hidden during regular gameplay.
- Collecting the Red Coin triggers the burst and unhides the TextMesh Pro element, displaying:
  ```
  BonusCoins: 0/50
  ```
- Each collected `BlueCoin` increments the bonus counter and updates the text display in real-time until all 50 coins are collected.

---

## 📂 Project Architecture (`Assets/Scripts/`)

| Script | Purpose |
| :--- | :--- |
| [`PlayerController.cs`](Assets/Scripts/PlayerController.cs) | Applies directional physics force to the ball Rigidbody based on axis inputs. |
| [`Coin.cs`](Assets/Scripts/Coin.cs) | Rotates the standard coin, handles trigger collision with player, and awards points. |
| [`RedCoin.cs`](Assets/Scripts/RedCoin.cs) | Special collectible that awards bonus score and triggers the 50 blue coin circular burst. |
| [`BlueCoin.cs`](Assets/Scripts/BlueCoin.cs) | Bonus coin collectible that advances the `BonusCoins: X/50` counter. |
| [`Spawner.cs`](Assets/Scripts/Spawner.cs) | Manages timed normal coin generation and executes the one-time random red coin spawn. |
| [`GameManager.cs`](Assets/Scripts/GameManager.cs) | Singleton coordinator managing main score, win state, blue coin burst generation, and bonus HUD. |

---

## 🛠️ How to Run

1. Clone this repository:
   ```bash
   git clone https://github.com/Nooru-dotcom/ITU-CS-464-LAB-05-bscs24088.git
   ```
2. Open **Unity Hub** and select **Add project from disk**.
3. Select the repository root folder.
4. Ensure the Editor version is set to **Unity 6 (6000.6.0f1)** or compatible.
5. Open `Assets/Scenes/SampleScene.unity`.
6. Press the **Play** button in the Editor to test the game.
   - Use **WASD** or **Arrow Keys** to steer the ball.
