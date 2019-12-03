using System.IO;
using System.Linq;

namespace AoC19
{
    public static class Input
    {
        public static int[] ReadIntLines(string filename)
        {
            return File.ReadLines(filename).Select(l => int.Parse(l)).ToArray();
        }
        public static double[] ReadDoubleLines(string filename)
        {
            return File.ReadLines(filename).Select(l => double.Parse(l)).ToArray();
        }

        public static string[] ReadStringLines(string filename)
        {
            return File.ReadLines(filename).ToArray();
        }

        public static int[] ReadIntSeparated(string filename, char separator)
        {

            return File.ReadLines(filename).SelectMany(l => l.Split(separator)).Select(i => int.Parse(i)).ToArray();
        }

        public static double[] ReadDoubleCommaSeparated(string filename, char separator)
        {

            return File.ReadLines(filename).SelectMany(l => l.Split(separator)).Select(i => double.Parse(i)).ToArray();
        }

        public static string[] ReadStringSeparated(string filename, char separator)
        {

            return File.ReadLines(filename).SelectMany(l => l.Split(separator)).ToArray();
        }
    }
}
