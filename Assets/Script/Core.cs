using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core
{

    GameStateManager gameStateManager;
    DataStore dataStore;
    EventBus eventBus;
    GameManger gameManger;
    public Core() { 

        dataStore = new DataStore();
        eventBus = new EventBus();
        gameManger = new GameManger(eventBus);
        var appControl = ApplicationControl.GetInstance();
        appControl.SetSingleton<EventBus>(eventBus);
        appControl.SetSingleton(dataStore);
        appControl.SetSingleton(gameManger);

        //Load Game Asset
        //Network Setup
        //Work for pre game state setup

        gameManger.GetGameManager<GameStateManager>().SetGameState(GameState.PREGAME);
    }
}
