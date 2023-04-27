
using System.Linq.Expressions;

namespace OrderNested.Extensions
{
    public static class OrderExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TKey>(this IQueryable<TEntity> source,
                                                                 Expression<Func<TEntity, TKey>> orderByKeySelector,
                                                                 bool asc)
        {
            string command = asc ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), command,
                                                   new Type[] { typeof(TEntity), typeof(TKey) },
                                                   source.Expression, orderByKeySelector);
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
        }


        /* 
         
        

        public static Expression<Func<TEntity, TKey>> GetOrderByKeySelector<TEntity, TKey>(string field)
        {
            if(field.Contains("_"))
            {
                string[] propParam = field.Split('_');
                string nameObject = propParam[0];
                string propObject = propParam[1];
                
                return GetOrderByKeySelectorNested<TEntity, TKey>(nameObject, propObject);
            }
            else
            {
                return GetOrderByKeySelectorSingle<TEntity, TKey>(field);
            }
        }
         */
    }
}
