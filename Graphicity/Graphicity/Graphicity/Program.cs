using System.Collections.Generic;
using System.IO;

namespace Graphicity
{
    class Program
    {
        class DiceAndExaminations
        {
            public int numOfDice = 0;
            public int examinations = 0;
            public DiceAndExaminations(int numOfDice, int examinations)
            {
                this.numOfDice = numOfDice;
                this.examinations = examinations;
            }
        }
        static List<DiceAndExaminations> diceAndExaminations = new List<DiceAndExaminations>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            if (lines.Length > 0)
            {
                var numOfLines = int.Parse(lines[0]);
                for (int i = 1; i < numOfLines; i++)
                {
                    var values = lines[i].Split(' ');
                    if (values.Length == 2)
                    {
                        var dice = int.Parse(values[0]);
                        var examinations = int.Parse(values[1]);
                        diceAndExaminations.Add(new DiceAndExaminations(dice, examinations));
                    }
                }
            }

            foreach (var diceAndExamination in diceAndExaminations)
            {
                List<int> dices = new List<int>();
                for (int i = 0; i < diceAndExamination.numOfDice; i++)
                {
                    dices.Add(1);
                }
                bool finished = false;
                do
                {
                    int sum = 0;
                    foreach (var dice in dices)
                    {
                        sum += dice;
                    }
                    if (sum == 13)
                    {

                    }
                } while (!finished);
            }
        }
    }
}
