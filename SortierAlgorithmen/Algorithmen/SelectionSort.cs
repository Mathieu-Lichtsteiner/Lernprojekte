namespace SortierAlgorithmen {
	class SelectionSort : IAlgorithmus {
		public uint[] Sort( uint[] input ) {
			for( int i = 0; i < input.Length; i++ )
				for( int j = i + 1; j < input.Length; j++ )
					if( input[i] > input[j] ) {
						uint save = input[i];
						input[i] = input[j];
						input[j] = save;
					}
			return input;
		}
	}
}
