using OsuDb.Core;

var scores = OsuDbReader.ReadScores("scores.db");
foreach(var score in scores.Scores)
{
    Console.WriteLine(score);
}
