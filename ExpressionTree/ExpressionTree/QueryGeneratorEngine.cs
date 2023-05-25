using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ExpressionTree;

/// <summary>
/// Based on query json, output IQueryable.
/// </summary>
public static class QueryGeneratorEngine
{
    public static IQueryable<T> BuildQuery<T>(DbContext context, Query query) where T : class
    {
        // Get DbSet for the entity
        DbSet<T> dbSet = context.Set<T>();

        // Build predicate expression based on query properties
        ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "e");

        // Builds e.g. e => e.Id == 1 && e.Name == "Alice"
        Expression predicate = BuildPredicateExpression(parameterExpression, query.Properties);

        // Build queryable, which is simple .Where(e => e.Id == 1 && e.Name == "Alice")
        IQueryable<T> queryable = dbSet.Where(Expression.Lambda<Func<T, bool>>(predicate, parameterExpression));

        return queryable;
    }

    private static Expression BuildPredicateExpression(ParameterExpression parameterExpression,
        List<Property> properties)
    {
        // Build expressions for each property's constraints
        Expression propertyExpression = null;
        foreach (var property in properties)
        {
            // e.g e.Id
            MemberExpression memberExpression = Expression.Property(parameterExpression, property.Name);

            foreach (var constraint in property.Constraints)
            {
                // e.g. 1
                ConstantExpression valueExpression = GetConstantExpression(constraint.Value, property.Type);

                // e.g. e.Id == 1
                Expression constraintExpression = GetOperatorExpression(constraint.Operator, memberExpression, valueExpression);

                if (propertyExpression == null)
                {
                    propertyExpression = constraintExpression;
                }
                else
                {
                    // Concatenate e.Name == "Alice"
                    propertyExpression = Expression.AndAlso(propertyExpression, constraintExpression);
                }
            }
        }

        return propertyExpression;
    }

    private static ConstantExpression GetConstantExpression(object value, string type)
    {
        switch (type.ToLower())
        {
            case "string":

                if (value is string stringValue)
                {
                    return Expression.Constant(stringValue, typeof(string));
                }

                break;

            case "integer":

                if (value is long longValue)
                {
                    return Expression.Constant((int)longValue, typeof(int));
                }

                if (value is int intValue)
                {
                    return Expression.Constant(intValue, typeof(int));
                }

                break;
        }

        throw new InvalidOperationException("Type not supported.");
    }

    private static Expression GetOperatorExpression(string @operator, Expression left, Expression right)
    {
        switch (@operator.ToLower())
        {
            case "equal":
                return Expression.Equal(left, right);
            case "notequal":
                return Expression.NotEqual(left, right);
            case "greaterthan":
                return Expression.GreaterThan(left, right);
            case "greaterthanorequal":
                return Expression.GreaterThanOrEqual(left, right);
            case "lessthan":
                return Expression.LessThan(left, right);
            case "lessthanorequal":
                return Expression.LessThanOrEqual(left, right);
            default:
                throw new ArgumentException($"Invalid operator '{@operator}'");
        }
    }
}