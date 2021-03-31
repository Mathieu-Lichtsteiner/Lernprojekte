namespace SortierAlgorithmen
{

	public class BubbleSort : IAlgorithmus {
		public uint[] Sort( uint[] input ) {
			bool isSorted = false;
			while( isSorted is false ) {
				isSorted = true;
				for( int i = 0; i < (input.Length - 1); i++ )
				{
					if ( input[i] > input[i + 1] ) {
						uint save = input[i];
						input[i] = input[i + 1];
						input[i + 1] = save;
						isSorted = false;
					}
				}
			}
			return input;
		}
	}
}
