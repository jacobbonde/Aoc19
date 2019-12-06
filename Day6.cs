using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC19
{
    internal class Day6
    {
        public static void Part1And2(string[] input)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var orbitMap = OrbitMap.Create(input);

            Console.WriteLine($"\t Rels: {orbitMap.Checksum()}");
            Console.WriteLine($"\t Rels: {orbitMap.OrbitalTransfers("YOU", "SAN")}");
            
            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var orbitMap = OrbitMap.Create(input);

            sw.Stop();
            Console.WriteLine($"\t Rels: {orbitMap.OrbitalTransfers("YOU", "SAN")}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }

    internal class OrbitMap
    {
        private int orbitCountCheckSum;

        private Body centerOfMass;
        Dictionary<string, Body> bodies = new Dictionary<string, Body>();

        public static OrbitMap Create(string[] orbitalRelationships)
        {
            OrbitMap orbitMap = new OrbitMap();

            foreach (var relationship in orbitalRelationships)
            {
                var bodyNames = relationship.Split(')');
                var primaryName = bodyNames[0];
                var satelliteName = bodyNames[1];

                Body primary = orbitMap.AddBody(primaryName);
                Body satellite = orbitMap.AddBody(satelliteName);

                primary.OrbitedBy(satellite);
            }

            orbitMap.centerOfMass = orbitMap.bodies["COM"];
            return orbitMap;
        }
        
        internal int Checksum()
        {
            if (orbitCountCheckSum > 0)
            {
                return orbitCountCheckSum;
            }

            this.TraverseAndIncrementChecksum(centerOfMass);
            return orbitCountCheckSum;
        }

        private Body AddBody(string bodyName)
        {
            if (!bodies.ContainsKey(bodyName))
            {
                var body = new Body(bodyName);
                bodies[bodyName] = body;
                return body;
            }
            return bodies[bodyName];
        }

        private void TraverseAndIncrementChecksum(Body body, int level = -1)
        {
            int myLevel = level + 1;

            orbitCountCheckSum += myLevel;

            if (body.satellites is null)
            {
                return;
            }
            foreach (var satellite in body.satellites)
            {
                TraverseAndIncrementChecksum(satellite, myLevel);
            }
        }

        internal int OrbitalTransfers(string from, string to)
        {
            var me = bodies[from];
            var santa = bodies[to];

            var myPath = PathToCOM(me);
            var santasPath = PathToCOM(santa);

            Body nearestIntersection = null;
            foreach (var body in myPath)
            {
                if (santasPath.Contains(body)) 
                {
                    nearestIntersection = body;
                    break;
                }
            }

            return CountSteps(me, nearestIntersection) + CountSteps(santa, nearestIntersection);
        }
        private static IEnumerable<Body> PathToCOM(Body me)
        {

            var body = me;
            while (body.primary != null)
            {
                body = body.primary;
                yield return body;
            }
        }
        private static int CountSteps(Body from, Body to, int stepsSoFar = 0)
        {
            if (from.primary == to || from.primary is null)
            {
                return stepsSoFar;
            }
            return CountSteps(from.primary, to, stepsSoFar+1);
        }
    }
    

    public class Body
    {
        public List<Body> satellites = null;
        private string name;
        public Body primary;

        public Body(string name)
        {
            this.name = name;
        }

        public void OrbitedBy(Body satellite)
        {
            if (satellites == null)
            {
                satellites = new List<Body>();
            }
            
            this.satellites.Add(satellite);
            satellite.primary = this;
        }
    }
}