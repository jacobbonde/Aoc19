using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC19
{
    internal class Day8
    {
        public static void Part1(string imageData)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

             int width = 25;
             int height = 6;

             int layerSize = width * height;
             if (imageData.Length % layerSize != 0)
             {
                 throw new Exception("Not full layers!");
             }

            var image = new Image(width, height);
            for (int i = 0; i < imageData.Length ; i++)
            {
                var v = imageData.AsSpan(i, 1);
                image.Add(int.Parse(v));
            }

            // part 1
            var layerWithMostZeros = image.Layers.OrderBy(l => l.data.Count(v => v == 0)).First();
            int ones = layerWithMostZeros.data.Count(v => v == 1);
            int twos = layerWithMostZeros.data.Count(v => v == 2);

            int result = ones * twos;


            // Part 2
            image.Render();

            sw.Stop();
            Console.WriteLine($"\t Answer: {result}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();


            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        internal class Layer
        {
            private readonly int width;
            private readonly int height;
            internal readonly int[] data;
            private int capacity;
            private int position;

            public Layer(int width, int height)
            {
                this.width = width;
                this.height = height;

                capacity = height * width;

                data = new int[capacity];
            }

            public bool Add(int value)
            {
                if (position == capacity)
                {
                    return false;
                }

                // int row = position / width;
                // int column = position % width;

                data[position] = value;

                position++;
                return true;
            }

            internal bool IsFull()
            {
                return position == capacity;
            }
        }


        internal class Image
        {
            private readonly int width;
            private readonly int height;
            private int capacity;
            private List<Layer> layers = new List<Layer>();
            private Layer current;
            internal IEnumerable<Layer> Layers => layers;


            public Image(int width, int height)
            {
                this.width = width;
                this.height = height;
                capacity = height * width;

                current = new Layer(width, height);
                layers.Add(current);
            }

            public void Add(int value)
            {
                if (current.IsFull())
                {
                    current = new Layer(width, height);
                    layers.Add(current);
                }

                current.Add(value);
            }

            public void Render()
            {
                var flattened = new int[capacity];

                for (int i = 0; i < capacity; i++)
                {
                    int flattenedValue = Flatten(i);
                    flattened[i] = flattenedValue;

                    if(i % width == 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(flattenedValue == 0 ? ' ' : 'X');

                }
            }

            private int Flatten(int i)
            {
                int colorValue = 2;
                foreach (var layer in Layers)
                {
                    colorValue = layer.data[i];
                    if(colorValue != 2)
                    {
                        break;
                    }
                }

                return colorValue;
            }
        }
    }
}