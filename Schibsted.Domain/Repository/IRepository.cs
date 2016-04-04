using System;
namespace Schibsted.Domain.Repository
{
    public interface IRepository
    {
        IRepositoryEngine Engine { get; }
        Schibsted.Domain.Model.Enums.RepositoryKind Kind { get; }
        bool SetEngine(Schibsted.Domain.Model.Enums.RepositoryKind kind, IRepositoryEngine value);
    }
}
