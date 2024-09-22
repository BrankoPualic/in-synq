namespace InSynq.Core.Model.Interfaces;

public interface IDatabaseView
{
	string Script { get; }

	string DropScript { get; }
}