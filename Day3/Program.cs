// Note: a number may only have 1 neighbouring symbol
var lines = File.ReadAllLines("Input.txt");
int lineLen = lines[0].Length;

List<PartNumber> numbers = [];

for (int y = 0; y < lines.Length; y++)
{
	for (int x = 0; x < lineLen; x++)
	{
		char ch = lines[y][x];
		if (char.IsDigit(ch))
		{
			string numString = "";
			PartNumber num = new();
			do
			{
				numString += lines[y][x];
				num.SymbolLocation ??= GetSymbolLocation(y, x);
				x++;
			} while (x < lineLen && char.IsDigit(lines[y][x]));
			num.Value = int.Parse(numString);
			numbers.Add(num);
		}
	}
}

Console.WriteLine(numbers.Where(x => x.SymbolLocation != null).Sum(x => x.Value));    // 546312

// Gear-neighboring numbers.
var wanted = numbers.Where(loc => {
	if (loc.SymbolLocation == null) { return false; }
	return lines[loc.SymbolLocation.Value.Y][loc.SymbolLocation.Value.X] == '*';
});

// Group and sum the products of each group's values.
long partTwoTotal = wanted.GroupBy(x => x.SymbolLocation)
	.Where(x => x.Count() == 2)
	.Sum(group => group.ElementAt(0).Value * group.ElementAt(1).Value);
	//.Sum(x => x.Aggregate(1, (tot, num) => tot * num.Value));

Console.WriteLine(partTwoTotal);    // 87449461

// Get neighbouring symbol location, or null if not found.
(int Y, int X)? GetSymbolLocation(int y, int x)
{
	foreach ((int dy, int dx) in new[] {
		(0, -1), (0, 1), (-1, -1), (-1, 0), (-1, 1), (1, -1), (1, 0), (1, 1) })
	{
		int y1 = y + dy; int x1 = x + dx;
		if (y1 >= 0 && y1 < lines.Length && x1 >= 0 && x1 < lineLen &&
			lines[y1][x1] != '.' && !char.IsDigit(lines[y1][x1]))
		{
			return (y1, x1);
		}
	}
	return null;
}

record PartNumber { public int Value; public (int Y, int X)? SymbolLocation; }