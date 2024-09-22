namespace InSynq.Core.Model.Interfaces;

public interface IEntity<TKey> where TKey : struct
{
	TKey Id { get; set; }
}