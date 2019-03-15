using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace HubiMichu
{
    class Program
    {
        static void Main(string[] args)
        {
            Watek w = new Watek();
            Console.ReadKey();
        }
    }


    class Watek
    {
        static int[] array;
        static int[] sortedArr;
        static double[] surfaces;
        static bool generated;
        static bool sorted;
        public Watek()
        {
            generated = false;
            sorted = false;
            array = new int[1500];
            Thread tGen = new Thread(new ThreadStart(generate));
            Thread tSort = new Thread(new ThreadStart(sort));
            Thread tCalc = new Thread(new ThreadStart(calcCSurf));
            tGen.Start();
            tSort.Start();
            tCalc.Start();


            // join them all
            tGen.Join();
            tSort.Join();
            tCalc.Join();
            printArray();           
        }

        public void generate()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 1500);
            }
            var time = sw.Elapsed;
            Console.WriteLine("Generating done in: " + time.ToString() );
            generated = true;
        }

        public void sort()
        {
            // wait to generate
            while( !generated ) { }
            Stopwatch sw = Stopwatch.StartNew();
            sortedArr = new int[array.Length];
            for(int i = 0; i < sortedArr.Length; i++)
            {
                sortedArr[i] = array[i];
            }
            Array.Sort(sortedArr);
            var time = sw.Elapsed;
            Console.WriteLine("Sorting done in: " + time.ToString());
            sorted = true;
        }

        public void calcCSurf()
        {
                
            while ( !sorted ) { }
            Stopwatch sw = Stopwatch.StartNew();
            surfaces = new double[sortedArr.Length];
            for( int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i] = 3.14 * sortedArr[i]* sortedArr[i];
            }
            var time = sw.Elapsed;
            Console.WriteLine("Calculation done in: " + time.ToString());

        }

        public void printArray()
        {
            //*//
            Console.Write("[ ");
            foreach( var item in surfaces )
            {
                Console.Write(item.ToString() + "; ");
            }
            Console.Write("]");
            //*/
            Console.Write(surfaces.Length);
        }
    }
}
