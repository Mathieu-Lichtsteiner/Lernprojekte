using System;
using System.Collections.Generic;

namespace SortierAlgorithmen
{
	public static class CollectionWriter {

		public static void Write<T>( ICollection<T> collection ) {
			foreach( T item in collection )
			{
				Console.Write( $"{item}, " );
			}

			Console.WriteLine();
		}

	}
}
