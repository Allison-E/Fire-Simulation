using System.Text;

namespace Fire_Simulation;

/// <summary>
/// The grid of the forest.
/// </summary>
internal class Grid
{
    public Grid(int height, int width)
    {
        Width = width;
        Height = height;
        InitialiseMap();
    }

    /// <summary>
    /// Width of the grid.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Height of the grid.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Map containing chars that represent the current visual state of the forest.
    /// </summary>
    public char[,] Map { get; private set; }

    /// <summary>
    /// Updates map based on the forest's cells.
    /// </summary>
    /// <param name="forest">Forest.</param>
    /// <exception cref="Exception"></exception>
    public void ApplyForest(Cell[,] forest)
    {
        if (!ForestAndMapAreSameSize(forest))
            throw new Exception("Forest is not the same size as map.");

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Map[i, j] = forest[i, j].GetValue();
            }
        }

        Console.Write(this);
    }

    /// <summary>
    /// Set width of grid.
    /// </summary>
    /// <param name="width"></param>
    public void SetWidth(int width)
    {
        Width = width;
        InitialiseMap();
    }

    /// <summary>
    /// Set height of grid.
    /// </summary>
    /// <param name="height"></param>
    public void SetHeight(int height)
    {
        Height = height;
        InitialiseMap();
    }

    /// <summary>
    /// Clears grid on display.
    /// </summary>
    /// <remarks>The cursor has to be on the same line as the
    /// ending of this grid to work.</remarks>
    public void ClearGridDisplay()
    {
        int i = 1;

        while (i < Height)
        {
            if (i == 1)
                Console.SetCursorPosition(0, Console.CursorTop);

            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            i++;
        }
    }

    /// <summary>
    /// Returns the <see cref="Grid.Map"/> as a string to be displayed.
    /// </summary>
    /// <returns>A string representation of the map.</returns>
    public override string ToString()
    {
        StringBuilder builder = new();

        for (int i = 0; i < Height; i++)
        {
            builder.Append("\t|");
            for (int j = 0; j < Width; j++)
            {
                builder.Append(Map[i, j]);
            }

            if (i != Height - 1)
                builder.Append("|\r\n");
            else
                builder.Append("|");
        }
        return builder.ToString().TrimEnd();
    }

    private void InitialiseMap()
    {
        Map = new char[Height, Width];
    }

    private bool ForestAndMapAreSameSize(Cell[,] forest)
    {
        if (forest.Rank != Map.Rank)
            return false;

        return true;
    }
}
