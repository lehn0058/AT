using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Objects;
using AT.Data;

namespace AT.Data.UnitTests
{
    [TestClass]
    public class DbContextExtensionsTest
    {
        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_NResults_Test()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                int age = 17;
                int horsePower = 140;

                MultipleResultQuery<Person> peopleQuery = (from p in context.People
                                                           where p.Age > age
                                                           select p).AsMultipleResultQuery();

                MultipleResultQuery<Car> carQuery = (from c in context.Cars
                                                     where c.HorsePower > horsePower
                                                     select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(3, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("John")));
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Jane")));

                Assert.AreEqual(2, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
            }
        }

        #region Init

        [ClassInitialize()]
        public static void ClassInit(TestContext testContext)
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                context.People.Select(s => s).ToList().ForEach(s => context.People.Remove(s));
                context.Cars.Select(s => s).ToList().ForEach(s => context.Cars.Remove(s));
                context.SaveChanges();

                List<Person> people = new List<Person>
                {
                    new Person { Id = 1, FirstName = "Tony", LastName = "Stark", Age = 40, Birthday = DateTime.Today.AddYears(-40), IsMarried = false, IQ = 1000 },
                    new Person { Id = 2, FirstName = "John", LastName = "Doe", Age = 32, Birthday = DateTime.Today.AddYears(-32), IsMarried = true },
                    new Person { Id = 3, FirstName = "Jane", LastName = "Doe", Age = 30, Birthday = DateTime.Today.AddYears(-30), IsMarried = true },
                    new Person { Id = 4, FirstName = "Peter", LastName = "Parker", Age = 16, Birthday = DateTime.Today.AddYears(-16), IsMarried = false }
                };

                people.ForEach(s => context.People.Add(s));

                List<Car> cars = new List<Car>
                {
                    new Car { Id = 1, Model = "Corolla", Year = new DateTime(2009, 1, 1, 0, 0, 0), MilesPerGallon = 35, HorsePower = 158, PersonId = 1 },
                    new Car { Id = 2, Model = "Camry", Year = new DateTime(2009, 1, 1, 0, 0, 0), MilesPerGallon = 32, HorsePower = 170, PersonId = 2 },
                    new Car { Id = 3, Model = "Yaris", Year = new DateTime(2009, 1, 1, 0, 0, 0), MilesPerGallon = 40, HorsePower = 140, PersonId = 3 },
                };

                cars.ForEach(s => context.Cars.Add(s));

                context.SaveChanges();
            }
        }

        #endregion

        #region Context Mapping

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_PropertyMapping()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                MultipleResultQuery<Person> peopleQuery = (from p in context.People
                                                           select p).AsMultipleResultQuery();

                MultipleResultQuery<Car> carQuery = (from c in context.Cars
                                                     select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.IsNotNull(carQuery.Results.Single(s => s.Id == 1).Person.FirstName.Equals("Tony"));
                Assert.IsNotNull(carQuery.Results.Single(s => s.Id == 2).Person.FirstName.Equals("John"));
                Assert.IsNotNull(carQuery.Results.Single(s => s.Id == 3).Person.FirstName.Equals("Jane"));
            }
        }

        #endregion

        #region General

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_SimpleQuery_ReturnsMultipleResults()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                int age = 17;
                int horsePower = 140;

                var peopleQuery = (from p in context.People
                                   where p.Age > age
                                   select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                                where c.HorsePower > horsePower
                                select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(3, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("John")));
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Jane")));

                Assert.AreEqual(2, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
            }
        }

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_JoinQuery_ReturmsMultipleResults()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                int age = 30;
                int horsePower = 170;

                var peopleQuery = (from p in context.People
                                  join c in context.Cars on p.Id equals c.Id
                                  where p.Age > age && c.HorsePower == horsePower
                                  select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                               where c.HorsePower < horsePower
                               select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("John")));

                Assert.AreEqual(2, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Yaris")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
            }
        }

        #endregion

        #region DateTime

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_DateTime_Equal_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                DateTime birthday = DateTime.Today.AddYears(-16);
                DateTime year = new DateTime(2009, 1, 1, 0, 0, 0);

                var peopleQuery = (from p in context.People
                                   where p.Birthday == birthday
                                   select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                                where c.Year == year
                                select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Peter")));

                Assert.AreEqual(3, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Yaris")));
            }
        }

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_DateTime_GreaterThan_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                DateTime birthday = DateTime.Today.AddYears(-18);
                DateTime year = new DateTime(2009, 1, 1, 0, 0, 0);

                var peopleQuery = (from p in context.People
                                   where p.Birthday > birthday
                                   select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                                where c.Year >= year
                                select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Peter")));

                Assert.AreEqual(3, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Yaris")));
            }
        }

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_DateTime_LessThan_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                DateTime birthday = DateTime.Today.AddYears(-18);
                DateTime year = new DateTime(2010, 1, 1, 0, 0, 0);

                var peopleQuery = (from p in context.People
                                  where birthday < p.Birthday
                                  select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                               where c.Year < year
                               select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Peter")));

                Assert.AreEqual(3, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Yaris")));
            }
        }

        #endregion

        #region Int16

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_Int16Collection_Contains_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                List<short> youngAges = new List<short> { 1, 16, 30 };
                List<short> oldAges = new List<short> { 32, 40, 60 };

                var youngQuery = (from p in context.People
                                  where youngAges.Contains(p.Age)
                                  select p).AsMultipleResultQuery();

                var oldQuery = (from p in context.People
                               where oldAges.Contains(p.Age)
                               select p).AsMultipleResultQuery();

                context.MultipleResultSet(youngQuery, oldQuery);

                Assert.AreEqual(2, youngQuery.Results.Count());
                Assert.IsNotNull(youngQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Jane")));
                Assert.IsNotNull(youngQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Peter")));

                Assert.AreEqual(2, oldQuery.Results.Count());
                Assert.IsNotNull(oldQuery.Results.SingleOrDefault(s => s.FirstName.Equals("John")));
                Assert.IsNotNull(oldQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));
            }
        }

        #endregion

        #region Int32

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_Int32_Equals_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                int milesPerGallon = 35;
                int iq = 1000;

                var peopleQuery = (from p in context.People
                                   where p.IQ == iq
                                   select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                                where c.MilesPerGallon == milesPerGallon
                                select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));

                Assert.AreEqual(1, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Corolla")));
            }
        }

        #endregion

        #region String

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_String_Contains_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                string personName = "Tony";
                string carName = "Camry";

                var peopleQuery = (from p in context.People
                                  where p.FirstName.Contains(personName)
                                  select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                               where c.Model.Contains(carName)
                               select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));

                Assert.AreEqual(1, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
            }
        }

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_String_Constant_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                const string personName = "Tony";
                const string carName = "Camry";

                var peopleQuery = (from p in context.People
                                  where p.FirstName.Equals(personName)
                                  select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                               where c.Model.Equals(carName)
                               select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));

                Assert.AreEqual(1, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
            }
        }

        #endregion

        #region Aggregate Functions

        [TestMethod]
        public void DbContextExtensions_MultipleResultSetQuery_MaxMin_Valid()
        {
            using (TestModelEntities context = new TestModelEntities())
            {
                var peopleQuery = (from p in context.People
                                  where p.Age == context.People.Max(s => s.Age)
                                  select p).AsMultipleResultQuery();

                var carQuery = (from c in context.Cars
                               where c.HorsePower == context.Cars.Max(s => s.HorsePower)
                               select c).AsMultipleResultQuery();

                context.MultipleResultSet(peopleQuery, carQuery);

                Assert.AreEqual(1, peopleQuery.Results.Count());
                Assert.IsNotNull(peopleQuery.Results.SingleOrDefault(s => s.FirstName.Equals("Tony")));

                Assert.AreEqual(1, carQuery.Results.Count());
                Assert.IsNotNull(carQuery.Results.SingleOrDefault(s => s.Model.Equals("Camry")));
            }
        }

        #endregion
    }
}
