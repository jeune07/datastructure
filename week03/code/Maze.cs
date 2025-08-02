using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        var result = new List<string>();
        var wordSet = new HashSet<string>(words);

        foreach (var word in words)
        {
            if (word.Length != 2 || word[0] == word[1]) continue;

            var reversed = new string(new[] { word[1], word[0] });
            if (wordSet.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                wordSet.Remove(word);
                wordSet.Remove(reversed);
            }
        }

        return result.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length >= 4)
            {
                var degree = fields[3].Trim();
                if (!string.IsNullOrWhiteSpace(degree))
                {
                    if (!degrees.TryAdd(degree, 1))
                        degrees[degree]++;
                }
            }
        }

        return degrees;
    }

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

    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = client.Send(request);
        using var jsonStream = response.Content.ReadAsStream();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = JsonSerializer.Deserialize<FeatureCollection>(jsonStream, options);

        if (data?.Features == null) return Array.Empty<string>();

        return data.Features
            .Where(f => f.Properties?.Place != null)
            .Select(f =>
            {
                string place = f.Properties.Place;
                string mag = f.Properties.Mag.HasValue ? f.Properties.Mag.Value.ToString("0.##") : "N/A";
                return $"{place} - Mag {mag}";
            })
            .ToArray();
    }
}

// Earthquake JSON models
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

// Maze class
public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    public void MoveLeft()
    {
        if (_mazeMap.TryGetValue((_currX, _currY), out var directions) && directions[0])
            _currX--;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveRight()
    {
        if (_mazeMap.TryGetValue((_currX, _currY), out var directions) && directions[1])
            _currX++;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveUp()
    {
        if (_mazeMap.TryGetValue((_currX, _currY), out var directions) && directions[2])
            _currY--;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public void MoveDown()
    {
        if (_mazeMap.TryGetValue((_currX, _currY), out var directions) && directions[3])
            _currY++;
        else
            throw new InvalidOperationException("Can't go that way!");
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
