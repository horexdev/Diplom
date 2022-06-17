using System.Reflection;

namespace Script
{
    public class ServerScripts
    {
        private static List<MethodInfo> GetAllClasses()
        {
            return (from type in Assembly.GetEntryAssembly()
                    ?.GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ServerScripts)))!
                from method in type.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(ServerEvent), false).FirstOrDefault() != null)
                let a = method.GetCustomAttribute(typeof(ServerEvent), false) as ServerEvent
                    where a != null
                select method).ToList();
        }

        public static void Invoke(object handler, string eventName, params object[] args)
        {
            foreach (var method in GetAllClasses())
            {
                var serverEvent = method.GetCustomAttribute(typeof(ServerEvent), false) as ServerEvent;
                if (serverEvent == null)
                    continue;

                if (serverEvent.EventName.Equals(eventName) == false)
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