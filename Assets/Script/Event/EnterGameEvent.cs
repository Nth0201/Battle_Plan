using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Script.Event
{
    public class EnterGameEvent : EventItem {  }
    public class EnterPregameEvent : EventItem { }
    public class EnterPostgameEvent : EventItem { }
    public class EnterGameState : EventItem {  }
    public class EnterPregameState : EventItem { }
    public class EnterPostgameState : EventItem { }
    public class EnterLoadingSceneEvent : EventItem { }
    public class LeaveLoadingSceneEvent : EventItem { }
}
