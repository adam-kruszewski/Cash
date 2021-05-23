using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cash.Images
{
    class MoneyImageList
    {
        public static string Zloty1 = FullName("1.jpg");
        public static string Zloty2 = FullName("2.jpg");
        public static string Zloty5 = FullName("5.jpg");
        public static string Zloty10 = FullName("10.jpg");
        public static string Zloty20 = FullName("20.jpg");
        public static string Zloty50 = FullName("50.jpg");
        public static string Zloty100 = FullName("100.jpg");
        public static string Zloty200 = FullName("200.jpg");

        public static string Groszy1 = FullName("1gr.jpg");
        public static string Groszy2 = FullName("2gr.jpg");
        public static string Groszy5 = FullName("5gr.jpg");
        public static string Groszy10 = FullName("10gr.jpg");
        public static string Groszy20 = FullName("20gr.jpg");
        public static string Groszy50 = FullName("50gr.jpg");

        private static Dictionary<decimal, string> Mappings = new Dictionary<decimal, string>
        {
            { 0.01m, Groszy1 },
            { 0.02m, Groszy2 },
            { 0.05m, Groszy5 },
            { 0.10m, Groszy10 },
            { 0.20m, Groszy20 },
            { 0.50m, Groszy50 },
            { 1m, Zloty1 },
            { 2m, Zloty2 },
            { 5m, Zloty5 },
            { 10m, Zloty10 },
            { 20m, Zloty20 },
            { 50m, Zloty50 },
            { 100m, Zloty100 },
            { 200m, Zloty200 }
        };

        public static Image GetImageForValue(decimal value)
        {
            if (Mappings.ContainsKey(value))
            {
                using (var stream = typeof(MoneyImageList).Assembly.GetManifestResourceStream(Mappings[value]))
                {
                    return Image.FromStream(stream);
                }
            }

            throw new ArgumentException("Bad money value");
        }

        public static IEnumerable<decimal> GetValues()
        {
            return Mappings.Keys;
        }

        private static string FullName(string name)
        {
            return $"{typeof(MoneyImageList).Namespace}.{name}";
        }
    }
}
