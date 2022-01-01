# Ninject.Extensions.Interception.AttributeProxy

Allows to proxy any methods in your application. You only need to apply a custom attribute to the methods yout want to intercept and then register that attribute to your custom interceptor.

## Usage
```csharp
    using System;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.ProxyAttributes;
    using Ninject.Modules;

    public class InterceptAttribute : Attribute
    {

    }

    public class MyService
    {
        [Intercept]
        public void DoSomething()
        {

        }
    }

    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            // Do before

            invocation.Proceed();

            // Do after
        }
    }

    public class MyNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.AddInterceptor<InterceptAttribute, Interceptor>();
        }
    }
```

