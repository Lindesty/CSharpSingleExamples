using _4_EFCoreWithSqliteInWPF.Models;
using Mapster;

namespace _4_EFCoreWithSqliteInWPF.Mappers;


public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookForShow>().TwoWays();
    }
}