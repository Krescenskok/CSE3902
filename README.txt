# CSE3902
Sprint 3
Group 5

Task board screenshot is included in the zip file with the name: "TaskBoard"

Known bugs/issues:
- The collision with Link's shield deflecting the enemy fireballs does not work
- The impact animation for the sword beam does not have the blinking projectiles spread out.
- Link can only have one projectile (arrow, wand beam, sword beam, boomerang) actively flying in the air at a time. (In other words: if you try to shoot 2 sword beams/wand beams/arrows in a row, the second projectile sprite will not draw. It is only when the first fired projectile expires that a second can be drawn.) 
- The explosion animation for the bomb is not very smooth.

Notes on the damage on Link from enemies:
- We decided to make Link's full health 60hp because of the blue ring. Since the blue ring makes the damage decrease by half, this would be easier to implement using ints instead of doubles. So the enemy damage is either 10 or 20, so dividing by 2 will still be an int.
- Each heart for Link represents 20hp.

Notes on the items Link uses:
- When Link uses the blue ring, he changes his color to aquamarine. There are no sprites with Link's tunic being white, so we decided to tint his entire sprite to be a different color and represent that the blue ring has been used.
- The sword beam and the wand beams colors have various color variations based on Link's tunic color. We are choosing to stick with only one color palette for those items.

Program controls are the same from last game:
- The arrow keys move link, along with wasd. 
- To attack, n or z is pressed, and to change weapons
- Numbers are used to switch.
- e is used to show Link being damaged.
- u and i are still used to switch between items (this will later on be used for Link's inventory of items)
- o and p are still used to switch between enemies (this will be removed later on, just kept it to debug if needed)
- q is used to quit
- r is used to reset the game

For this sprint, we implemented collisions between all the different objects (blocks, items, Link, and enemies), and implemented the effects of what happens when certain objects collide with each other. 

Andrew and Ann worked on item collisions and cleaned up the code for all related item classes.
Krescens worked on cleaning up code for Link, fixing his movement, created new methods for certain actions, and implemented collisions.
Krescens and Ann worked on making Link use items (arrow, boomerang, etc.). 
Noah worked on collisions with blocks and cleaning up the code for all related block classes.
JT created the collision handler and a template for all collisions.
Yuan and JT worked to create collisions with enemies and bosses.
Andrew and JT created and worked on the basic structure for file reading and room drawing.


Code reviews and analysis were done with FxCop Analyzers.


Code review for Link:


Code review for Items:


Code review for Enemies:


Code review for Blocks:
