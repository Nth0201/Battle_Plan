using Assets.Script;
using Assets.Script.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager{
    public GameState CurrentState {  get; private set; }
    private IBaseScene _scene; 
    private IEventBus _eventBus;
    public GameStateManager(IEventBus eventBus) {
        _eventBus = eventBus;
        CurrentState = GameState.NONE;
        _eventBus.Subscribe<EnterGameEvent>(_ => SetGameState(GameState.GAMEPLAY));
        _eventBus.Subscribe<EnterPregameEvent>(_ => SetGameState(GameState.PREGAME));
        _eventBus.Subscribe<EnterPostgameEvent>(_ => SetGameState(GameState.POSTGAME));
    }

    public bool SetGameState(GameState state) {
        if (state == GameState.NONE)
        {
            Console.Write("Incorrect State"); 
            return false;
        }

        if (CurrentState == state)
            return false;

        CurrentState = state;
 
        return true;
    }
    
    private string _LoadSceneMap(GameState state) { 
        switch (state)
        {
            case GameState.NONE:
                return "";
            case GameState.PREGAME:
                return "Scenes/PregameScene";
            case GameState.GAMEPLAY:
                return "Scenes/GameplayScene";
            case GameState.POSTGAME:
                return "Scenes/PostgameScene";
            default:
                return "";
        }
    }

}

