using NET6_API_with_Generic_SQL_Repository.Entities;
using NET6_API_with_Generic_SQL_Repository.ViewModel;

namespace NET6_API_with_Generic_SQL_Repository.Interfaces
{
    public interface IExampleMapper
    {
        public ExampleEntity Map(ExampleViewModel model);
        public ExampleEntity Map(ExampleViewModel model, ExampleEntity example);
    }
}
