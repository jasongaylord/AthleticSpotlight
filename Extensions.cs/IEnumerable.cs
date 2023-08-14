using System;
using System.Collections;
using System.Collections.Generic;

namespace AthleticSpotlight.Core;

public static class IEnumerableExtensions {
    public static decimal InningSum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) {
        decimal totalInnings = 0m;
        decimal totalFractionalInnings = 0m;

        foreach (decimal val in source.Select(selector)) {
            int inning = (int)val;
            decimal fraction = val - inning;

            totalInnings += inning;
            totalFractionalInnings += fraction;
        }

        decimal bumpUp = Math.Floor(totalFractionalInnings / 0.3m);
        decimal remainder = totalFractionalInnings - (bumpUp * 0.3m);

        return totalInnings + bumpUp + remainder;
    }
}