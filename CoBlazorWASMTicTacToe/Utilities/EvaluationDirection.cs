using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBlazorWASMTicTacToe.Utilities
{
    //First, let's implement an enumeration to show the directions from which we will look for tic-tac-toes:
    public enum EvaluationDirection
    {
        Up,
        UpRight,
        Right,
        DownRight
    }
}
