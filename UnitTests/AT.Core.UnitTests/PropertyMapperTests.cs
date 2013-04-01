using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Core;

namespace AT.Core.UnitTests
{
    [TestClass]
    public class PropertyMapperTests
    {
        private class MapperTest
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? Age { get; set; }
        }

        [TestMethod]
        public void PropertyMapper_Map_AllProperties_Success()
        {
            int id = 1;
            string name = "John";
            DateTime age = DateTime.Now;

            MapperTest entityFrom = new MapperTest { Id = id, Name = name, Age = age };
            MapperTest entityTo = new MapperTest();

            PropertyMapper.Map(entityFrom, entityTo);

            Assert.AreEqual(id, entityTo.Id);
            Assert.AreEqual(name, entityTo.Name);
            Assert.AreEqual(age, entityTo.Age);

            Assert.AreEqual(id, entityFrom.Id);
            Assert.AreEqual(name, entityFrom.Name);
            Assert.AreEqual(age, entityFrom.Age);
        }

        [TestMethod]
        public void PropertyMapper_MapNull_OneNull_OthersSet()
        {
            int toId = 1;
            string toName = "John";

            int fromId = 4;
            string fromName = "Sam";
            DateTime fromAge = DateTime.Now;

            MapperTest entityTo = new MapperTest { Id = toId, Name = toName };
            MapperTest entityFrom = new MapperTest { Id = fromId, Name = fromName, Age = fromAge };

            PropertyMapper.MapNull(entityFrom, entityTo);

            Assert.AreEqual(toId, entityTo.Id);
            Assert.AreEqual(toName, entityTo.Name);
            Assert.AreEqual(fromAge, entityTo.Age);

            Assert.AreEqual(fromId, entityFrom.Id);
            Assert.AreEqual(fromName, entityFrom.Name);
            Assert.AreEqual(fromAge, entityFrom.Age);
        }
    }
}
