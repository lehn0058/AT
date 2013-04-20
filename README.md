 Compact parameter validation
=============================
Validate your parameters with minimal code using [AT.Core.Argument](https://github.com/lehn0058/AT/wiki/AT.Core.Argument)

    // Instead of writing this:
    if (arg1 == null)
    {
         throw new ArgumentNullException("arg1", "argument can not be null");
    }
    
    if (arg2 == null)
    {
         throw new ArgumentNullException("arg2", "argument can not be null");
    }
    
    if (arg3 == null)
    {
         throw new ArgumentNullException("arg3", "argument can not be null");
    }

    // Use this simplification instead:
    Argument.NotNull(() => arg1, () => arg2, () => arg3);



EF Multiple Result Sets
=======================
Execute SQL queries with multiple result sets using [AT.Data.DbContextExtensions](https://github.com/lehn0058/AT/wiki/AT.Data.DbContextExtensions)

    Tuple<IEnumerable<Person>, IEnumerable<Car>> results = context.MultipleResultSet(peopleQuery, carQuery);
    
    MultipleResultQuery<Person> peopleQuery = (from p in context.People
                                               where p.Age > age
                                               select p).AsMultipleResultQuery();

    MultipleResultQuery<Car> carQuery = (from c in context.Cars
                                         where c.HorsePower > horsePower
                                         select c).AsMultipleResultQuery();

    // Single database round trip
    context.MultipleResultSet(peopleQuery, carQuery);
    
    IEnumerable<Person> peopleResults = peopleQuery.Results;
    IEnumerable<Car> carResults = carQuery.Results;
