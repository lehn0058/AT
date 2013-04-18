using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AT.Core.UnitTests
{
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void IEnumerableExtensions_ForEach_NullAction()
        //{
        //    IEnumerable<int> collection = new List<int>();
        //    Action<int> action = null;
        //    collection.ForEach(action);
        //}

        [TestMethod]
        public void IEnumerableExtensions_ForEach_EmptyCollection()
        {
            IEnumerable<int> collection = new List<int>();
            collection.ForEach(s =>
                {
                    Assert.Fail("the collection is empty.");
                });

            Assert.AreEqual(0, collection.Count());
        }

        [TestMethod]
        public void IEnumerableExtensions_ForEach_SingleItem()
        {
            int counter = 0;
            IEnumerable<int> collection = new List<int> { 1 };
            collection.ForEach(s =>
            {
                counter += s;
            });

            Assert.AreEqual(counter, collection.Count());
        }

        [TestMethod]
        public void IEnumerableExtensions_ForEach_MultipleItems()
        {
            int counter = 0;
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            collection.ForEach(s =>
            {
                counter += s;
            });

            Assert.AreEqual(counter, 6);
        }
    }
}
