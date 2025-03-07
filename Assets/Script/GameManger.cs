using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Script
{
    public class GameManger
    {
        public Dictionary<Type, object> GameManagerList = new Dictionary<Type, object>();
        private IEventBus _eventBus;
        public GameManger(IEventBus eventBus) {
            _eventBus = eventBus;

            GameManagerList.Add(typeof(GameSceneEntryManager), new GameSceneEntryManager(_eventBus));
            GameManagerList.Add(typeof(GameStateManager), new GameStateManager(_eventBus));
        }

        public T GetGameManager<T>() {
            if (GameManagerList.TryGetValue(typeof(T), out var target))
                return (T)target;
            
            throw new Exception("Cannot Get Game Manger");
        }
    }
}
