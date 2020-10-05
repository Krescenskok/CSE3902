# CSE3902
Creating Legend of Zelda, level 1


Write up a document with useful information on your project. This might include: program controls, descriptions of known bugs that program has, and details of any tools or processes your team used that aren't explicitly required (for example, calculating and using Code Metrics as part of your design process)



Code Review For Link:
No errors, 35 warnings, and 151 messages

Warnings:
-ItemsStateMachine: The field 'ItemsStateMachine.currentItem' is never used
  -> Removed the field
  
-LinkItems: The field 'LinkItems.GoingForward' is never used
  -> Removed the field
  
-Each Item state in States: The field '"itemStateName".currentFrame' is never used
  -> Removed the field and initialization
  
-Game: The fields 'Game1.donkey', 'Game1.keyboardCurrent', 'Game1.delay' are never used
  -> removed the fields
  
-KeyboardController: The fields 'KeyboardController.goingForward', 'KeyboardController.itemsCommand', and 'KeyboardController.spriteBatch' are never used
  -> removed the fields
  
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
