using Assets.Script;
using Assets.Script.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PregameScene : MonoBehaviour, IBaseScene
{
    public void DisposeScene()
    {
    }

    public void SetUp()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickEnterGame() {
        ApplicationControl.GetInstance().GetSingleton<EventBus>().Publish( new EnterGameEvent() );
    }
}
