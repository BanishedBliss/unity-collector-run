# Collector Run - Unity Project
## Description
Collect pirate coins appearing seemingly out of nowhere until life says it's game over.

## How to compile and run
### Perquisites
- Unity 6.0 Editor
### Initial Steps
1. Clone the repository to your desired location
2. Add the project from disk using Unity Hub
3. Open `Assets/Scenes/GameScene.unity`
4. Find `GameRules` object in Hierarchy tree
5. In the nested objects `SpawnManager` and `GameManager` adjust essential game settings such as game duration, collectible spawn interval, etc
6. Click `File` -> `Build and Run` to start the game
7. Repeat steps 5 and 6 until happy with the settings
8. Locate the game build directory (i.e. `/build`) and run it without editor

## Possible improvements
- Character design
- Different collectible types (time-bonus, rare bonus collectible, etc)
- Better terrain texture
- Lighting and shaders
- In-Game control over settings in a form of set game modes or customizable values
- Better organized UI and Game State transition system
- Separate audio sources for each coin, so the sounds overlap instead of being interrupted 

## Behind the scenes
### UI Elements
- Main menu
- HUD with score counter and timer
- Pause screen
- Game Over screen with final score and high score stored over game sessions

### Player
- Versatile input controls with InputSystem plugin, allowing easy support for multiple control schemes and devices (only WASD and arrows implemented)
- Separate visual prefab for precise player position control
- Obstacle hugging allowing the player to slide alongside walls without getting stuck to the slightest angle encountered.

### Collectibles
- Animated prefab with collect effects
- Spawn manager with settings for area, spawn interval, initial collectibles count
- Prefab pool to reduce initialization costs on game object, allowing reuse of the same objects hidden and activated dynamically.
- Sound effect emitters spawning alongside with coins, so the sound doesn't interrupt when the coin gets disabled.
- Event emission on collision with player, that changes game state

### Game State
- Centralized control over UX flow allowing user to stop coin from spawning on game pause
- Level reset on reentering main menu or by clicking the Restart button
- Quitting mechanism working both for Editor environment and standalone application

### Possibly something else I forgot to mention :)

### Used Free Packages
- Collectables Sound Effects Pack _by HoveAudio_
- FREE Stylized PBR Textures Pack _by Lumo-Art 3D_
- Hyper Casual FX _by _Lana Studio_
- Pirate Coin _by DavePixel_
- Stylized Farm Asset Pack - Small _by Easy3D_

## Time taken: 50 hours
