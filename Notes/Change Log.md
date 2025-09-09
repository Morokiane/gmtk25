#### 08.09.25
- Blacksmith menu working
---
#### 03.09.25
- Blacksmith buttons
- Blacksmith costs
- Realigned menu window
---
#### 01.09.25
- Fixed bug where player would continue to take damage after dying and Master level would not load
- Fixed coins hud not updating after player resses
- Fixed Master door coin display
- Enemies will tick damage if they stay in the players hitbox
- Added loop level text to HUD
- Player respawns after dying like when the Master level loads
- Added floor switch
- Renamed LevelController -> GameController
- Enemy will stun when hit
- Moved Player Input off of player and onto GameController
- You can quit the game now!
- Added speech bubble graphics
- Blacksmith
	- Interaction bubble
	- Interaction works
---
#### 30.08.25
- Fixed level loading when player resurrects
- Player resets stats on res, but coins are messing up
---
#### 28.08.25
- Master door opens with correct coinage
- Started on player death and res
---
#### 27.08.25
- Coins stuff for the master door to open correctly
---
#### 22.08.25
- Breakable pot added with drop
- Added pillar room
---
#### 21.08.25
- Enemy fully functioning
- Master room door opening with coins works now
- Blacksmith sprite added
---
#### 20.08.25
- New enemy controller with A-Star working
---
#### 19.08.25
- A A-Star Grid map to help with creating the Unity grid
- A-Star grid created in Unity
---
#### 18.08.25
- Fixed pursue mover for this style of enemy movement
- A-Star path finding added
    - ConfigureEnemy() disabled in RoomController since it doesn't work with it
---
#### 17.08.25
- Enemies spawn from portals
- Changes the chest to hold more coins
- Pits work better
- Add bleu slime sprites
---
#### 16.08.25
- Chests
	- Change chest spawning to gameobject placement
	- Added better animation when opening
- Changes enemy damage to come the enemy
- Switch room functions
- Pits
	- Player can fall down pits
	- Player has fall animation for pits
- Changed bat shadow to be transparent
- Enemies will spawn when the room loads