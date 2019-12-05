

namespace Montagsmaler
{
    /// <summary>
    /// A static class where all important constants are saved.
    /// </summary>
    public static class Variables
    {
        public static readonly int RoundTime = 60;                              //Time per turn in seconds
        public static readonly int RoundsMax = 3;                               //Rounds to play
        public static readonly int PointsPlayerGuess = 100;                     //Points for a correct guess
        public static readonly int PointsPlayerDraw = 50;                       //Points for the drawer if the word was guessed
        public static readonly int ServerPort = 4333;
        public static readonly int ClientManagerTimeout = 7;                    
        public static readonly int ServerManagerTimeout = 5;
        public static readonly string NetIdentifier = "Montagsmaler";           //Used to identify the server
    }
}
