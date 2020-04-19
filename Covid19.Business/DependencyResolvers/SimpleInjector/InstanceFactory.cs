using SimpleInjector;
using System;

namespace Covid19.Business.DependencyResolvers.SimpleInjector
{
    public static class InstanceFactory
    {
        public static Container Container;

        public static T GetInstance<T>()
        {
            if (Container == null)
                throw new ArgumentNullException(nameof(Container));
            return (T)Container.GetInstance(typeof(T));
        }
    }
}
