using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    public class SubscriptionToken
    {
        public readonly Guid Token;
        public readonly Type TEventType;

        public SubscriptionToken(Type type) { 
            Token = Guid.NewGuid();
            TEventType = type;
        }
    }
}
