using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Query
{

    public class Queryable 
    {
        private readonly IQueryable _queryable;

        public Queryable(IEnumerable queryEntity)
        {
            _queryable = queryEntity.AsQueryable();
        }

        public IQueryable<T> Query<T>(Func<T, bool> filter) => 
            _queryable.OfType<T>().AsEnumerable().Where(filter).AsQueryable();
    }
}
