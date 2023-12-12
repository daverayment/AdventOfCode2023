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

var currentNodes = network.Where(x => x.Key.EndsWith('A'))
	.Select(x => x.Key)
	.ToArray();
string[] originalNodes = new string[currentNodes.Length];
currentNodes.CopyTo(originalNodes, 0);

var lastZ = new int[currentNodes.Length];

index = 0;
string[] newNodes = new string[currentNodes.Length];
Dictionary<string, int> loops = [];

do
{
	char instruction = lines[0][index % lines[0].Length];
	for (int i = 0; i < currentNodes.Length; i++)
	{
		var (Left, Right) = network[currentNodes[i]];
		newNodes[i] = instruction == 'L' ? Left : Right;
		if (newNodes[i].EndsWith('Z'))
		{
			loops.Add(originalNodes[i], index + 1);
			if (loops.Count == currentNodes.Length)
			{
                Console.WriteLine(Numerics.LeastCommonMultiple(
					loops.Select(x => new BigInteger(x.Value)).ToArray()));
				return;
            }
		}
	}
	newNodes.CopyTo(currentNodes, 0);
	index++;
} while (currentNodes.Any(x => !x.EndsWith('Z')));

Console.WriteLine(index);   // 13663968099527
