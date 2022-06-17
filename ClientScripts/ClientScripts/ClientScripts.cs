using System.Reflection;

namespace Script
{
    public class ClientScripts
    {
        private static List<MethodInfo> GetAllClasses()
        {
            return (from type in Assembly.GetEntryAssembly()
                    ?.GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ClientScripts)))!
                from method in type.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(ClientEvent), false).FirstOrDefault() != null)
                let a = method.GetCustomAttribute(typeof(ClientEvent), false) as ClientEvent
                where a != null
                select method).ToList();
        }

        public static void Invoke(string eventName, params object[] args)
        {
            foreach (var method in GetAllClasses())
            {
                var clientEvent = method.GetCustomAttribute(typeof(ClientEvent), false) as ClientEvent;
                if (clientEvent == null)
                    continue;

                if (clientEvent.EventName.Equals(eventName) == false)
                    continue;

                var declaringType = method.DeclaringType;
                if (declaringType == null)
                    throw new NullReferenceException("Declaring type of searching method is null");

                var obj = Activator.CreateInstance(declaringType);

                method.Invoke(obj, args);

                break;
            }
        }
    }
}