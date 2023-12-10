using Toolbox;

var lines = File.ReadAllLines("Input.txt");
long minSeed = long.MaxValue;
int lineNum;

foreach (long seed in GetNums(lines[0]))
{
	long current = seed;
	lineNum = 2;
	while (lineNum < lines.Length)
	{
		string line = lines[lineNum++];
		if (line.Contains(':') || line.Length == 0) { continue; }
		(long destination, long source, long range) = GetNums(line);
		if (current < source || current >= source + range) { continue; }
		// Match found. Update the location and proceed to the next set of maps.
		current += destination - source;
		while (lineNum < lines.Length && lines[lineNum].Length > 0) { lineNum++; }
	}
	minSeed = Math.Min(minSeed, current);
}

Console.WriteLine(minSeed); // 457535844

// Part two.
// Seed intervals as (start, length) tuples.
var ranges = GetNums(lines[0])
	.Chunk(2)
	.Select(x => (Start: x.ElementAt(0), Length: x.ElementAt(1)))
	.ToList();
// The new overlapping intervals found in the current group's processing.
List<(long Start, long Length)> newRanges = [];
// The non-matched intervals to keep processing on every line.
List<(long Start, long Length)> nonMatches = [];

// First group starts on line 3
lineNum = 3;
while (lineNum < lines.Length)
{
	string line = lines[lineNum];

	// Next group start?
	if (line.Trim().Length == 0)
	{
		// Add in the matched ranges from the previous group.
        ranges.AddRange(newRanges);
		nonMatches.Clear();
		newRanges.Clear();
		lineNum += 2;	// skip blank line and group header
		continue;
	}

	(long destination, long source, long length) = GetNums(line);

	foreach (var range in ranges)
	{
        var o = range.GetOverlap((source, length));

		if (o != null)
		{
			// Add the overlapping part.
			long offset = destination - source;
            newRanges.Add((o.Value.Start + offset, o.Value.Length));

			// Add any non-overlapping parts to be checked with the next line.
			if (range.Start < o.Value.Start)
			{
                nonMatches.Add((range.Start, o.Value.Start - range.Start));
			}
			if (range.Start + range.Length > o.Value.Start + o.Value.Length)
			{
                nonMatches.Add((o.Value.Start + o.Value.Length,
					(range.Start + range.Length) - (o.Value.Start + o.Value.Length)));
			}
		}
		else
		{
			// No overlap. Add the whole range to be checked again.
			nonMatches.Add(range);
		}
	}

	// We have processed all the seed ranges against this line. Reset to only
	// include non-matched ranges.
	ranges = new List<(long Start, long Length)>(nonMatches.Distinct());
	nonMatches.Clear();

	lineNum++;
}

// Final step to add in any last matches.
ranges.AddRange(newRanges);
Console.WriteLine(ranges.Min(x => x.Start));    // 41222968

static IEnumerable<long> GetNums(string line)
{
	return line.Split(' ').Where(x => !x.EndsWith(':')).Select(long.Parse);
}
