using System;
using System.Collections.Generic;
using System.Linq;
using OptimizelySDK;
using OptimizelySDK.Entity;

namespace optimizely_test
{
    class Program
    {
        /// <param name="sdkKey">Optimizely SDK key.</param>
        /// <param name="feature">Name of the feature you want to check.</param>
        /// <param name="iterations">Number of iterations to test the feature.</param>
        /// <param name="userId">Value that uniquely identifies the user.</param>
        /// <param name="attributes">Colon-delimited key-value pairs separated by spaces.</param>
        static void Main(string sdkKey, string feature, string userId = null, int iterations = 1000, params string[] attributes)
        {
            if (string.IsNullOrWhiteSpace(sdkKey) || string.IsNullOrWhiteSpace(feature))
            {
                Console.WriteLine("Usage:\n\toptimizely-test -h");
            }
            else
            {
                CheckOptimizelyFeature(sdkKey, feature, userId, iterations, ExtractAttributes(attributes));
            }
        }

        private static IEnumerable<(string, string)> ExtractAttributes(params string[] attributes)
            => attributes
                ?.Select(attribute => attribute.Split(new[] { ':', '=' }))
                .Select(attribute => (attribute.ElementAtOrDefault(0), attribute.ElementAtOrDefault(1)))
                ?? new (string, string)[] {};

        private static void CheckOptimizelyFeature(string sdkKey, string feature, string userId, int iterations, IEnumerable<(string, string)> attributes)
        {
            var userAttributes = new UserAttributes();

            Console.WriteLine("SDK Key:\t{0}", sdkKey);
            Console.WriteLine("Feature:\t{0}", feature);
            Console.WriteLine("Iterations:\t{0}", iterations);
            Console.WriteLine("Attributes:");

            foreach (var (attribute, value) in attributes)
            {
                if(userAttributes.TryAdd(attribute, value))
                    Console.WriteLine("\t{0}:\t{1}", attribute, value);
            }

            var isEnabledCount = 0d;

            Console.Write("IsEnabled:\n\t");

            using (var optimizelyInstance = OptimizelyFactory.NewDefaultInstance(sdkKey))
            for (int i = 0; i < iterations; i++)
            {
                var isEnabled = optimizelyInstance.IsFeatureEnabled(
                    feature, userId ?? Guid.NewGuid().ToString(), userAttributes);
                Console.Write("{0}", isEnabled ? '*' : '.');

                if (isEnabled)
                    ++isEnabledCount;
            }

            Console.WriteLine("\n\t{0}/{1} ({2}%)", isEnabledCount, iterations, 100 * isEnabledCount / iterations);
        }
    }
}
