long total = 0;

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
	for (int i = triangle.Count - 1; i >= 0; i--)
	{
		toAdd += triangle[i].Last();
	}

	total += toAdd;
}

Console.WriteLine(total);   // 1974232246
