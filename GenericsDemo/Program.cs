using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

namespace GenericsDemo
{
	class Program
	{
		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}

		static void Main(string[] args)
		{
			var list1 = new List<int> { 1, 2, 3, 4, 5 };
			var list2 = new List<int> { 2, 4, 6, 8, 10 };
			var set1 = new HashSet<int> { 3, 6, 7, 12, 15 };
			var array1 = new[] { 4, 8, 12, 16, 20 };

			var ec = new EnumerableCompositor<int> { list1, list2, set1, array1 };

			int numOdd = 0;

			foreach (var value in ec)
			{
				if (IsOdd(value)) numOdd++;
			}

			numOdd = ec.Count(x => IsOdd(x));
		}

		public class EnumerableCompositor<T> : IEnumerable<T>
		{
			private List<IEnumerable<T>> _collections { get; set; }


			public EnumerableCompositor() 
				=> _collections = new List<IEnumerable<T>>();
			public EnumerableCompositor(IEnumerable<IEnumerable<T>> collections) 
				=> _collections = collections.ToList();


			public void Add(IEnumerable<T> collection) => _collections.Add(collection);



			public IEnumerator<T> GetEnumerator()
			{
				foreach (var collection in _collections)
				{
					foreach (var item in collection)
					{
						yield return item;
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}
	}
}
