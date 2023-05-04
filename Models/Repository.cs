using Metalama.Shared;
using Shared.Interfaces;

namespace Models;

[Injection]
public class Repository<T> where T : IHasId
{
}