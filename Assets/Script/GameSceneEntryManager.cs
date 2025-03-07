using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    public class GameSceneEntryManager
    {
        private IEventBus _eventBus;
        private IBaseScene _currentScene;

        public GameSceneEntryManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _currentScene = null; 
            //_eventBus.Subscribe()
        }

        public void OnEnterScene<T>() where T : IBaseScene, new(){ 
            
            _currentScene?.DisposeScene();
            _currentScene = new T();
            _currentScene.SetUp();
        }
    }
}
