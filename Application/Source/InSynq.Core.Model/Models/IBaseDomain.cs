namespace InSynq.Core.Model.Models;

public interface IBaseDomain<TKey> : IEntity<TKey> where TKey : struct
{ }