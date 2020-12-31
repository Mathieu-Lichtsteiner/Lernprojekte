namespace SortierAlgorithmen {
	public class CountingSort : IAlgorithmus {
		public uint[] Sort( uint[] input ) {

			// countarray erstellen
			int maximum = 0;
			foreach( int val in input )
				if( val > maximum )
					maximum = val;
			uint[] countArray = new uint[maximum + 1];

			// vorkommen von zahlen zählen
			foreach( uint val in input )
				countArray[val] += 1;
			// vorderes feld dazurechnen
			for( int i = 0; i < countArray.Length; i++ )
				countArray[i] += (i - 1 < 0 ? 0 : countArray[i - 1]);

			// ausgabearray erstellen
			uint[] output = new uint[input.Length];
			foreach( uint item in input ) {
				// zahl in ausgabearray eintragen
				output[countArray[item]-1] = item;
				// countarray um 1 verringern, falls eine zahl doppelt vorkommt wird sie in den nächstkleineren index gesetzt
				countArray[item] -= 1;
			}


			return output;
		}
	}
}
