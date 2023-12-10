// See https://aka.ms/new-console-template for more information

using Day4;

var test = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

var lines = test.Split(Environment.NewLine);
var testParser = new Parser(5, 8);
var testSum = 0;
foreach (var line in lines)
{
  var (cardNo, winningNumbers, numbersOnCard) = testParser.ParseLine(line);
  var score = Scorer.Score(winningNumbers, numbersOnCard);
  Console.WriteLine($"Card {cardNo}: {score}");
  testSum += score;
}
Console.WriteLine($"Test sum: {testSum}");

var parser = new Parser(10, 25);
var cards = File.ReadAllLines("input4.txt").Select(x => parser.ParseLine(x)).ToList();
var sum = cards.Select(x => Scorer.Score(x.WinningNumbers, x.NumbersOnCard)).Sum();
Console.WriteLine($"Part 1 sum: {sum}");

// Part 2

// Test:
testSum = Scorer.CountTotalCards(lines.Select(x => testParser.ParseLine(x)).ToList());
Console.WriteLine($"Test total: {testSum} (expect 30)");

var cardsTotal = Scorer.CountTotalCards(cards);
Console.WriteLine($"Total cards: {cardsTotal}");

