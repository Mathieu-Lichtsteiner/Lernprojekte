using System;

namespace SortierAlgorithmen
{
	public class MergeSort : IAlgorithmus {
		public uint[] Sort( uint[] input ) {
			uint[] second, ret, i1, i2;
			int retIdx = 0, lIdx = 0, rIdx = 0;

			if( input.Length == 1 )
			{
				return input;
			}

			if ( input.Length > 1 ) {
				int lHalf = (int)Math.Floor( input.Length / 2.0 );
				int rHalf = input.Length - lHalf;
				i1 = new uint[lHalf];
				i2 = new uint[rHalf];
				Array.Copy( input, 0, i1, 0, lHalf );
				Array.Copy( input, lHalf, i2, 0, rHalf );
				input = Sort( i1 );
				second = Sort( i2 );
			}
			else
			{
				second = new uint[0];
			}

			ret = new uint[input.Length + second.Length];

			while( lIdx < input.Length && rIdx < second.Length )
			{
				ret[retIdx++] = (input[lIdx] <= second[rIdx]) ? input[lIdx++] : second[rIdx++];
			}

			if ( lIdx < input.Length )
			{
				for ( int i = lIdx; i < input.Length; i++ )
				{
					ret[retIdx++] = input[lIdx++];
				}
			}

			if ( rIdx < second.Length )
			{
				for ( int i = rIdx; i < second.Length; i++ )
				{
					ret[retIdx++] = second[rIdx++];
				}
			}

			return ret;
		}

		private uint[] Merge( uint[] first, uint[] second ) {
			if( first.Length > 1 ) {
				SplitArray( first, out uint[] f1, out uint[] f2 );
				first = Merge( f1, f2 );
			}
			if( second.Length > 1 ) {
				SplitArray( second, out uint[] s1, out uint[] s2 );
				second = Merge( s1, s2 );
			}

			uint[] ret = new uint[first.Length + second.Length];

			int retIdx = 0;
			int lIdx = 0;
			int rIdx = 0;

			while( lIdx < first.Length && rIdx < second.Length )
			{
				ret[retIdx++] = (first[lIdx] <= second[rIdx]) ? first[lIdx++] : second[rIdx++];
			}

			if ( lIdx < first.Length )
			{
				for ( int i = lIdx; i < first.Length; i++ )
				{
					ret[retIdx++] = first[lIdx++];
				}
			}

			if ( rIdx < second.Length )
			{
				for ( int i = rIdx; i < second.Length; i++ )
				{
					ret[retIdx++] = second[rIdx++];
				}
			}

			return ret;
		}

		private void SplitArray( uint[] input, out uint[] first, out uint[] second ) {
			int half = (int)Math.Floor( input.Length / 2.0 );
			Array.Copy( input, 0, first = new uint[half], 0, half );
			Array.Copy( input, half, second = new uint[input.Length - half], 0, input.Length - half );
		}


	}
}
