using System;
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

        static int factorial(int num)
        {
            int result = num;
            for (int i = 1; i < num; i++)
            {
                result *= i;
            }
            return result;
        }

        static int findSimilarGroups(List<int> dices)
        {
            List<int> similarGroups = new List<int>();
            int nums = 1, previousNum = 0, goodCases = 0;
            for (int i = 0; i < dices.Count; i++)
            {
                if (previousNum == dices[i])
                {
                    nums++;
                }
                else
                {
                    if (nums != 1)
                    {
                        similarGroups.Add(nums);
                    }
                    nums = 1;
                }
                previousNum = dices[i];
            }
            if (nums > 1)
            {
                similarGroups.Add(nums);
            }
            int division = 1;
            for (int i = 0; i < similarGroups.Count; i++)
            {
                division *= factorial(similarGroups[i]);
            }
            goodCases += factorial(dices.Count) / division;
            return goodCases;
        }

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
                int goodCases = 0;
                int allCases = 6;
                for (int i = 1; i < dices.Count; i++)
                {
                    allCases *= 6;
                }
                bool finished = false;
                do
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
                            dices[i]++;
                            finished = false;
                            break;
                        }
                    }
                } while (!finished);
                double probability = (double)goodCases / allCases;
                for (int i = 1; i < diceAndExamination.examinations; i++)
                {
                    probability *= probability;
                }
            }
        }
    }
}
