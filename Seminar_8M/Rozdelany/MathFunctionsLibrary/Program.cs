using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionsLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Interval linearDomain = new Interval(Double.NegativeInfinity, Double.PositiveInfinity, "(", ")");
            Interval linearRange = new Interval(Double.NegativeInfinity, Double.PositiveInfinity, "(", ")");
            Linear f = new Linear("Lineární funkce", "f(x)=3x+5", linearDomain, linearRange, 3, 5);
            Console.WriteLine(f.ToString());
            Console.WriteLine(f.Solve(2));
        }
    }
    struct Interval
    {
        public double Min {  get; set; }
        public double Max { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public Interval(double min, double max, string start, string end)
        {
            Min = min;
            Max = max;
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"{Start}{Min}; {Max}{End}";
        }
    }

    abstract class MathFunction
    {
        public string Nazev { get; private set; }
        public string Description { get; private set; }
        public Interval Domain;
        public Interval Range;

        public MathFunction(string nazev, string description, Interval domain, Interval range)
        {
            Nazev = nazev;
            Description = description;
            Domain = domain;
            Range = range;
        }
        public override string ToString()
        {
            return $"{Nazev}, {Description}, {Domain.ToString()}, {Range.ToString()}";
        }

        abstract public double Solve(double x);
    }

    class Linear : MathFunction
    {
        public double Slope { get; private set; }
        public double Intercept { get; private set; }

        public Linear(string nazev, string description, Interval domain, Interval range, double slope, double intercept)
            : base(nazev, description, domain, range)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public override double Solve(double x)
        {
            return Slope * x + Intercept;
        }
    }
}
