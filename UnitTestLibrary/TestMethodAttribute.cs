using System;

namespace UnitTestLibrary
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodAttribute : Attribute
    {
        public bool Ignore { get; set; }
        public Type ExpectedExceptionType { get; set; }

        public TestMethodAttribute()
        {
            Ignore = false;
            ExpectedExceptionType = null;
        }

        public TestMethodAttribute(bool ignore)
        {
            Ignore = ignore;
            ExpectedExceptionType = null;
        }

        public TestMethodAttribute(Type expectedExceptionType)
        {
            Ignore = false;
            ExpectedExceptionType = expectedExceptionType;
        }
    }
}
