var lines = File.ReadAllLines("Input.txt");

DoRun(GetNums(lines[0]), GetNums(lines[1]));
DoRun([49787980], [298118510661181]);

void DoRun(long[] times, long[] distances)
{
	int total = 1;
	for (int run = 0; run < times.Length; run++)
	{
		int wins = 0;

		for (int i = 1; i < times[run]; i++)
		{
			long distance = i * (times[run] - i);
			if (distance > distances[run]) { wins++; }
		}

		if (wins > 0) { total *= wins; }
	}
	Console.WriteLine(total);
}

long[] GetNums(string line)
{
	return new System.Text.RegularExpressions.Regex(@"\d+")
		.Matches(line)
		.Select(x => long.Parse(x.Value))
		.ToArray();
}
