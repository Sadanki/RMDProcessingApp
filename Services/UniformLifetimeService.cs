using System;
using System.Collections.Generic;

namespace RMDProcessingApp.Services
{
    public class UniformLifetimeService : IUniformLifetimeService
    {
        // Partial table for demo; add more ages as needed.
        private static readonly Dictionary<int, decimal> _factors = new()
        {
            { 72, 27.4m }, // 3.65%
            { 73, 26.5m }, // 3.77%
            { 74, 25.5m }, // 3.92%
            { 75, 24.6m }, // 4.07%
            { 76, 23.7m }, // 4.22%
            { 77, 22.9m }, // 4.37%
            { 78, 22.0m }, // 4.55%
            { 79, 21.1m }, // 4.74%
            { 80, 20.2m }, // 4.95%
            { 81, 19.4m }, // 5.15%
            { 82, 18.5m }, // 5.41%
            { 83, 17.7m }, // 5.65%
            { 84, 16.8m }, // 5.95%
            { 85, 16.0m }, // 6.25%
            { 86, 15.2m }, // 6.58%
            { 87, 14.4m }, // 6.94%
            { 88, 13.7m }, // 7.30%
            { 89, 12.9m }, // 7.75%
            { 90, 12.2m }  // 8.20%
        };

        public decimal GetLifeExpectancyFactor(int age)
        {
            if (age < 72)
                throw new ArgumentOutOfRangeException(nameof(age), "RMD starts at 72/73.");

            if (_factors.TryGetValue(age, out var factor))
                return factor;

            // Fallback: use last known factor for higher ages, or adjust per table
            if (age > 90)
                return 12.2m;

            throw new ArgumentOutOfRangeException(nameof(age), $"No factor defined for age {age}.");
        }
    }
}
