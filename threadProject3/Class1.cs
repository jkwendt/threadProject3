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
          
            int [] myArray = {21, 15, 7,37, 23, 90, 51, 87, 43, 61, 60, 27, 76, 24, 86, 3};
            printArray(myArray);
            Thread originalQuickSortThread = new Thread(quickSort);
            System.Console.WriteLine("Sorting Array");
            lock (lockObject)
            {
                System.Console.Write("Begin sort of " + 0 + "->" + (myArray.Length-1) + ": ");

                printArray(myArray, (myArray.Length-1), 0);
                System.Console.WriteLine(" ");
                Console.ResetColor();
            }
            originalQuickSortThread.Start(new Range{Array = myArray, Lower = 0, Upper = myArray.Length-1});
            originalQuickSortThread.Join();
            lock (lockObject)
            {
                System.Console.Write("End sort of " + 0 + "->" + (myArray.Length-1) + ": ");

                printArray(myArray, (myArray.Length-1), 0);
                System.Console.WriteLine(" ");
                Console.ResetColor();
            }
            System.Console.WriteLine("Sorted Array");
            printArray(myArray);
            

        }
       // public static void print()
        public static void printArray(int[] arr, int upper, int lower)
        {

            for(int i = 0; i < arr.Length; i++)
            {
                if(i == lower)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.Write("[");
                }
                if(i == upper +1)
                {
                    Console.ResetColor();
                }
                System.Console.Write(arr[i]);
                if(i == upper)
                {
                    System.Console.Write("]");
                }
                System.Console.Write(" ");
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
                /*lock (lockObject)
                {
                    System.Console.Write("Begin sort of " + lower + "->" + upper + ": ");

                    printArray(arr, upper, lower);
                    System.Console.WriteLine(" ");
                    Console.ResetColor();
                }*/
                int pivot = partition(arr, lower, upper);
                /*lock (lockObject)
                {
                    System.Console.Write("Begin sort of " + lower + "->" + pivot + ": ");

                    printArray(arr, pivot, lower);
                    System.Console.WriteLine(" ");
                    Console.ResetColor();
                }*/
                Thread lowerThread = new Thread(quickSort);
                Thread upperThread = new Thread(quickSort);
                lowerThread.Start(new Range { Array = arr, Lower = lower, Upper = pivot - 1 });
                upperThread.Start( new Range{ Array = arr, Lower = pivot+1, Upper = upper});
                lowerThread.Join();
                upperThread.Join();
                /*lock (lockObject)
                {
                    System.Console.Write("End sort of " + lower + "->" + upper + ": ");
                    printArray(arr, upper, lower);
                    System.Console.WriteLine(" ");
                    Console.ResetColor();
                }*/
                /*lock (lockObject)
                {
                    System.Console.Write("End sort of " + lower + "->" + pivot + ": ");
                    printArray(arr, pivot, lower);
                    System.Console.WriteLine(" ");
                    Console.ResetColor();
                }*/
                
            }

        }
        public static int partition(int[] arr, int lower, int upper)
        {
            /*lock (lockObject)
            {
                System.Console.Write("Begin sort of " + lower + "->" + upper + ": ");

                printArray(arr, upper, lower);
                System.Console.WriteLine(" ");
                Console.ResetColor();
            }*/
            //Console.ResetColor();
            int pivotIndex = upper + (lower - upper) / 2;
            int pivotValue = arr[pivotIndex];
            lock (lockObject)
            {
                System.Console.Write("Begin sort of " + lower + "->" + pivotIndex + ": ");

                printArray(arr, pivotIndex, lower);
                System.Console.WriteLine(" ");
                Console.ResetColor();
            }
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
            lock (lockObject)
            {
                System.Console.Write("End sort of " + lower + "->" + pivotIndex + ": ");

                printArray(arr, pivotIndex, lower);
                System.Console.WriteLine(" ");
                Console.ResetColor();
            }
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

