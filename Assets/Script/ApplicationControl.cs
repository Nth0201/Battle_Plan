using System;
using System.Collections.Generic;

namespace Assets.Script
{
    public class ApplicationControl
    {
        private Dictionary<Type,object> _singleton = new Dictionary<Type,object>();
        
        static private ApplicationControl _instance;
        static public ApplicationControl GetInstance() {
            return _instance ?? (_instance = new ApplicationControl());
        }

        public void SetSingleton<T>(T instance) {
            // is it already setup in singleton, 
            if (GetInstance()._singleton.TryGetValue(typeof(T), out var result))
                return;
            GetInstance()._singleton[typeof(T)] = (instance);
        }
        public T GetSingleton<T>()
        {
            if (GetInstance()._singleton.TryGetValue(typeof(T), out var result))
                return (T)result;
            throw new Exception("Get Wrong Singleton");
        }
    }
}
