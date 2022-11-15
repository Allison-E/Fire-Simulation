using System.Security.Cryptography;

namespace Fire_Simulation;

/// <summary>
/// A cell in the grid which may be an empty site, tree or fire.
/// </summary>
internal class Cell
{
    /// <summary>
    /// Creates an instance of <see cref="Cell"/> with a random state
    /// which would either be a tree or empty site.
    /// </summary>
    public Cell()
    {
        var randomValue = RandomNumberGenerator.GetInt32(2);

        State = (CellState)randomValue;
    }

    /// <summary>
    /// Creates an instance of <see cref="Cell"/> with the given state.
    /// </summary>
    /// <param name="state"></param>
    public Cell(CellState state) => State = state;



    /// <summary>
    /// The current state of the cell.
    /// </summary>
    public CellState State { get; private set; }

    /// <summary>
    /// Set the state of the cell.
    /// </summary>
    /// <param name="state">Cell state.</param>
    public void SetState(CellState state) => State = state;

    /// <summary>
    /// Returns the value contained in the cell.
    /// </summary>
    /// <remarks><![CDATA[Returns & for a tree, x for a fire or a space for
    /// an empty site]]>.</remarks>
    /// <returns>A character.</returns>
    public char GetValue()
    {
        switch (State)
        {
            case CellState.Tree:
                return '&';
            case CellState.Fire:
                return 'x';
            case CellState.Empty:
            default:
                return ' ';
        }
    }
}