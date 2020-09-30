# CSE3902
Creating Legend of Zelda, level 1


Code Review For Link:
No errors, 35 warnings, and 151 messages

Warnings:
- LinkPlayer: Member 'damagedStartTime' is explicitly initialized to its default value

  -> Removed the initialization
  
- MoveLeft/MoveRight/MoveUp/MoveDown: Member 'lastTime' is explicitly initialized to its default value

  -> Removed the initialization
  
The remaining warnings:
- In externally visible method 'Vector2 MoveDown.HandleMagicalRod(GameTime gameTime, Vector2 location)', validate parameter 'gameTime' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.

  -> This warning message appears 21 times. It cannot be removed because gameTime is used to keep track of how often the program updates, but it will be null initially.
  
- Member 'i' is explicitly initialized to its default value

  -> i needs to be initialized because it's the index of the array of colors used for tinting when Link is damaged.
  
- Do not declare visible instance fields

  -> This warning message appears 4 times. These variables are protected, which are needed since it is only visible to "Movement" and all the classes that inherit it.
  
- Member 'loc' is explicitly initialized to its default value
- Member 'isAttacking' is explicitly initialized to its default value 
- Member 'isDamaged' is explicitly initialized to its default value 
- Member 'isStopped' is explicitly initialized to its default value 

  -> These initializations are all necessary for the different states that Link appears in
