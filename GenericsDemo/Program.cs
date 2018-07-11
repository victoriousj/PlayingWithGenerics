using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static GenericsDemo.Program.EnumerableCompositor;
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

			int numOdd = 0;

			foreach (var value in Create(list1, list2, set1, array1))
			{
				if (IsOdd(value)) numOdd++;
			}

			HashSet<int> set = Create(list1, list2, set1, array1).To<HashSet<int>>();

			IEnumerable<int> firstThree = Util.Take(set1, 3);
			foreach (var item in firstThree)
			{

			}

		}
		public static class EnumerableCompositor
		{
			public static EnumerableCompositor<T> Create<T>(params IEnumerable<T>[] collections)
			{
				return new EnumerableCompositor<T>(collections);
			}
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
				return GetEnumerator();
			}

			public TCollection To<TCollection>() where TCollection : ICollection<T>, new()
			{
				var collection = new TCollection();

				foreach (var item in this)
				{
					collection.Add(item);
				}

				return collection;
			}
		}

		public static class Util
		{
			public static IEnumerable<T> Take<T>(IEnumerable<T> source, int n)
			{
				int i = 0;
				foreach (var item in source)
				{
					yield return item;

					if (++i == n)
					{
						yield break;
					}
				}
			}

			public static T Min<T>(T item1, T item2) where T: IComparable<T>
			{
				return (item1.CompareTo(item2) < 0) ? item1 : item2;
			}
		}
	}
}
