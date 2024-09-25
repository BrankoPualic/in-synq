using InSynq.Common;
using System.Collections;

namespace InSynq.Core;

public class ResponseWrapper<T> : ResponseWrapper
{
	private readonly T _data;

	public ResponseWrapper(Error error) : base(error)
	{ }

	public ResponseWrapper(T data) : base() => _data = data;

	public T Data => IsSuccess ? _data : throw new InvalidOperationException("The value of a response can't be accessed.");

	public bool HasData => Data != null && (!typeof(IEnumerable).IsAssignableFrom(typeof(T)) || ((IEnumerable)Data).OfType<object>().Any());
}