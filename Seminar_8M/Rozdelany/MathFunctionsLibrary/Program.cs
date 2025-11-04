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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Interval linearDomain = new Interval(Double.NegativeInfinity, Double.PositiveInfinity, "(", ")");
            Interval linearRange = new Interval(Double.NegativeInfinity, Double.PositiveInfinity, "(", ")");
            Linear f = new Linear("Lineární funkce", "f(x)=3x+5", linearDomain, linearRange, 3, 5);
            Console.WriteLine(f.ToString());
            Console.WriteLine(f.Solve(2));

            Interval fractDomain1 = new Interval(Double.NegativeInfinity, 6.0 / 4.0, "(", ")");
            Interval fractDomain2 = new Interval(6.0 / 4.0, Double.PositiveInfinity, "(", ")");

            LinearFraction q = new LinearFraction("Lineární lomená funkce", "f(x) = (3x+5)/(6x-4)", fractDomain1, fractDomain2, linearRange, 3, 5, 6, -4);
            Console.WriteLine(q.ToString());
            Console.WriteLine(q.Solve(2));
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
    class LinearFraction : MathFunction
    {
        public double NumeratorSlope { get; private set; }
        public double NumeratorIntercept { get; private set; }
        public double DenominatorSlope { get; private set; }
        public double DenominatorIntercept { get; private set; }
        Interval Domain2;

        public LinearFraction(string nazev, string description, Interval domain, Interval domain2, Interval range, double numeratorSlope, double numeratorIntercept, double denominatorSlope, double denominatorIntercept) 
            : base(nazev, description, domain, range)
        {
            NumeratorSlope = numeratorSlope;
            NumeratorIntercept = numeratorIntercept;
            DenominatorSlope = denominatorSlope;
            DenominatorIntercept = denominatorIntercept;
            Domain2 = domain2;
        }

        public override double Solve(double x)
        {
            return (NumeratorSlope * x + NumeratorIntercept)/(DenominatorSlope * x + DenominatorIntercept);
        }

        public override string ToString()
        {
            return $"{Nazev}, {Description}, {Domain.ToString()} U {Domain2.ToString()}, {Range.ToString()}, funkce má hyperbolický průběh";
        }
    }
}
