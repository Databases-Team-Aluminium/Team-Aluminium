namespace ArtGallery.ConsoleClient.Utils
{
    using System;
    using System.Text;

    public class RandomGenerator
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        private readonly Random random;

        public static RandomGenerator Create()
        {
            return new RandomGenerator();
        }

        private RandomGenerator()
        {
            this.random = new Random();
        }

        public int GetIntegerBetween(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }

        public string GetStringBetween(int min, int max)
        {
            var result = new StringBuilder();

            int stringLength = this.GetIntegerBetween(min, max);
            for (int i = 0; i < stringLength; i++)
            {
                int randomCharIndex = this.GetIntegerBetween(0, Alphabet.Length - 1);
                char character = Alphabet[randomCharIndex];
                result.Append(character);
            }

            return result.ToString();
        }

        public DateTime GetRandomDate(DateTime? start = null, DateTime? end = null)
        {
            start = start ?? new DateTime(1940, 01, 01);
            end = end ?? new DateTime(1985, 12, 31);

            TimeSpan? span = end - start;
            int daysToAdd = this.GetIntegerBetween(0, span.Value.Days);

            return start.Value.AddDays(daysToAdd);
        }
    }
}
