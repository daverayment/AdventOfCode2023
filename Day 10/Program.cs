var north = (-1, 0);
var south = (1, 0);
var east = (0, 1);
var west = (0, -1);

var lines = File.ReadAllLines("Input.txt");

(int Y, int X) start = (-1, -1);

for (int i = 0; i < lines.Length; i++)
{
	if (lines[i].Contains('S'))
	{
		start = (i, lines[i].IndexOf('S'));
		break;
	}
}

Console.WriteLine(GetPathLength(lines, start) / 2);	// 6828

int GetPathLength(string[] maze, (int Y, int X) start)
{
	var direction = ValidDirections(maze, start).First();
	int length = 1;
	var current = (Y: start.Y + direction.DY, X: start.X + direction.DX);
	while (current != start)
	{
		direction = NextDirection(maze[current.Y][current.X], direction);
		current.Y += direction.DY;
		current.X += direction.DX;
		length++;
	}

	return length;
}

IEnumerable<(int DY, int DX)> ValidDirections(string[] maze, (int Y, int X) pos)
{
	if (pos.Y > 0				&& "|F7".Contains(maze[pos.Y - 1][pos.X])) { yield return north; }
	if (pos.Y < maze.Length - 1 && "|LJ".Contains(maze[pos.Y + 1][pos.X])) { yield return south; }
	if (pos.X < maze[0].Length	&& "-7J".Contains(maze[pos.Y][pos.X + 1])) { yield return east; }
	if (pos.X > 0				&& "-LF".Contains(maze[pos.Y][pos.X - 1])) { yield return west; }
}

(int DY, int DX) NextDirection(char current, (int DY, int DX) direction)
{
	// Maze character and direction to new direction.
	var table = new Dictionary<(char, (int, int)), (int, int)>
	{
		{ ('F', west), south },
		{ ('F', north), east },
		{ ('7', east), south },
		{ ('7', north), west },
		{ ('L', south), east },
		{ ('L', west), north },
		{ ('J', south), west },
		{ ('J', east), north },
		{ ('-', east), east },
		{ ('-', west), west },
		{ ('|', north), north },
		{ ('|', south), south }
	};

	return table[(current, direction)];
}