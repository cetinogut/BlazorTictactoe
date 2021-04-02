using CoBlazorWASMTicTacToe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBlazorWASMTicTacToe.Models
{
    // The WinningPlay class will be used to store the winner if we find a tic-tac-toe:
    public class WinningPlay
    {
        public List<string> WinningMoves { get; set; }
        public EvaluationDirection WinningDirection { get; set; }
        public PieceStyle WinningStyle { get; set; }
    }
}
