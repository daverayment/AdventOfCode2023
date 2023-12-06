using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("Input.txt");
Dictionary<int, int> totals = [];

Console.WriteLine(lines.Sum(GetMatches));	// 862

// Part two. Work back from end so we don't have to keep track of multiple running totals.
for (int i = lines.Length - 1; i >= 0; i--)
{
	int matches = GetMatches(lines[i]);
	totals[i] = 1 + Enumerable.Range(1, matches).Sum(j => totals[i + j]);
}

Console.WriteLine(totals.Sum(x => x.Value));    // 13768818

// How many candidate cards match the winning numbers?
static int GetMatches(string line)
{
	var nums = Regex.Matches(line, @"\d+").Select(x => x.Value);
	return nums.Skip(1).Take(10).Intersect(nums.Skip(11)).Count();
}
