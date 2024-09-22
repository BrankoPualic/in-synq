using InSynq.Common;

namespace InSynq.Core.ResponseWrapper;

public class ResponseWrapper
{
	public ResponseWrapper()
	{
		IsSuccess = true;
		Error = new();
	}

	public ResponseWrapper(Error error)
	{
		IsSuccess = false;
		Error = error;
	}

	public bool IsSuccess { get; }

	public Error Error { get; }
}