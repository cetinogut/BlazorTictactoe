using CoBlazorWASMTicTacToe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBlazorWASMTicTacToe.Models
{
    public class GamePiece
    {
        public PieceStyle Style;

        public GamePiece()
        {
            Style = PieceStyle.Blank;
        }
    }
}
