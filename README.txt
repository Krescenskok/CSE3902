Write up a document with useful information on your project. This might include: program controls, descriptions of known bugs that program has, and details of any tools or processes 
your team used that aren't explicitly required (for example, calculating and using Code Metrics as part of your design process)


Known Bugs:
- Sometimes, Projectiles glitch and are drawing multiple times
- The sword beam expires when it hits the wall, but the animation does not play (it works fine on doors though)
- Link's boomerang draws an impact sprite when it collides with an enemy, but the sprite is both off center and sometimes draws way more impact sprites than necessary
- The game may crash if you spam projectiles a lot


Code Analysis on Link (Done by Krescens)
0 Errors, 8 warnings
- Fields are assigned but never used in Movement, MoveDown, MoveRight, MoveLeft, and MoveUp: Deleted the field


Code Analysis on HUD (done by Ann)
0 errors, 0 warnings


Code Analysis on Inventory (done by Ann)
0 errors, 0 warnings


Code Analysis on Items (done by Ann)
0 errors, 0 warnings

