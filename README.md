# This project is created for reproduce AutoMapper's `ProjectTo` problem

## Sample Project's Tech Stack

- .NET Core 2.2
- EF Core 2.2
- AutoMapper 8.1.1 (or 9.0.0)
- SqlServer or PostgreSQL

## The Problem

In our company, we've recently added composite foreign keys to entities. We just want to ensure the data consistency between tenants. But we started to get this error  when we use AutoMapper's `ProjectTo` method.

```csharp
Unhandled Exception: System.InvalidOperationException: Nullable object must have a value.
   at lambda_method(Closure , Object[] )
   at Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal.TaskLiftingExpressionVisitor._ExecuteAsync[T](IReadOnlyList`1 taskFactories, Func`2 selector)
   at Microsoft.EntityFrameworkCore.Query.Internal.AsyncLinqOperatorProvider.AsyncSelectEnumerable`2.AsyncSelectEnumerator.MoveNext(CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.Internal.AsyncLinqOperatorProvider.ExceptionInterceptor`1.EnumeratorExceptionInterceptor.MoveNext(CancellationToken cancellationToken)
   at System.Linq.AsyncEnumerable.Aggregate_[TSource,TAccumulate,TResult](IAsyncEnumerable`1 source, TAccumulate seed, Func`3 accumulator, Func`2 resultSelector, CancellationToken cancellationToken) in D:\a\1\s\Ix.NET\Source\System.Interactive.Async\Aggregate.cs:line 120
   at CompositeNullableForeignKeySample.Program.Main(String[] args) in C:\Users\mustafa.sadedil\source\repos\CompositeNullableForeignKeySample\Program.cs:line 113
   at CompositeNullableForeignKeySample.Program.<Main>(String[] args)
```

## How can I reproduce the problem

- Clone the repo
- Check the `SampleContext.OnConfiguring` method, and change the connection string for your own setup (you can use MsSql or PostgreSql, the error will be same)
- The database created automatically when you run the project

### If you want to get this error

- Just run the project (I'm using VS 2019)

### If you want to get rid of this error

- Comment out two lines in `SampleContext.OnModelCreating` method (which is containing `HasForeignKey` and `HasPrincipalKey`)
- Then run the project
