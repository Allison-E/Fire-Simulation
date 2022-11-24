using System.Security.Cryptography;

Console.Title = "Fire Simulation";
Console.WriteLine("\tFIRE SIMULATION");
Console.WriteLine(new string('-', 31));


int h = 21;
int w = 21;
Cell[,] forest = new Cell[h, w];
Grid grid = new(h, w);
Weather weather = new();

Initialize();
Console.WriteLine(weather + "\r\n");
Display();

while (Console.ReadKey().Key != ConsoleKey.Enter)
{
    if (!forest.Cast<Cell>().Any(c => c.State == CellState.Fire))
    {
        Console.WriteLine(new string('=', 31));
        Console.WriteLine("Simulation finished. Press any key to close.");
        Console.ReadKey();
        break;
    }

    Cell[,] newForest = new Cell[h, w];

    // Update cells.
    for (int i = 0; i < h; i++)
    {
        for (int j = 0; j < w; j++)
        {
            var neighbourStates = new List<CellState>();

            // North neighbour.
            if (i > 0)
                neighbourStates.Add(forest[i - 1, j].State);

            // South neighbour.
            if (i != h - 1)
                neighbourStates.Add(forest[i + 1, j].State);

            // West neighbour.
            if (j > 0)
                neighbourStates.Add(forest[i, j - 1].State);

            // East neighbour.
            if (j != w - 1)
                neighbourStates.Add(forest[i, j + 1].State);

            var newState = Spread(forest[i, j].State, neighbourStates);
            newForest[i, j] = new Cell(newState);
        }
    }

    forest = newForest;
    grid.ApplyForest(forest);
    Display();
}

void Initialize()
{
    for (int i = 0; i < h; i++)
    {
        for (int j = 0; j < w; j++)
        {
            forest[i, j] = new Cell();
        }
    }

    // Set the central tree to a burning tree.
    forest[h / 2, w / 2].SetState(CellState.Fire);

    grid.ApplyForest(forest);
}

void Display()
{
    Console.WriteLine("Press any key to go to the next time step, Enter to end simulation.");
    Console.WriteLine();
    Console.WriteLine(grid);
}

CellState Spread(CellState cellState, ICollection<CellState> neighbourStates)
{
    if (neighbourStates.Any(state => state == CellState.Fire) && cellState == CellState.Tree)
    {
        // Either 0 or 1.
        var probabilityItCatchesFire = RandomNumberGenerator.GetInt32(1, 101);
        probabilityItCatchesFire += weather.GetInfluence();

        // Tree caught fire by a chance of 50%.
        if (probabilityItCatchesFire > 50)
            return CellState.Fire;
    }

    if (cellState == CellState.Fire)
        return CellState.Empty;

    return cellState;
}