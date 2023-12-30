var north = (-1, 0);
var south = (1, 0);
var east = (0, 1);
var west = (0, -1);

var lines = File.ReadAllLines("Input.txt");

(int Y, int X) start = (-1, -1);

// Keep track of vertices for area calculation.
List<(int Y, int X)> vertices = [];

for (int y = 0; y < lines.Length; y++)
{
	if (lines[y].Contains('S'))
	{
		start = (y, lines[y].IndexOf('S'));
		break;
	}
}

vertices.Add(start);

var direction = ValidDirections(lines, start).First();
var current = (Y: start.Y + direction.DY, X: start.X + direction.DX);
int length = 1;

double area = 0;	// total area of enclosed space

while (current != start)
{
	var newDirection = NextDirection(lines[current.Y][current.X], direction);
	if (newDirection != direction)
	{
		vertices.Add(current);
		direction = newDirection;
		UpdateArea(vertices, ref area);
	}

	current.Y += direction.DY;
	current.X += direction.DX;
	length++;
}

// Part One.
Console.WriteLine(length / 2);  // 6828

// Part Two.
// Close the polygon and calculate the area total.
vertices.Add(start);
UpdateArea(vertices, ref area);
area = Math.Abs(area / 2);
// Need to make an adjustment for the size of the pipeline itself.
// TODO: use another method (shifting the grid?) to remove the need for this.
area = area - (length / 2) + 1;
Console.WriteLine(area);	// 459

void UpdateArea(List<(int Y, int X)> vertices, ref double area)
{
	if (vertices.Count < 2) return;

	int j = vertices.Count - 2;
	int i = vertices.Count - 1;
	area += (vertices[j].X * vertices[i].Y) - (vertices[i].X * vertices[j].Y);
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
	// Map current direction and new character to new direction.
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
