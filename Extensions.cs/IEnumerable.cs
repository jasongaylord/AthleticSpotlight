using System;
using System.Collections;
using System.Collections.Generic;

namespace AthleticSpotlight.Core;

public static class IEnumerableExtensions {
    public static decimal InningSum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) {
        var filteredEnumerable = Enumerable.Select(source, selector);

        decimal fractions = 0m;
        decimal innings = 0m;

        foreach (decimal val in filteredEnumerable) {
            int inning = (int)val;
            fractions = (val - inning) + fractions;

            innings += inning;
        }

        var bumpUp = (fractions / 3) * 10;
        innings += (int)bumpUp;
        decimal remainder = bumpUp - (int)bumpUp;

        return innings;
    }
}