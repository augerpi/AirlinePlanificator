using System;
using System.Linq.Expressions;

namespace AirlinePlanificator.Utilities
{
    public class ReflectorPropertyName
    {
        // requires object instance, but you can skip specifying T
        static public string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of object type
        static public string GetPropertyName<TObject>(Expression<Func<TObject, object>> exp)
        {
            var body = exp.Body;
            var convertExpression = body as UnaryExpression;
            if (convertExpression != null)
            {
                if (convertExpression.NodeType != ExpressionType.Convert)
                {
                    throw new ArgumentException("Invalid property expression.", "exp");
                }
                body = convertExpression.Operand;
            }
            return ((MemberExpression)body).Member.Name;
        }
    }
}
