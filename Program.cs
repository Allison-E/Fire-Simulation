using System.Security.Cryptography;

Console.Title = "Fire Simulation";
Console.WriteLine("\tFIRE SIMULATION");
Console.WriteLine(new string('-', 31));
Console.WriteLine("Press any key to go to the next time step, Enter to end simulation.");
Console.WriteLine();

int h = 21;
int w = 21;
Cell[,] forest = new Cell[h, w];
Grid grid = new(h, w);

InitialiseDisplay();

while (Console.ReadKey().Key != ConsoleKey.Enter)
{
    if (!forest.Cast<Cell>().Any(c => c.State == CellState.Fire))
    {
        Console.WriteLine("\r\n\r\nSimulation finished.");
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
    grid.ClearGridDisplay();
    grid.ApplyForest(forest);
}

void InitialiseDisplay()
{
    for (int i = 0; i < h; i++)
    {
        for (int j = 0; j < w; j++)
        {
            forest[i, j] = new Cell();
        }
    }

    // Set central tree to a burning tree.
    forest[h / 2, w / 2].SetState(CellState.Fire);

    grid.ApplyForest(forest);
}

CellState Spread(CellState cellState, ICollection<CellState> neighbourStates)
{
    if (neighbourStates.Any(state => state == CellState.Fire) && cellState == CellState.Tree)
    {
        // Either 0 or 1.
        var caughtFireBit = RandomNumberGenerator.GetInt32(2);

        // Tree caught fire by a chance of 50%.
        if (caughtFireBit == 1)
            return CellState.Fire;
    }

    if (cellState == CellState.Fire)
        return CellState.Empty;

    return cellState;
}