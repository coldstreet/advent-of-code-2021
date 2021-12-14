﻿namespace AdventOfCode2021
{
    public static class Day13_TransparentOrigami
    {
        public static int FoldUpAndCountDots(string[] input)
        {
            var separatorIndex = Array.IndexOf(input, "");

            var dotMap = input
                .Take(separatorIndex)
                .Select(l => l.Split(',').Select(int.Parse).ToArray());

            var maxIndexX = dotMap.Select(x => x[0]).Max();
            var maxIndexY = dotMap.Select(x => x[1]).Max();

            var grid = new int[maxIndexX + 1, maxIndexY + 1];
            foreach (var dot in dotMap)
            {
                grid[dot[0], dot[1]] = 1;
            }

            // get fold instructions
            var foldInfo = input.TakeLast(input.Length - (separatorIndex + 1)).ToArray();
            // for part I - TEMP
            foldInfo = foldInfo.Take(1).ToArray();

            int count = 0;
            foreach(var foldInstruction in foldInfo)
            {
                var newGridLengthX = maxIndexX + 1;
                var newGridLengthY = maxIndexY + 1;
                bool foldUp = false;
                if (foldInstruction.Contains("x"))
                {
                    newGridLengthX = int.Parse(foldInstruction.Remove(0, 13));
                }
                else
                {
                    newGridLengthY = int.Parse(foldInstruction.Remove(0, 13));
                    foldUp = true;
                }

                var newFoldedUpGrid = new int[newGridLengthX, newGridLengthY];

                for (int y = 0; y < newGridLengthY; y++)
                {
                    for (int x = 0; x < newGridLengthX; x++)
                    {
                        if (foldUp)
                        {
                            newFoldedUpGrid[x, y] = grid[x, y] == 1 || grid[x, maxIndexY - y] == 1 ? 1 : 0;
                        }
                        else
                        {
                            newFoldedUpGrid[x, y] = grid[x, y] == 1 || grid[maxIndexX - x, y] == 1 ? 1 : 0;
                        }
                    }
                }

                // count dots
                for (int y = 0; y < newFoldedUpGrid.GetLength(1); y++)
                {
                    for (int x = 0; x < newFoldedUpGrid.GetLength(0); x++)
                    {
                        if (newFoldedUpGrid[x, y] == 1)
                        {
                            count++;
                        }
                    }
                }

                maxIndexX = newGridLengthX - 1;
                maxIndexY = newGridLengthY - 1;
            }


            return count;
        }
    }
}

