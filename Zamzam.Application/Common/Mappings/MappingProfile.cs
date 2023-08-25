using AutoMapper;
using System.Reflection;

namespace Zamzam.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplymappingsFromAssemply(Assembly.GetExecutingAssembly());
        }
        private void ApplymappingsFromAssemply(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);
            var mappingMethodname = nameof(IMapFrom<object>.Mapping);
            bool HaseInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HaseInterface)).ToList();
            var argumentType = new Type[] { typeof(Profile) };
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(mappingMethodname);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HaseInterface).ToList();
                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodname, argumentType);
                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }

                }
            }
        }
    }
}
