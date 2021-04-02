using CoBlazorWASMTicTacToe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBlazorWASMTicTacToe.Models
{
    public class GameBoard
    {
        //A 2D array of game pieces,
        //expected to blank at the beginning of the game.
        public GamePiece[,] Board { get; set; }

        public PieceStyle CurrentTurn = PieceStyle.X;

        public bool GameComplete => GetWinner() != null || IsADraw(); // check if the game is complete
        public GameBoard()
        {
            Reset();
        }

        //...Properties
        public void Reset()
        {
            Board = new GamePiece[3, 3];

            //Populate the Board with blank pieces
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Board[i, j] = new GamePiece();
                }
            }
        }

        //public PieceStyle CurrentTurn = PieceStyle.X; // its defined up in the first lines

        private void SwitchTurns()
        {
            //This is equivalent to: if currently X's turn,
            // make it O's turn, and vice-versa
            CurrentTurn = CurrentTurn == PieceStyle.X ? PieceStyle.O : PieceStyle.X;
        }

        //Given the coordinates of the space that was clicked...
        public void PieceClicked(int x, int y)
        {
            //If the game is complete, do nothing
            if (GameComplete) { return; }

            //If the space is not already claimed...
            GamePiece clickedSpace = Board[x, y];
            if (clickedSpace.Style == PieceStyle.Blank)
            {
                //Set the marker to the current turn marker (X or O)
                clickedSpace.Style = CurrentTurn;

                //Make it the other player's turn
                SwitchTurns();
            }
        }

        public bool IsADraw()
        {
            int pieceBlankCount = 0;

            //Count all the blank spaces. If the count is 0, this is a draw.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pieceBlankCount = this.Board[i, j].Style == PieceStyle.Blank
                    ? pieceBlankCount + 1
                    : pieceBlankCount;
                }
            }

            return pieceBlankCount == 0;
        }

        //The method to search for tic-tac-toes for a single given space looks like this:
        private WinningPlay EvaluatePieceForWinner(int i, int j,
                                               EvaluationDirection dir)
        {
            GamePiece currentPiece = Board[i, j];
            if (currentPiece.Style == PieceStyle.Blank)
            {
                return null;
            }

            int inARow = 1;
            int iNext = i;
            int jNext = j;

            var winningMoves = new List<string>();

            while (inARow < 3)
            {
                //For each direction, increment the pointers to the next space
                //to be evaluated
                switch (dir)
                {
                    case EvaluationDirection.Up:
                        jNext -= 1;
                        break;

                    case EvaluationDirection.UpRight:
                        iNext += 1;
                        jNext -= 1;
                        break;

                    case EvaluationDirection.Right:
                        iNext += 1;
                        break;

                    case EvaluationDirection.DownRight:
                        iNext += 1;
                        jNext += 1;
                        break;
                }

                //If the next "space" is off the board, don't check it.
                if (iNext < 0 || iNext >= 3 || jNext < 0 || jNext >= 3) { break; }

                //If the next space has a matching letter...
                if (Board[iNext, jNext].Style == currentPiece.Style)
                {
                    //Add this space to the collection of winning spaces.
                    winningMoves.Add($"{iNext},{jNext}");
                    inARow++;
                }
                else //Otherwise, no tic-tac-toe is found for this space/direction
                {
                    return null;
                }
            }

            //If we found three in a row
            if (inARow >= 3)
            {
                //Return this set of spaces as the winning set
                winningMoves.Add($"{i},{j}");

                return new WinningPlay()
                {
                    WinningMoves = winningMoves,
                    WinningStyle = currentPiece.Style,
                    WinningDirection = dir,
                };
            }

            //If we got here, we didn't find any tic-tac-toes for the given space.
            return null;
        }

        //However, that method only evaluates for a single space; now we need a method to do that for all spaces.
        public WinningPlay GetWinner()
        {
            WinningPlay winningPlay = null;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    foreach (EvaluationDirection evalDirection in (EvaluationDirection[])Enum.GetValues(typeof(EvaluationDirection)))
                    {
                        winningPlay = EvaluatePieceForWinner(i, j, evalDirection);
                        if (winningPlay != null) { return winningPlay; }
                    }
                }
            }

            return winningPlay;
        }


        //Finally, let's implement two more methods: one which gets a message to display the user when the game is won or drawn, and one to check for those situations so we can display the message.
        public string GetGameCompleteMessage()
        {
            var winningPlay = GetWinner();
            return winningPlay != null ? $"{winningPlay.WinningStyle} Wins!" : "Draw!";
        }

        public bool IsGamePieceAWinningPiece(int i, int j)
        {
            var winningPlay = GetWinner();
            return winningPlay?.WinningMoves?.Contains($"{i},{j}") ?? false;
        }
    }
}
