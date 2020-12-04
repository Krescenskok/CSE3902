Write up a document with useful information on your project. This might include: program controls, descriptions of known bugs that program has, and details of any tools or processes 
your team used that aren't explicitly required (for example, calculating and using Code Metrics as part of your design process)

Program Controls:
- W and Up arrow: Move up
- A and Left arrow: Move left
- S and Down arrow: Move down
- D and Right arrow: Move right
- U and I: Used to switch between items in inventory
- Space: Brings up HUD and Link's inventory
- N: Primary Attack
- B: Secondary Attack
- G: Pause the game
- Q: Quit the game
- F: Full Screen
- M: Mute


Known Bugs:
- If the sword beam hits the very edges of the door entrance, it may not show the impact animation, but it will expire.
- Link does not hold the triforce/bow over his head.
- Link does not pick up boomerang with one hand.



Code Analysis on Link (Done by Krescens)
0 Errors, 10 warnings
- Fields are assigned but never used in MoveUp, MoveDown, MoveLeft, MoveRight, Movement, Projectiles, ProjectilesFactory, Stationary. These resolved by removing the variables.
- In MoveUp, currentFrames is stated that it is never used, however it is used so it has been kept.





