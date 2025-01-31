using System;
using System.Collections.Generic;
using System.Linq;

    public class EmplacementHelper
    {
        static string letterBaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Base-26 for letters
        static string digitBaseChars = "0123456789"; // Base-10 for digits

        // Convert a string location to a numeric value
        public static int ConvertToNumericValue(string location)
        {
            int letterBase = letterBaseChars.Length;
            int digitBase = digitBaseChars.Length;F

            int numericValue = 0;
            int basePower = 1;

            // Parse location from right to left
            for (int i = location.Length - 1; i >= 0; i--)
            {
                char c = location[i];
                if (char.IsDigit(c))
                {
                    // It's a digit
                    numericValue += digitBaseChars.IndexOf(c) * basePower;
                    basePower *= digitBase;
                }
                else if (char.IsLetter(c))
                {
                    // It's a letter
                    numericValue += letterBaseChars.IndexOf(c) * basePower;
                    basePower *= letterBase;
                }
                else
                {
                    throw new ArgumentException($"Character {c} is not a valid letter or digit.");
                }
            }

            return numericValue;
        }

        // Convert a numeric value back to the location string format
        public static string ConvertToLocationString(long numericValue, string startLocation)
        {
            char[] location = new char[startLocation.Length];
            int letterBase = letterBaseChars.Length;
            int digitBase = digitBaseChars.Length;

            // We start from right to left, similar to how we encode
            for (int i = startLocation.Length - 1; i >= 0; i--)
            {
                char originalChar = startLocation[i];

                if (char.IsDigit(originalChar))
                {
                    location[i] = digitBaseChars[(int)(numericValue % digitBase)];
                    numericValue /= digitBase;
                }
                else if (char.IsLetter(originalChar))
                {
                    location[i] = letterBaseChars[(int)(numericValue % letterBase)];
                    numericValue /= letterBase;
                }
                else
                {
                    throw new ArgumentException($"Character {originalChar} is not a valid letter or digit.");
                }
            }

            return new string(location);
        }

        // Generate the list of all possible locations between startLocation and endLocation
        public static List<string> GenerateLocationList(string startLocation, string endLocation)
        {
            if (startLocation.Length != endLocation.Length)
                throw new ArgumentException("The length of the Start and End Location fields must match the length of "Location Format"!);

            long startNumeric = ConvertToNumericValue(startLocation);
            long endNumeric = ConvertToNumericValue(endLocation);

            // Check if the end location is smaller than the start location
            if (endNumeric < startNumeric)
                throw new ArgumentException("The end location must be greater than or equal to the start location.");

            List<string> locations = new List<string>();

            for (long currentNumeric = startNumeric; currentNumeric <= endNumeric; currentNumeric++)
            {
                string location = ConvertToLocationString(currentNumeric, startLocation);
                locations.Add(location);
            }

            return locations;
        }

        public static int GetNumberOfLocations(string format, string startString, string endString)
        {
            // Split the start and end strings into parts based on the format
            List<string> startSubStrings = GetStringSubStrings(startString, format);
            List<string> endSubStrings = GetStringSubStrings(endString, format);

            int totalLocations = 1;

            for (int i = 0; i < startSubStrings.Count; i++)
            {
                // Convert each substring of start and end to numeric values
                int startValue = ConvertToNumericValue(startSubStrings[i]);
                int endValue = ConvertToNumericValue(endSubStrings[i]);
                if (endValue < startValue)
                    return -1;
                // Calculate the number of possible values in this segment
                int segmentCount = (endValue - startValue) + 1;

                // Multiply the result by the count of this segment
                totalLocations *= segmentCount;
            }

            return totalLocations;
        }


        public static List<string> Generate(string format, string startString, string EndString)
        {
            List<string> finalLocations = new List<string>();
            try
            {
                // Split the start and end strings into parts based on the format
                List<string> startSubStrings = GetStringSubStrings(startString, format);
                List<string> endSubStrings = GetStringSubStrings(EndString, format);

                // List to store the generated location segments for each part
                List<List<string>> locationSegments = new List<List<string>>();

                // For each part, generate the list of possible locations between start and end
                for (int i = 0; i < startSubStrings.Count; i++)
                {
                    locationSegments.Add(GenerateLocationList(startSubStrings[i], endSubStrings[i]));
                }

                // Combine all the segments to get the final list of locations
                finalLocations = CombineSegments(locationSegments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return finalLocations;
        }

        // Combine the different segments into complete location strings
        public static List<string> CombineSegments(List<List<string>> segments)
        {
            List<string> result = new List<string>();
            CombineRecursive(segments, 0, "", result);
            return result;
        }

        // Recursive helper to combine segments
        private static void CombineRecursive(List<List<string>> segments, int index, string current, List<string> result)
        {
            if (index == segments.Count)
            {
                result.Add(current);
                return;
            }

            foreach (var segment in segments[index])
            {
                CombineRecursive(segments, index + 1, current + segment, result);
            }
        }


        public static List<string> GetFromatSequences(string format)
        {
            List<string> sequences = new List<string>();
            sequences.Add(format[0].ToString());

            for (int i = 1; i < format.Length; i++)
            {
                char lastChar = sequences.Last()[0];
                char currentChar = format[i];
                if (currentChar != lastChar)
                {
                    sequences.Add(currentChar.ToString());
                }
                else
                {
                    sequences[sequences.Count - 1] = String.Concat(sequences.Last(), currentChar);
                }
            }

            return sequences;
        }

        public static List<string> GetStringSubStrings(string value, string format)
        {

            List<string> subStrings = new List<string>();
            int index = 0;
            foreach (var item in GetFromatSequences(format))
            {
                subStrings.Add(value.Substring(index, item.Length));
                index += item.Length;
            }

            return subStrings;
        }

        public static bool CheckSubStrings(List<string> debutSubStrings, List<string> finSubStrings)
        {
            if (debutSubStrings is null || finSubStrings is null)
            {
                return false;
            }
            for (int i = 0; i < debutSubStrings.Count; i++)
            {
                string debutSubString = debutSubStrings[i];
                string finSubString = finSubStrings[i];
                if (finSubString.Length != debutSubString.Length)
                {
                    return false;
                }
                for (int j = 0; j < debutSubString.Length; j++)
                {
                    char debutSubStringChar = debutSubString[j];
                    char finSubStringChar = finSubString[j];
                    if (char.IsDigit(debutSubStringChar) && char.IsLetter(finSubStringChar)
                        || char.IsDigit(finSubStringChar) && char.IsLetter(debutSubStringChar))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool CheckStartAndEndStrings(string format, string debutString, string endString)
        {
            if (format.IsNullOrEmpty() || debutString.IsNullOrEmpty() || endString.IsNullOrEmpty())
            {
                return false;
            }
            if (debutString.Length != format.Length || endString.Length != format.Length)
            {
                throw new ArgumentException("The length of the Start and End Location fields must match the length of “Location Format”!");
            }
            List<string> debutSubStrings = GetStringSubStrings(debutString, format);
            List<string> finSubStrings = GetStringSubStrings(endString, format);

            if (!CheckSubStrings(debutSubStrings, finSubStrings))
            {
                throw new ArgumentException("The Start and End Location fields must have the same format!");
            }
            int nombreEmplacements = GetNumberOfLocations(format, debutString, endString);

            if (nombreEmplacements < 0)
            {
                throw new ArgumentException("Format incorrect!");
            }

            return true;
        }
    }
