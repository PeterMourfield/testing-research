using System;

namespace UnitTestLibrary
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ExpectedExceptionAttribute : Attribute
    {
        public Type ExpectedType { get; set; }

        public ExpectedExceptionAttribute(Type type)
        {
            ExpectedType = type;
        }
    }
}
