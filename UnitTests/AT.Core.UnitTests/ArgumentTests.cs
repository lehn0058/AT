using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AT.Core;
using System.Collections.Generic;

namespace WhereToMeetUnitTests
{
    [TestClass]
    public class ArgumentTests
    {
        #region Argument.NotNull

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_NotNull_NullInput_ThrowsArgumentNullException()
        {
            Func<String> expression = null;
            Argument.NotNull(expression);
        }

        [TestMethod]
        public void Argument_NotNull_Valid_DoesNotThrow()
        {
            DateTime? someParameter = DateTime.UtcNow;
            DateTime? resultParameter = Argument.NotNull(() => someParameter);

            Assert.AreEqual(someParameter, resultParameter);
        }

        [TestMethod]
        public void Argument_NotNull_Throws_ArgumentNullException()
        {
            DateTime? someParameter = null;

            try
            {
                Argument.NotNull(() => someParameter);
                Assert.Fail("ArgumentNullException was not thrown when expected");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("someParameter", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNull_FunctionResult_Valid()
        {
            const int wrappedValue = 54;

            Func<int?> someFunction = () => wrappedValue;

            int? result = Argument.NotNull(someFunction);
            Assert.AreEqual(wrappedValue, result);
        }

        [TestMethod]
        public void Argument_NotNull_ConstNull_ThrowsArgumentNullException()
        {
            const string parameter = null;

            try
            {
                Argument.NotNull(() => parameter);
                Assert.Fail("The expected ArgumentNullException was not raised.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("unknown parameter name - was this a constant value?", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNull_FunctionParameter_DoesNotEvaluateFunction()
        {
            Func<int?> someFunction = () => null;

            Func<int?> result = Argument.NotNull(() => someFunction);
            Assert.AreEqual(someFunction, result);
        }

        #endregion

        #region Argument.NotNull Multiple input Tests

        [TestMethod]
        public void Argument_NotNull_MultipleParameters_Valid()
        {
            DateTime? someParameter1 = DateTime.Now;
            DateTime? someParameter2 = DateTime.Now;
                        
            Argument.NotNull(() => someParameter1, () => someParameter2);
        }

        [TestMethod]
        public void Argument_NotNull_FirstParameterNull_Throws_ArgumentNullException()
        {
            DateTime? someParameter1 = null;
            DateTime? someParameter2 = DateTime.Now;

            try
            {
                Argument.NotNull(() => someParameter1, () => someParameter2);
                Assert.Fail("ArgumentNullException was not thrown when expected");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("someParameter1", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNull_SecondParameterNull_Throws_ArgumentNullException()
        {
            DateTime? someParameter1 = DateTime.Now;
            DateTime? someParameter2 = null;

            try
            {
                Argument.NotNull(() => someParameter1, () => someParameter2);
                Assert.Fail("ArgumentNullException was not thrown when expected");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("someParameter2", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNull_BothParametersNull_Throws_ArgumentNullException()
        {
            DateTime? someParameter1 = null;
            DateTime? someParameter2 = null;

            try
            {
                Argument.NotNull(() => someParameter1, () => someParameter2);
                Assert.Fail("ArgumentNullException was not thrown when expected");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("someParameter1", ex.ParamName);
            }
        }

        #endregion

        #region Argument.NotNullOrEmpty

        [TestMethod]
        public void Argument_NotNullOrEmpty_Valid_DoesNotThrow()
        {
            List<object> objectCollection = new List<object> { DateTime.Now };
            List<string> stringCollection = new List<string> { "Testing" };
            List<int> integerCollection = new List<int> { 1, 2, 3 };

            Argument.NotNullOrEmpty(() => objectCollection, () => stringCollection, () => integerCollection);
        }

        [TestMethod]
        public void Argument_NotNullOrEmpty_Valid_LastItemNull()
        {
            List<object> objectCollection = new List<object> { DateTime.Now };
            List<string> stringCollection = new List<string> { "Testing" };
            List<int> integerCollection = null;

            try
            {
                Argument.NotNullOrEmpty(() => objectCollection, () => stringCollection, () => integerCollection);
                Assert.Fail("The expected argument null exception was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("integerCollection", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNullOrEmpty_Valid_MiddleItemEmpty()
        {
            List<object> objectCollection = new List<object> { DateTime.Now };
            List<string> stringCollection = new List<string>();
            List<int> integerCollection = null;

            try
            {
                Argument.NotNullOrEmpty(() => objectCollection, () => stringCollection, () => integerCollection);
                Assert.Fail("The expected argument exception was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("stringCollection", ex.ParamName);
            }
        }

        #endregion

        #region Argument.NotNullOrEmptyOrWhiteSpace

        [TestMethod]
        public void Argument_NotNullOrEmptyOrWhiteSpace_Valid_DoesNotThrow()
        {
            const string stringName = "string1";

            String result = Argument.NotNullOrEmptyOrWhiteSpace(() => stringName);
            Assert.AreEqual("string1", result);
        }

        [TestMethod]
        public void Argument_NotNullOrEmptyOrWhiteSpace_Valid_ThrowsArgumentNullException()
        {
            string string1 = null;

            try
            {
                Argument.NotNullOrEmptyOrWhiteSpace(() => string1);
                Assert.Fail("The expected argument null exception was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("string1", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNullOrEmptyOrWhiteSpace_MultipleParameters_Throws()
        {
            const string string1 = "string1";
            string string2 = string.Empty;
            const string string3 = null;

            try
            {
                Argument.NotNullOrEmptyOrWhiteSpace(() => string1, () => string2, () => string3);
                Assert.Fail("The expected argument exception was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("string2", ex.ParamName);
            }
        }

        #endregion

        #region Argument.NotNullIsBetween

        [TestMethod]
        public void Argument_NotNullIsBetween_Valid()
        {
            int negativeTen = -10;
            int positiveTen = 10;
            int five = 5;

            int result = Argument.NotNullIsBetween(negativeTen, positiveTen, () => five);
            Assert.AreEqual(five, result);
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_OutOfBounds_Lower()
        {
            int negativeTen = -10;
            int positiveTen = 10;
            int negativeTwenty = -20;

            try
            {
                int result = Argument.NotNullIsBetween(negativeTen, positiveTen, () => negativeTwenty);
                Assert.Fail("The expected ArgumentOutOfRangeExeception was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("negativeTwenty", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_OutOfBounds_Upper()
        {
            int negativeTen = -10;
            int positiveTen = 10;
            int positiveTwenty = 20;

            try
            {
                int result = Argument.NotNullIsBetween(negativeTen, positiveTen, () => positiveTwenty);
                Assert.Fail("The expected ArgumentOutOfRangeExeception was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("positiveTwenty", ex.ParamName);
            }
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_InBoundsInclusive_Lower()
        {
            int negativeTen = -10;
            int positiveTen = 10;

            int result = Argument.NotNullIsBetween(negativeTen, positiveTen, () => negativeTen);
            Assert.AreEqual(negativeTen, result);
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_InBoundsInclusive_Upper()
        {
            int negativeTen = -10;
            int positiveTen = 10;

            int result = Argument.NotNullIsBetween(negativeTen, positiveTen, () => positiveTen);
            Assert.AreEqual(positiveTen, result);
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_MultipleItems_Valid()
        {
            int negativeTen = -10;
            int positiveTen = 10;

            Argument.NotNullIsBetween(negativeTen, positiveTen, () => 1, () => -10, () => 0);
        }

        [TestMethod]
        public void Argument_NotNullIsBetween_MultipleItems_NotValid()
        {
            int negativeTen = -10;
            int positiveTen = 10;
            int invalid = -100;

            try
            {
                Argument.NotNullIsBetween(negativeTen, positiveTen, () => 1, () => 10, () => -3, () => invalid);
                Assert.Fail("The expected ArgumentOutOfRangeException was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("invalid", ex.ParamName);
            }
        }

        #endregion
    }
}
