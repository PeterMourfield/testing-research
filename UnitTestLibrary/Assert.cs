using System;

namespace UnitTestLibrary
{
    public static class Assert
    {
        public static void ShouldEqual(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new ApplicationException(string.Format("The expected value {0} doesn't equal the actual value {1}", expected, actual));
            }
        }
    }
}
