using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mitchell1.Online.Catalog.Host
{
	public class MultiList<T1,T2> : List<T1>, IList<T2>, IReadOnlyList<T2> where T2 : T1
	{
		public MultiList() {}

		public MultiList(IEnumerable<T2> collection) : base(collection.Select(x => (T1)x)) {}

		T2 IReadOnlyList<T2>.this[int index] => (T2)base[index];
		T2 IList<T2>.this[int index]
		{
			get => (T2)base[index];
			set => base[index] = value;
		}
		public int IndexOf(T2 item) => base.IndexOf(item);
		public void Insert(int index, T2 item) => base.Insert(index, item);
		public void Add(T2 item) => base.Add(item);
		public bool Contains(T2 item) => base.Contains(item);
		public void CopyTo(T2[] array, int arrayIndex)
		{
			if (array.Length - arrayIndex < Count)
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
			for (int i = 0; i < Count; i++)
				array[arrayIndex + i] = (T2)base[i];
		}
		public bool Remove(T2 item) => base.Remove(item);
		bool ICollection<T2>.IsReadOnly => ((ICollection<T1>) this).IsReadOnly;

		IEnumerator<T2> IEnumerable<T2>.GetEnumerator() => new T2Enumerator(GetEnumerator());

		private readonly struct T2Enumerator : IEnumerator<T2>
		{
			private readonly IEnumerator<T1> enumerator;

			public T2Enumerator(IEnumerator<T1> enumerator) => this.enumerator = enumerator;

			public void Dispose() => enumerator.Dispose();
			public bool MoveNext() => enumerator.MoveNext();
			public void Reset() => enumerator.Reset();
			public T2 Current => (T2) enumerator.Current;
			object IEnumerator.Current => Current;
		}
	}
}