namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    public class Locator
    {
        private static Dictionary<Type, object> instances = new Dictionary<Type, object>();

        public static void Bind<T, T2>(T2 instance) 
            where T2 : T
        {
            instances.Add(typeof(T), instance);
        }

        public static T Get<T>()
        {
            return (T)instances[typeof(T)];
        }
    }
}