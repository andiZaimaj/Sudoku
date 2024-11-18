public class Program
{
    public static void Main()
    {
        int[][] goodSudoku1 = {
            new int[] {7,8,4, 1,5,9, 3,2,6},
            new int[] {5,3,9, 6,7,2, 8,4,1},
            new int[] {6,1,2, 4,3,8, 7,5,9},
            new int[] {9,2,8, 7,1,5, 4,6,3},
            new int[] {3,5,7, 8,4,6, 1,9,2},
            new int[] {4,6,1, 9,2,3, 5,8,7},
            new int[] {8,7,6, 3,9,4, 2,1,5},
            new int[] {2,4,3, 5,6,1, 9,7,8},
            new int[] {1,9,5, 2,8,7, 6,3,4}
        };

        int[][] goodSudoku2 = {
            new int[] {1,4, 2,3},
            new int[] {3,2, 4,1},
            new int[] {4,1, 3,2},
            new int[] {2,3, 1,4}
        };

        int[][] badSudoku1 =  {
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9}
        };

        int[][] badSudoku2 = {
            new int[] {1,2,3,4,5},
            new int[] {1,2,3,4},
            new int[] {1,2,3,4},
            new int[] {1}
        };

        Console.WriteLine("Validating goodSudoku1: " + ValidateSudoku(goodSudoku1));
        Console.WriteLine("Validating goodSudoku2: " + ValidateSudoku(goodSudoku2));
        Console.WriteLine("Validating badSudoku1: " + ValidateSudoku(badSudoku1));
        Console.WriteLine("Validating badSudoku2: " + ValidateSudoku(badSudoku2));
    }

    public static bool ValidateSudoku(int[][] sudoku)
    {
        int size = sudoku.Length;
        if (size == 0 || Math.Sqrt(size) % 1 != 0) return false; // Check for square size
        int boxSize = (int)Math.Sqrt(size);
        int[] validNumbers = Enumerable.Range(1, size).ToArray();

        // Check if all rows are OK and contain only valid numbers
        if (sudoku.Any(row => row == null || row.Length != size || row.Any(n => n < 1 || n > size)))
            return false;

        // Validation for rows
        bool rowsValid = sudoku.All(row => row.OrderBy(n => n).SequenceEqual(validNumbers));

        // Validation for columns
        bool columnsValid = Enumerable.Range(0, size)
            .Select(col => sudoku.Select(row => row[col]))
            .All(column => column.OrderBy(n => n).SequenceEqual(validNumbers));

        // Validation for boxes
        bool boxesValid = Enumerable.Range(0, size)
            .Select(i => new
            {
                Row = boxSize * (i / boxSize),
                Col = boxSize * (i % boxSize)
            })
            .Select(b => sudoku.Skip(b.Row).Take(boxSize)
                .SelectMany(r => r.Skip(b.Col).Take(boxSize)))
            .All(block => block.OrderBy(n => n).SequenceEqual(validNumbers));

        return rowsValid && columnsValid && boxesValid;
    }

}
