using System.Text.RegularExpressions;

var games = File.ReadAllLines("Input.txt");

int partOneScore = 0;
int partTwoScore = 0;
for (int i = 0; i < games.Length; i++)
{
	var hands = games[i].Split(":")[1].Split(";");
	if (hands.All(IsValid)) {
		partOneScore += i + 1;
	}
	partTwoScore += MinCubes(games[i], "red") * 
		MinCubes(games[i], "green") * MinCubes(games[i], "blue");
}

Console.WriteLine(partOneScore);	// 2505
Console.WriteLine(partTwoScore);    // 70265

bool IsValid(string hand)
{
	return CubeCount(hand, "red") <= 12 &&
		CubeCount(hand, "green") <= 13 &&
		CubeCount(hand, "blue") <= 14;
}

int CubeCount(string hand, string colour)
{
	var match = new Regex($"(\\d+) {colour}").Match(hand);
	return match.Success ? int.Parse(match.Groups[1].Value) : 0;
}

int MinCubes(string game, string colour)
{
	var matches = new Regex($"(\\d+) {colour}").Matches(game);
	return Math.Max(matches.Max(x => int.Parse(x.Groups[1].Value)), 1);
}