using System.Text.Json;

namespace AdventOfCode.Days;

public class Day13 : AdventOfCodeDay
{
    protected override int GetDay() => 13;

    protected override string GetTask1Solution()
    {
        int sortedIndexesSum = 0;
        List<(JsonElement, JsonElement)> pairs = GetPairs();

        for (int i = 0; i < pairs.Count; i++)
        {
            if (Compare(pairs[i].Item1, pairs[i].Item2) < 0)
            {
                sortedIndexesSum += i + 1;
            }
        }

        return sortedIndexesSum.ToString();

        List<(JsonElement, JsonElement)> GetPairs()
        {
            string inputPath = Path.Combine("Assets", "day13.txt");
            string[] input = File.ReadAllLines(inputPath);

            List<(JsonElement, JsonElement)> pairs = new();

            for (int i = 0; i < input.Length; i += 3)
            {
                pairs.Add((JsonSerializer.Deserialize<JsonElement>(input[i]), JsonSerializer.Deserialize<JsonElement>(input[i + 1])));
            }

            return pairs;
        }
    }

    protected override string GetTask2Solution()
    {
        int decoderKey = 0;
        List<JsonElement> packets = GetPackets();

        JsonElement key1 = JsonSerializer.Deserialize<JsonElement>("[[2]]");
        JsonElement key2 = JsonSerializer.Deserialize<JsonElement>("[[6]]");
        
        packets.Add(key1);
        packets.Add(key2);

        QuickSort(packets, 0, packets.Count - 1);

        int keyIndex1 = packets.FindIndex(p => p.Equals(key1)) + 1;
        int keyIndex2 = packets.FindIndex(p => p.Equals(key2)) + 1;
        
        return (keyIndex1 * keyIndex2).ToString();

        List<JsonElement> GetPackets()
        {
            string inputPath = Path.Combine("Assets", "day13.txt");
            string[] input = File.ReadAllLines(inputPath);

            List<JsonElement> packets = new();

            for (int i = 0; i < input.Length; i += 3)
            {
                packets.Add(JsonSerializer.Deserialize<JsonElement>(input[i]));
                packets.Add(JsonSerializer.Deserialize<JsonElement>(input[i + 1]));
            }

            return packets;
        }
    }

    private int Compare(JsonElement item1, JsonElement item2)
    {
        int result = (item1.ValueKind, item2.ValueKind) switch
        {
            (JsonValueKind.Number, JsonValueKind.Number) => item1.GetInt32().CompareTo(item2.GetInt32()),
            (JsonValueKind.Number, JsonValueKind.Array) => Compare(IntToJsonElement(item1.GetInt32()), item2),
            (JsonValueKind.Array, JsonValueKind.Number) => Compare(item1, IntToJsonElement(item2.GetInt32())),
            _ => CompareArrays(item1, item2),
        };

        return result;

        JsonElement IntToJsonElement(int value)
        {
            return JsonSerializer.Deserialize<JsonElement>($"[{value}]");
        }

        int CompareArrays(JsonElement array1, JsonElement array2)
        {
            JsonElement.ArrayEnumerator enumerator1 = array1.EnumerateArray();
            JsonElement.ArrayEnumerator enumerator2 = array2.EnumerateArray();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                result = Compare(enumerator1.Current, enumerator2.Current);
                if (result != 0)
                    return result;
            }

            return array1.GetArrayLength().CompareTo(array2.GetArrayLength());
        }
    }

    private void QuickSort(List<JsonElement> items, int low, int high)
    {
        if (low >= high)
            return;

        int pivotIndex = Partition(items, low, high);
        QuickSort(items, low, pivotIndex - 1);
        QuickSort(items, pivotIndex + 1, high);

        int Partition(List<JsonElement> items, int low, int high)
        {
            JsonElement pivot = items[high];
            
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (Compare(items[j], pivot) < 1)
                {
                    i++;
                    (items[i], items[j]) = (items[j], items[i]);
                }
            }

            (items[i + 1], items[high]) = (items[high], items[i + 1]);

            return i + 1;
        }
    }
}