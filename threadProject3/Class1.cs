using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threadProject3
{
    class Class1
    {
        static Object lockObject = new Object();
        public static void Main()
        {
            Console.WriteLine("Enter a size for an array to sort:");
            int length = int.Parse(Console.ReadLine());
           
            int [] myArray = randomizer(length);
            printArray(myArray);
            Thread originalQuickSortThread = new Thread(quickSort);
            
                printArray(myArray, (myArray.Length-1), 0, false);

            originalQuickSortThread.Start(new Range{Array = myArray, Lower = 0, Upper = myArray.Length-1});
            originalQuickSortThread.Join();

                printArray(myArray, (myArray.Length-1), 0, true);

            System.Console.WriteLine("\nThe Sorted Array is");
            printArray(myArray);
            

        }
        public static int[] randomizer(int length)
        {
            Random rand = new Random();
           int[] myArray = new int[length];


            for (int i = 0; i < myArray.Length; i++)
            {
                int rInt = rand.Next(0, 99 + 1);

                while (myArray.Contains(rInt))
                {
                    rand = new Random();
                    rInt = rand.Next(0, 99 + 1);

                }
                myArray[i] = rInt;

            }
            return myArray;
        }
        public static void printArray(int[] arr, int upper, int lower, bool beginEnd)
        {
            lock (lockObject)
            {
                System.Console.WriteLine(" ");
                if(beginEnd == true)
                {
                    System.Console.Write("End sort of " + lower + "->" + upper + ": ");
                }
                if(beginEnd == false)
                {
                    System.Console.Write("Begin sort of " + lower + "->" + upper + ": ");
                }
              
                Console.ResetColor();


                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == lower)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        System.Console.Write("[");
                    }
                    if (i == upper + 1)
                    {
                        Console.ResetColor();
                    }
                    System.Console.Write(arr[i]);
                    if (i == upper)
                    {
                        System.Console.Write("]");
                    }
                    System.Console.Write(" ");
                }
                Console.ResetColor();
            }
        }
        public static void printArray(int[] arr)
        {
            foreach (int a in arr)
            {
                
                System.Console.Write(a +" " );
            }
        }
        static void quickSort(object rangeObj)
        {
            Range range = rangeObj as Range;
            int[] arr = range.Array;
            int lower = range.Lower;
            int upper = range.Upper;
            if(lower < upper ) //echo entire array |White [blue] white|
            {

                int pivot = partition(arr, lower, upper);

                Thread lowerThread = new Thread(quickSort);
                Thread upperThread = new Thread(quickSort);
                lowerThread.Start(new Range { Array = arr, Lower = lower, Upper = pivot - 1 });
                upperThread.Start( new Range{ Array = arr, Lower = pivot+1, Upper = upper});
                lowerThread.Join();
                upperThread.Join();

                
            }

        }
        public static int partition(int[] arr, int lower, int upper)
        {

            int pivotIndex = upper + (lower - upper) / 2;
            int pivotValue = arr[pivotIndex];

                printArray(arr, pivotIndex, lower, false);
 
            Swap(arr, upper, pivotIndex);

            int Index = lower;

            for (int i = lower; i <= upper - 1; i++)
            {
                if (arr[i] < pivotValue)
                {
                    Swap(arr, Index, i);
                    Index++;
                }
            }

            Swap(arr, Index, upper);

                printArray(arr, pivotIndex, lower, true);

            return Index;
        }
        public static void Swap(int[] arr, int lower, int upper)
        {

            int temp = arr[lower];
            arr[lower] = arr[upper];
            arr[upper] = temp;
          
        }
    }
    class Range
    {
       public int [] Array{set;get;}
        public int Upper{set; get;}
        public int Lower{set;get;} 

    }
}

