using System;
using System.Collections;
using System.Collections.Generic;

namespace AthleticSpotlight.Core;

public static class IEnumerableExtensions {
    public static double InningSum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) {
        var filteredEnumerable = Enumerable.Select(source, selector);

        double fractions = 0;
        double innings = 0;

        foreach (double val in filteredEnumerable) {
            Console.WriteLine(val);
            int inning = (int)val;
            fractions += val - inning;

            innings += inning;
        }

        var bumpUp = (fractions / 3) * 10;
        innings += (int)bumpUp;
        double remainder = bumpUp - (int)bumpUp;

        Console.WriteLine("Innings: " + innings);
        Console.WriteLine("Remainder: " + remainder);

        return innings;
    }
}