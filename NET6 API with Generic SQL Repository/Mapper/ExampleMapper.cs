using NET6_API_with_Generic_SQL_Repository.Entities;
using NET6_API_with_Generic_SQL_Repository.Interfaces;
using NET6_API_with_Generic_SQL_Repository.ViewModel;

namespace NET6_API_with_Generic_SQL_Repository.Mapper
{
    public class ExampleMapper : IExampleMapper

    {
        public ExampleEntity Map(ExampleViewModel model)
        {
            return new ExampleEntity
            {
                Name = model.Name,
                Exists = model.Exists

            };
        }

        public ExampleEntity Map(ExampleViewModel model, ExampleEntity example)
        {
            example.Name = model.Name;
            example.Exists = model.Exists;
            return example;
        }
    }
}
