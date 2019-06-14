namespace Core.Common
{
    using System;

    public static class ResolverFactory
    {
        private static IServiceProvider _serviceProvider { set; get; }

        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
           where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        public static T GetPropValue<T>(this object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
