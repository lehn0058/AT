 Compact parameter validation
=============================
Validate your parameters with minimal code using [AT.Core.Argument](https://github.com/lehn0058/AT/wiki/AT.Core.Argument)

    Argument.NotNull(() => arg1);



EF Multiple Result Sets
=======================
Execute SQL queries with multiple result sets using [AT.Data.DbContextExtensions](https://github.com/lehn0058/AT/wiki/AT.Data.DbContextExtensions)

    Tuple<IEnumerable<Person>, IEnumerable<Car>> results = context.MultipleResultSet(peopleQuery, carQuery);
