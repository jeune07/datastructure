using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    // 1. Symmetric Pair Finder (O(n))
    public static string[] FindPairs(string[] words)
    {
        var result = new List<string>();
        var wordSet = new HashSet<string>(words);

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue;

            var reversed = new string(new[] { word[1], word[0] });
            if (wordSet.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                wordSet.Remove(reversed);
                wordSet.Remove(word);
            }
        }

        return result.ToArray();
    }

    // 2. Degree Summary From CSV File
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 4) continue;

            var degree = fields[3].Trim();
            if (!string.IsNullOrWhiteSpace(degree))
            {
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    // 3. Anagram Checker
    public static bool IsAnagram(string word1, string word2)
    {
        var dict = new Dictionary<char, int>();

        foreach (char c in word1.ToLower().Where(c => c != ' '))
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }

        foreach (char c in word2.ToLower().Where(c => c != ' '))
        {
            if (!dict.ContainsKey(c)) return false;
            dict[c]--;
            if (dict[c] < 0) return false;
        }

        return dict.Values.All(v => v == 0);
    }

    // 4. Earthquake Daily Summary from USGS JSON Feed
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(jsonStream, options);

        if (featureCollection?.Features == null) return Array.Empty<string>();

        var result = featureCollection.Features
            .Where(f => f.Properties?.Place != null)
            .Select(f =>
            {
                string place = f.Properties.Place;
                string magnitude = f.Properties.Mag.HasValue ? f.Properties.Mag.Value.ToString("0.##") : "N/A";
                return $"{place} - Mag {magnitude}";
            })
            .ToArray();

        return result;
    }
}

// ===== Model Classes for Earthquake JSON =====

public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double? Mag { get; set; }
}

// ===== Example Usage =====
// You can test the functions in a Main method like this:
// public static void Main()
// {
//     string[] pairs = SetsAndMaps.FindPairs(new[] { "am", "at", "ma", "if", "fi" });
//     foreach (var pair in pairs) Console.WriteLine(pair);
//
//     var summary = SetsAndMaps.EarthquakeDailySummary();
//     foreach (var line in summary) Console.WriteLine(line);
// }
