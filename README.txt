Write up a document with useful information on your project. This might include: program controls, descriptions of known bugs that program has, and details of any tools or processes 
your team used that aren't explicitly required (for example, calculating and using Code Metrics as part of your design process)

Program Controls:
- W and Up arrow: Move up
- A and Left arrow: Move left
- S and Down arrow: Move down
- D and Right arrow: Move right
- U and I: Used to switch between items in inventory
- Enter: Consume selected item in inventory (applicable only for the potion)
- Space: Brings up and closes HUD and Link's inventory
- N: Primary Attack
- B: Secondary Attack
- G: Pause the game
- Q: Quit the game
- F: Full Screen
- M: Mute Audio


Known Bugs:
- If the sword beam hits the very edges of the door entrance, it may not show the impact animation, but it will expire.
- Link does not hold the triforce/bow over his head (but he will hold his hands up).
- Link does not pick up boomerang with one hand.


Notes:
- In the inventory, the bow is intended to be drawing over the arrow once obtained, but only the bow should show up in the slot selection. 
- The Magic Book item is used as an invincibility item (it is not the same as the original game implementation). When Link collects it, he will be invincible for a short moment, and if he runs into enemies while invincible, he will damage them. 


Code Analysis on Link (Done by Krescens)
0 Errors, 10 warnings
- Fields are assigned but never used in MoveUp, MoveDown, MoveLeft, MoveRight, Movement, Projectiles, ProjectilesFactory, Stationary. These resolved by removing the variables.
- In MoveUp, currentFrames is stated that it is never used, however it is used so it has been kept.


Code Analysis on Inventory (Done by Ann)
0 Errors, 0 Warnings
- Use a switch caise. However, a switch case IS being used 


Code Analysis on HUD (Done by Ann)
0 errors, 3 warnings
- Fields are assigned but never used in HeartManagement. These were resolved by removing them.


Code Analysis on Items (Done by Ann)
0 errors, 13 warnings
- Colliders on Empty, Full, and Half Heart, as well as BombObject, BoomerangObject will always be null. These objects are HUD interface sprites, and thus shouuld never have colliders (the colliders are simply part of the interface so they cannot be removed).
- Fields are assigned but never used in BombCollider, BoomerangCollider, ItemCollider, BoomerangObject and BombExplosionState. Solved by removing.




