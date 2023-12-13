long partOne = 0;
long partTwo = 0;

foreach (string line in File.ReadAllLines("Input.txt"))
{
	List<IEnumerable<int>> triangle = [];
	var working = line.Split(' ').Select(int.Parse);

	do
	{
		triangle.Add(working);
		working = working.Zip(working.Skip(1), (a, b) => b - a).ToList();
	} while (!working.All(x => x == 0));

	int toAdd = 0;
	int toSub = 0;
	for (int i = triangle.Count - 1; i >= 0; i--)
	{
		toAdd += triangle[i].Last();
		toSub = triangle[i].First() - toSub;
	}

	partOne += toAdd;
	partTwo += toSub;
}

Console.WriteLine(partOne);	// 1974232246
Console.WriteLine(partTwo);	// 928
