using System.Security.Cryptography;

namespace Fire_Simulation;
internal class Weather
{
    private int _humidityPercentage;

    /// <summary>
    /// Amount of water in the atmosphere.
    /// </summary>
    public int HumidityPercentage
    {
        get => _humidityPercentage;
        set => _humidityPercentage = value is >= 0 and <= 100 ? value : RandomNumberGenerator.GetInt32(101);
    }

    public Weather()
    {
        HumidityPercentage = RandomNumberGenerator.GetInt32(101);
    }

    /// <summary>
    /// Get the amount by which the weather affects the way
    /// the fire spreads.
    /// </summary>
    /// <returns>A positive number if the factors cumulatively increase
    /// the chances of trees catching fire. Otherwise, a negative number.</returns>
    public int GetInfluence()
    {
        var influence = Math.Abs(50 - HumidityPercentage) / 2;

        return HumidityPercentage > 50 ? -influence : influence;
    }

    public override string ToString()
    {
        return $"Weather Forecast:\r\n- Humidity: {HumidityPercentage}%";
    }
}
