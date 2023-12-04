using System.Text.RegularExpressions;

int total = 0;
var lines = File.ReadAllLines("Input.txt");
int lineLen = lines[0].Length;
char[,] schematic = new char[lines.Length, lineLen];
for (int y = 0; y < lines.Length; y++)
{
	for (int x = 0; x < lineLen; x++)
	{
		schematic[y, x]	= lines[y][x];
	}
}

for (int y = 0; y < lines.Length; y++)
{
	foreach (Match m in Regex.Matches(lines[y], @"\d+"))
	{
		for (int x = m.Index; x < m.Index + m.Length; x++)
		{
			if (IsBordered(schematic, y, x))
			{
				total += int.Parse(m.Value);
				break;
			}
		}
	}
}

Console.WriteLine(total);

bool IsBordered(char[,] schematic, int y, int x)
{
	return IsSymbol(schematic, y, x - 1) || IsSymbol(schematic, y, x + 1) ||
		IsSymbol(schematic, y - 1, x - 1) || IsSymbol(schematic, y - 1, x) || IsSymbol(schematic, y - 1, x + 1) || IsSymbol(schematic, y, x + 1) ||
		IsSymbol(schematic, y + 1, x - 1) || IsSymbol(schematic, y + 1, x) || IsSymbol(schematic, y + 1, x + 1);
}

bool IsSymbol(char[,] schematic, int y, int x)
{
	if (y < 0 || y >= schematic.GetLength(0) || x < 0 || x >= schematic.GetLength(1))
	{
		return false;
	}
	return schematic[y, x] != '.' && !char.IsDigit(schematic[y, x]);
}
