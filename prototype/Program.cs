using System;

namespace Deo.LaserVg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //FileWatcher.Track("test.lvg", Convert);
            Convert();
        }

        static void Convert()
        {
            Converter.Convert("test.lvg", "test.svg");
        }
    }
}
