Compact parameter validation using AT.Core.Argument
===================================================

Often times, I found myself writing parameter validation code like this:

        public void MyMethod(MyObject arg1)
        {
            if (arg1 == null)
            {
                throw new ArgumentNullException("arg1", "The parameter can not be null");
            }
  
	          ...
        }

While this is simple enough, when someone has to do a lot of validation, it can become a lot of typing, and a lot of code, especially when there multiple parameters:

        public void MyMethod(MyObject arg1, MyObject arg2, DateTime? arg3)
        {
            if (arg1 == null)
            {
                throw new ArgumentNullException("arg1", "The parameter can not be null");
            }

            if (arg2 == null)
            {
                throw new ArgumentNullException("arg2", "The parameter can not be null");
            }

            if (arg3 == null)
            {
                throw new ArgumentNullException("arg3", "The parameter can not be null");
            }

	          ...
        }

To reduce this clutter and unnecessary typing, I wrote the Argument class. This class validates each parameter you pass along in a compact format. For instance, our first example above would become:
       
       public void MyMethod(MyObject arg1)
        {
            Argument.NotNull(() => arg1);
        }

Pretty compact, right? But wait, what’s that lambda for? I wanted to make sure that the property’s name was correctly being passed along so if the object was null and an exception was raised, we would be able to provide the name of the property that was null. This is exactly like what is happening in our first example. So, why didn’t we implement it like this:

      Argument.NotNull(arg1, “arg1”);

Remember, our goal is to reduce code and typing. We could have implemented it this way, but as parameters get longer you would need to add more characters:

      Argument.NotNull(myreallylongnamedargument, “myreallylongnamedargument”);

PLUS depending on the refactoring tools you use, if you ever rename your parameter you have to name sure to go back and rename your validation string name. Kind of a hassle. Using the lambda gets us around this issue and keeps our re-work to a minimum.

This method is much more relavant when we have multiple arguments we want to validate. Lets look at our second example above with this method applied:

        public void MyMethod(MyObject arg1, MyObject arg2, DateTime? arg3)
        {
            Argument.NotNull(() => arg1);
            Argument.NotNull(() => arg2);
            Argument.NotNull(() => arg3);

	          ...
        }

Much cleaner! But wait... even that looks like duplicate code for each validation. We can do better! Each parameter check has several overloads, so we can chain our checks together:

        public void MyMethod(MyObject arg1, MyObject arg2, DateTime? arg3)
        {
            Argument.NotNull(
                () => arg1,
                () => arg2,
                () => arg3);

            ...
        }

Or, if you prefer to have it all on one line:

        public void MyMethod(MyObject arg1, MyObject arg2, DateTime? arg3)
        {
            Argument.NotNull(() => arg1, () => arg2, () => arg3);

	          ...
        }

We’ve just compacted over a dozen lines down to just one.

Argument contains the most basic argument validation checks at the moment, including:

    NotNull (for T)
    NotNullOrEmpty (for String, Guid, and IEnumerable<T>)
    NotNullOrWhiteSpace (for String)
    NotNullOrEmptyOrWhiteSpace (for String)
