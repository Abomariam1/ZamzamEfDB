using Zamzam.Core;
using Zamzam.EF;

IDataService<Area> service = new GenericDataServices<Area>(new ZamzamDbContextFactory());
var entity = await service.Delete(2);
entity.ToString();
Console.ReadKey();