using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Homework_mvc.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel newKernel)
        {
            kernel = newKernel;
            kernel.Unbind<ModelValidatorProvider>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}