using System;

namespace Domain
{
	public abstract class AbstractModel<T>
	{
		public T Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
