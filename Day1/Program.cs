List<string> digits = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

// Part one. (55607).
Console.WriteLine(File.ReadAllLines("Input.txt").Sum(line => LineValue(line, true)));

// Part two. (55291).
Console.WriteLine(File.ReadAllLines("Input.txt").Sum(line => LineValue(line, false)));

int LineValue(string line, bool partOne)
{
	// NB: (?=...) zero-width lookahead required for overlapping elements in part two like "twone" or "oneight".
	string pattern = partOne ? @"\d" : $"(?=([0-9]|{string.Join("|", digits)}))";
	// Match groups are dependent on the part.
	int groupIndex = partOne ? 0 : 1;
	var matches = System.Text.RegularExpressions.Regex.Matches(line, pattern);
	if (matches.Count == 0) { return 0; }
	int first = Convert(matches.First().Groups[groupIndex].Value);
	int second = matches.Count > 1 ? Convert(matches.Last().Groups[groupIndex].Value) : first;

	return first * 10 + second;
}

int Convert(string match)
{
	if (int.TryParse(match, out int result))
	{
		return result;
	}
	return digits.IndexOf(match) + 1;
}
