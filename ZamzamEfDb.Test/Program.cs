using Zamzam.Core;
using Zamzam.EF;

IDataService<Area> service = new GenericDataServices<Area>(new ZamzamDbContextFactory());
await service.Create(new Area { Id = 1, Name = "Kafr sakr", Staion = "kk" });
