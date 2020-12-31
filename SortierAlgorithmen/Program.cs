using SortierAlgorithmen;
using System;

uint[] unsortiert;

Console.WriteLine( "--  UNSORTIERTE COLLECTION:  --" );
unsortiert = new uint[] { 423, 532, 1, 55, 45, 39, 101, 1000, 101, 32, 32, 160, 465, 423, 9 };
CollectionWriter.Write( unsortiert );

Console.WriteLine( "--  COUNTINGSORT - COLLECTION:  --" );
CollectionWriter.Write( new CountingSort().Sort( (uint[])unsortiert.Clone() ) );

Console.WriteLine( "--  SELECTIONSORT - COLLECTION:  --" );
CollectionWriter.Write( new SelectionSort().Sort( (uint[])unsortiert.Clone() ) );

Console.WriteLine( "--  INSERTIONSORT - COLLECTION:  --" );
CollectionWriter.Write( new InsertionSort().Sort( (uint[])unsortiert.Clone() ) );

Console.WriteLine( "--  BUBBLESORT - COLLECTION:  --" );
CollectionWriter.Write( new BubbleSort().Sort( (uint[])unsortiert.Clone() ) );

Console.WriteLine( "--  MERGESORT - COLLECTION:  --" );
CollectionWriter.Write( new MergeSort().Sort( (uint[])unsortiert.Clone() ) );

Console.WriteLine( "--  QUICKSORT - COLLECTION:  --" );
CollectionWriter.Write( new QuickSort().Sort( (uint[])unsortiert.Clone() ) );

Console.ReadKey();
