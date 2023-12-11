var handInfo = File.ReadAllLines("Input.txt")
	.Select(x => {
		var parts = x.Split(' ');
		return (hand: parts[0], stake: int.Parse(parts[1]));
	});

SumHands(handInfo); // 247961593
SumHands(handInfo, isPartTwo: true);    // 248750699

static void SumHands(IEnumerable<(string, int stake)> hands, bool isPartTwo = false)
{
	Console.WriteLine(hands.Order(new HandsComparer(isPartTwo))
		.Select((hand, i) => hand.stake * (i + 1))
		.Sum());
}

class HandsComparer(bool partTwo = false) : Comparer<(string hand, int stake)>
{
	public override int Compare((string hand, int stake) x, (string hand, int stake) y)
	{
		string order = partTwo ? "J23456789TQKA" : "23456789TJQKA";

		int result = Score(x.hand, partTwo).CompareTo(Score(y.hand, partTwo));
		if (result != 0) { return result; }

		for (int i = 0; i < 5; i++)
		{
			int cardDiff = order.IndexOf(x.hand[i]).CompareTo(order.IndexOf(y.hand[i]));
			if (cardDiff != 0) { return cardDiff; }
		}

		// Identical.
		return 0;
	}

	enum Tricks
	{
		HighCard = 1, Pair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind
	}

	static int Score(string hand, bool partTwo)
	{
		// Count how many times each card value appears in the hand.
		var grouped = hand.GroupBy(x => x)
			.ToDictionary(group => group.Key, group => group.Count());
		if (partTwo)
		{
			grouped.Remove('J');
		}

		// Score tricks.
		Tricks score = grouped switch
		{
			var counts when counts.ContainsValue(5) => Tricks.FiveOfAKind,
			var counts when counts.ContainsValue(4) => Tricks.FourOfAKind,
			var counts when counts.ContainsValue(3) && counts.ContainsValue(2) => Tricks.FullHouse,
			var counts when counts.ContainsValue(3) => Tricks.ThreeOfAKind,
			var counts when counts.Values.Count(x => x == 2) == 2 => Tricks.TwoPair,
			var counts when counts.ContainsValue(2) => Tricks.Pair,
			_ => Tricks.HighCard
		};

		if (partTwo)
		{
			int wildcardCount = hand.Count(x => x == 'J');
			for(int i = 0; i < wildcardCount; i++)
			{
				score = score switch
				{
					Tricks.HighCard => Tricks.Pair,
					Tricks.Pair => Tricks.ThreeOfAKind,
					Tricks.TwoPair => Tricks.FullHouse,
					Tricks.ThreeOfAKind => Tricks.FourOfAKind,
					Tricks.FullHouse => Tricks.FourOfAKind,
					Tricks.FourOfAKind => Tricks.FiveOfAKind,
					Tricks.FiveOfAKind => Tricks.FiveOfAKind
				};
			}
		}

		return (int)score;
	}
}
