using System;

namespace Domain
{
	public abstract class Model<T>
	{
		public T Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
