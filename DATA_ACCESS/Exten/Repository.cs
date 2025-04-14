using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsaludEcopetrol.Exten;

namespace DATA_ACCESS.Exten
{
    class Repository : IRepository
    {
        private readonly System.Data.Linq.DataContext _dataContext;

        public Repository(System.Data.Linq.DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void BulkInsertEntities<TModelEntity>(IList<TModelEntity> modelEntities) where TModelEntity : class
        {
            var table = _dataContext.GetTable<TModelEntity>();
            table.BulkInsert(modelEntities);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
