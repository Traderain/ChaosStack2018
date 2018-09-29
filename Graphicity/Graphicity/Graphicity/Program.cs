using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Graphicity
{
    class Program
    {
        class DiceAndExaminations
        {
            public int numOfDice = 0;
            public long examinations = 0;
            public DiceAndExaminations(int numOfDice, long examinations)
            {
                this.numOfDice = numOfDice;
                this.examinations = examinations;
            }
        }
        static List<DiceAndExaminations> diceAndExaminations = new List<DiceAndExaminations>();

        static Dictionary<int, int> dict = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 21},
            { 4, 140 },
            { 5, 420 },
            { 6, 756},
            { 7, 917},
            { 8, 792},
            { 9, 495},
            { 10, 220},
            { 11, 66},
            { 12, 12},
            { 13, 1}
        };

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            if (lines.Length > 0)
            {
                var numOfLines = int.Parse(lines[0]);
                for (int i = 1; i < numOfLines + 1; i++)
                {
                    var values = lines[i].Split(' ');
                    if (values.Length == 2)
                    {
                        var dice = int.Parse(values[0]);
                        var examinations = long.Parse(values[1]);
                        diceAndExaminations.Add(new DiceAndExaminations(dice, examinations));
                    }
                }
            }

            List<string> fileOut = new List<string>();
            foreach (var diceAndExamination in diceAndExaminations)
            {
                double probability = 0;
                double expectedDelay = 0;
                double currProb = 0;
                if (diceAndExamination.numOfDice <= 13)
                {
                    List<int> dices = new List<int>();
                    for (int i = 0; i < diceAndExamination.numOfDice; i++)
                    {
                        dices.Add(1);
                    }
                    int goodCases = 0;
                    long allCases = 6;
                    // Calculating all cases 6^n
                    for (int i = 1; i < dices.Count; i++)
                    {
                        allCases *= 6;
                    }
                    bool finished = false;
                    /*do
                    {
                        int sum = 0;
                        for (int i = 0; i < dices.Count; i++)
                        {
                            sum += dices[i];
                        }
                        if (sum == 13)
                        {
                            goodCases++;
                        }
                        finished = true;
                        for (int i = 0; i < dices.Count; i++)
                        {
                            if (dices[i] != 6)
                            {
                                // If every number before is 6, make them 1
                                bool all6 = true;
                                for (int j = 0; j < i; j++)
                                {
                                    if (dices[j] != 6)
                                    {
                                        all6 = false;
                                    }
                                }
                                if (all6)
                                {
                                    for (int j = 0; j < i; j++)
                                    {
                                        dices[j] = 1;
                                    }
                                }
                                // Increment last non-6 number
                                dices[i]++;
                                finished = false;
                                break;
                            }
                        }
                    } while(!finished);*/
                    probability = (double)dict[diceAndExamination.numOfDice] / allCases;
                    currProb = probability;
                    for (long i = 1; i < diceAndExamination.examinations; i++)
                    {
                        probability *= currProb;
                        if (probability == 0)
                        {
                            break;
                        }
                    }
                    expectedDelay = probability / (1d - currProb);
                }
                fileOut.Add(currProb + " " + expectedDelay);
            }
            File.WriteAllLines("output.txt", fileOut.ToArray());
        }
    }
}
