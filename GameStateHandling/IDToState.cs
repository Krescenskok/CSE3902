using Sprint5.GameStateHandling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class IDToState
    {
        public IDictionary<StateId, IGameStates> IDStateMap { get; set; }

        private static readonly IDToState instance = new IDToState();
        public static IDToState Instance
        {
            get
            {
                return instance;
            }
        }

        private IDToState()
        {
            IDStateMap = new Dictionary<StateId, IGameStates>();
            IDStateMap.Add(StateId.Controls, ControlsState.Instance);
            IDStateMap.Add(StateId.Credits, CreditsState.Instance);
            IDStateMap.Add(StateId.GameOver, GameOverState.Instance);
            IDStateMap.Add(StateId.Gameplay, GameplayState.Instance);
            IDStateMap.Add(StateId.Inventory, InventoryState.Instance);
            IDStateMap.Add(StateId.MainMenu, MainMenuState.Instance);
            IDStateMap.Add(StateId.Win, WinState.Instance);
            IDStateMap.Add(StateId.Pause, PauseState.Instance);
            IDStateMap.Add(StateId.Sound, SoundState.Instance);
            IDStateMap.Add(StateId.Stats, StatsState.Instance);
            IDStateMap.Add(StateId.Transition, TransitionState.Instance);
            IDStateMap.Add(StateId.Options, OptionsState.Instance);
            IDStateMap.Add(StateId.Mode, ModeState.Instance);
            IDStateMap.Add(StateId.Skin, SkinState.Instance);
            IDStateMap.Add(StateId.Difficulty, DifficultyState.Instance);
        }


        public IGameStates State(StateId id)
        {
            return IDStateMap[id];
        }




    }
}
