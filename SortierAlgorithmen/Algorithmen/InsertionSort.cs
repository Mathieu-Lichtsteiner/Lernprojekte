namespace SortierAlgorithmen {

	class InsertionSort : IAlgorithmus {
		public uint[] Sort( uint[] input ) {
			for( int i = 0; i < input.Length; i++ )
				for( int j = i; j > 0; j-- )
					if( input[j] < input[j - 1] ) {
						uint save = input[j];
						input[j] = input[j - 1];
						input[j - 1] = save;
					}
			return input;
		}
	}
}
