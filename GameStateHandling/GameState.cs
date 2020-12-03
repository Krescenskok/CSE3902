using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class GameState
    {


        public IGameStates.Type Id { get; set; }

        public IDifficulty.Level Difficulty { get; set; }

        public GameState()
        {

        }

    }
}
