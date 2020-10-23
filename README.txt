# CSE3902
Sprint 3
Group 5

Task board screenshot is included in the zip file: "TaskBoard"

Known bugs: The collision with Link's shield deflecting the enemy fireballs does not work

Program controls are the same from last game:
- The arrow keys move link, along with wasd. 
- To attack, n or z is pressed, and to change weapons
- Numbers are used to swtich.
- e is used to show Link being damaged.
- u and i are still used to switch between items (this will later on be used for Link's inventory of items)
- o and p are still used to switch between enemies (this will be removed later on, just kept it to debug if needed)
- q is used to quit
- r is used to reset the game

For this sprint, we implemented collisions between all the different objects (blocks, items, Link, and enemies), and implemented the effects of what happens when certain objects collide with each other. 

Andrew and Ann worked on item collisions and cleaned up the code for all related item classes.
Krescens worked on cleaning up code for Link, fixing his movement, created new methods for certain actions, and implemented collisions.
Noah worked on collisions with blocks.
JT created the collision handler and a template for all collisions. Yuan and JT worked to create collisions with enemies and bosses.

Code reviews and analysis were done with FxCop Analyzers

Code review for Link:


Code review for Items:


Code review for Enemies:


Code review for Blocks:
