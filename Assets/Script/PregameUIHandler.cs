using Assets.Script;
using Assets.Script.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class PregameUIHandler : MonoBehaviour
    {
        public Button startButton;
        public void Start()
        {
            startButton.onClick.AddListener(OnClickEnterGame);
        }

        public void OnClickEnterGame() {
            ApplicationControl.GetInstance().GetSingleton<Assets.Script.EventBus>().Publish( new EnterGameEvent() );
        }
    }
}
