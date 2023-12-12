using Toolbox;
using System.Text.RegularExpressions;
using System.Numerics;

var lines = File.ReadAllLines("Input.txt");
Regex rex = new(@"\w+");

Dictionary<string, (string Left, string Right)> network = [];
for (int i = 2; i < lines.Length; i++)
{
	var matches = rex.Matches(lines[i]);
	network[matches[0].Value] = (matches[1].Value, matches[2].Value);
}

string next = "AAA";
int index = 0;
do
{
	var (Left, Right) = network[next];
	char instruction = lines[0][index % lines[0].Length];
	next = instruction == 'L' ? Left : Right;
	index++;
} while (next != "ZZZ");

Console.WriteLine(index);   // 19199

List<int> loops = [];
foreach (var node in network.Where(x => x.Key.EndsWith('A')))
{
	index = 0;
	string current = node.Key;
	do
	{
		var (Left, Right) = network[current];
		char instruction = lines[0][index % lines[0].Length];
		current = instruction == 'L' ? Left : Right;
		index++;
		if (current.EndsWith('Z'))
		{
			loops.Add(index);
			break;
		}
	} while (true);
}

Console.WriteLine(Numerics.LeastCommonMultiple(
	loops.Select(x => new BigInteger(x)).ToArray())); // 13663968099527
