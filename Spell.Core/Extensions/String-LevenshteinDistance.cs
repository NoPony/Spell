using System;

namespace Spell.Core.Extensions
{
    internal static class String_LevenshteinDistance
    {
        internal static int LevenshteinDistance(this string source, string target)
        {
            int sourceLength = source.Length;
            int targetLength = target.Length;

            // Preconditions
            if (sourceLength == 0)
                return targetLength;

            if (targetLength == 0)
                return sourceLength;

            // Empty matrix
            int[,] matrix = new int[sourceLength + 1, targetLength + 1];

            // [0[0]]
            for (int sourceCursor = 0; sourceCursor <= sourceLength; matrix[sourceCursor, 0] = sourceCursor++) ;
            for (int targetCursor = 0; targetCursor <= targetLength; matrix[0, targetCursor] = targetCursor++) ;

            // [1..n[1..n]]
            for (int sourceCursor = 1; sourceCursor <= sourceLength; sourceCursor++)
            {
                for (int targetCursor = 1; targetCursor <= targetLength; targetCursor++)
                {
                    // Calculate cost
                    int cost = (target[targetCursor - 1] == source[sourceCursor - 1]) ? 0 : 1;

                    // Add it to the matrix
                    matrix[sourceCursor, targetCursor] = Math.Min(
                        Math.Min(
                            matrix[sourceCursor - 1, targetCursor] + 1, 
                            matrix[sourceCursor, targetCursor - 1] + 1),
                        matrix[sourceCursor - 1, targetCursor - 1] + cost);
                }
            }

            // Result == bottom right corner of the matrix
            return matrix[sourceLength, targetLength];
        }
    }
}
