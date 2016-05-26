using System;
using System.Linq;
using System.Reflection;

namespace SocketUtilities.Messaging
{
    public class TypeDto
    {
        public string Assemblyname;
        public string ClassName;

        public static TypeDto FromType(Type type)
        {
            return new TypeDto
            {
                Assemblyname = type.Assembly.FullName,
                ClassName = type.FullName
            };
        }

        public Type ToType()
        {
            return ToType(AppDomain.CurrentDomain);
        }

        public Type ToType(AppDomain appDomain)
        {
            Assembly assembly = appDomain.GetAssemblies().Single(t => t.FullName == Assemblyname);
            return assembly.GetType(ClassName);
        }
    }
}