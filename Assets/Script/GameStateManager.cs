using Assets.Script;
using Assets.Script.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager{
    public GameState CurrentState {  get; private set; }
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

        switch (CurrentState)
        {
            case GameState.GAMEPLAY:
                _eventBus.Publish(new EnterGameState());
                break;
            case GameState.PREGAME:
                _eventBus.Publish(new EnterPregameState());
                break;
            case GameState.POSTGAME:
                _eventBus.Publish(new EnterPostgameState());
                break;
        }
        
        return true;
    }
    

}

