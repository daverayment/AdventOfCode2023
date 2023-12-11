var handInfo = File.ReadAllLines("Input.txt")
	.Select(x => {
		var parts = x.Split(' ');
		return (hand: parts[0], stake: int.Parse(parts[1]));
	});

Console.WriteLine(handInfo
	.Order(new HandsComparer())
	.Select((h, i) => h.stake * (i + 1))
	.Sum());    // 247961593

class HandsComparer : Comparer<(string hand, int stake)>
{
	public override int Compare((string hand, int stake) x, (string hand, int stake) y)
	{
		const string order = "23456789TJQKA";

		int result = Score(x.hand).CompareTo(Score(y.hand));
		if (result != 0) { return result; }

		for (int i = 0; i < 5; i++)
		{
			int cardDiff = order.IndexOf(x.hand[i]).CompareTo(order.IndexOf(y.hand[i]));
			if (cardDiff != 0) { return cardDiff; }
		}

		// Identical.
		return 0;
	}

	static int Score(string hand)
	{
		// Count how many times each card value appears in the hand.
		var grouped = hand.GroupBy(x => x).Select(x => x.Count());

		// Score tricks.
		return grouped switch
		{
			var counts when counts.Contains(5) => 7,
			var counts when counts.Contains(4) => 6,
			var counts when counts.Contains(3) && counts.Contains(2) => 5,
			var counts when counts.Contains(3) => 4,
			var counts when counts.Count(x => x == 2) == 2 => 3,
			var counts when counts.Contains(2) => 2,
			_ => 1
		};
    }
}
