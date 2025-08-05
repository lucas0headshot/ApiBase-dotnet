using System;

namespace ApiBase.Core.Common.Bindings
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ComplexBindingResolver : Attribute { }
}
