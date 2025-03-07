using Assets.Script.Event;
using System;
using UnityEngine.SceneManagement;

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
            _eventBus.Subscribe<EnterGameState>(_ => OnEnterScene<GameplayScene>());
            _eventBus.Subscribe<EnterPregameState>(_ => OnEnterScene<PregameScene>());
            _eventBus.Subscribe<EnterPostgameState>(_ => OnEnterScene<PostgameScene>());
        }

        public void OnEnterScene<T>() where T : IBaseScene, new(){ 
            
            _currentScene?.Dispose();
            _currentScene = new T();
            _currentScene.SetUp();
            _LoadSceneMap(_currentScene);
        }

        private void _LoadSceneMap(IBaseScene state)
        {
            if (state is PregameScene)
                SceneManager.LoadScene( "Scenes/PregameScene");
            if (state is GameplayScene)
                SceneManager.LoadScene("Scenes/GameplayScene");
            if (state is PostgameScene)
                SceneManager.LoadScene("Scenes/PostgameScene");
            throw new Exception("Unexpect Scene");
        }
    }
}
