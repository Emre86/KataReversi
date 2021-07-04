using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;

namespace Reversi
{
    public class Reversi
    {
        private readonly string emptyCell = ". ";
        private readonly int _sideLength = 8;
        private string _activePlayer = "B ";
        private string[,] _gridReversi;
        private Dictionary<string, int> _charactersPosition;
        private Dictionary<int, string> _positionCharacters;
        private List<List<string>> _gameOrientation;
        
        public Reversi()
        {
          
        }

        public void Init()
        {
            this.InitCharactersPosition();
            this.InitGridReversi();
            this._gridReversi = new string [this._sideLength, this._sideLength];
            this._gameOrientation = new List<List<string>>();
        }
        
        private void InitCharactersPosition()
        {
            this._charactersPosition = new Dictionary<string, int>(){
                {"A", 0},
                {"B", 1},
                {"C", 2},
                {"D", 3},
                {"E", 4},
                {"F", 5},
                {"G", 6},
                {"H", 7}
            };
            
            this._positionCharacters = new Dictionary<int, string>(){
                {0, "A "},
                {1, "B "},
                {2, "C "},
                {3, "D "},
                {4, "E "},
                {5, "F "},
                {6, "G "},
                {7, "H "}
            };
        }

        private void InitGridReversi()
        {
            for (int ii = 0; ii < this._sideLength; ii++)
            {
                for (int jj = 0; jj < this._sideLength; jj++)
                {
                    this._gridReversi[ii, jj] = emptyCell;
                }
            }

            this._gridReversi[this._charactersPosition["D"], 3] = "W ";
            this._gridReversi[this._charactersPosition["E"], 4] = "W ";
            this._gridReversi[this._charactersPosition["D"], 4] = this._activePlayer;
            this._gridReversi[this._charactersPosition["E"], 3] = this._activePlayer;
        }

        public void DisplayGridReversi()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            for (int ii = 0; ii < this._sideLength; ii++)
            {
                Console.Write(this._positionCharacters[ii]);
                for (int jj = 0; jj < this._sideLength; jj++)
                {
                    Console.Write(this._gridReversi[ii,jj]);
                }
                Console.WriteLine("");
            }
        }

       

       

        private List<string> GetSubGridReversi(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        {
            int nextPositionLine = positionLine + orientationLine;
            int nextPositionColumn = positionColumn + orientationColumn;
            //string[] subArray = new string[] {};
            List<string> subGridReversi = new List<string>();
            //int ii = 0;
            while (nextPositionLine < this._sideLength && nextPositionLine > 0 && nextPositionColumn < this._sideLength && nextPositionColumn > 0)
            {
                //subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
                subGridReversi.Add(this._gridReversi[nextPositionLine, nextPositionColumn]);
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
            return subGridReversi;
        }

        private void PlayGrid(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        {
            int nextPositionLine = positionLine + orientationLine;
            int nextPositionColumn = positionColumn + orientationColumn;
            while (nextPositionLine < this._sideLength && nextPositionLine > 0 && nextPositionColumn < this._sideLength && nextPositionColumn > 0)
            {
                if (this._gridReversi[nextPositionLine, nextPositionColumn].Equals(emptyCell))
                {
                    break;
                }
                this._gridReversi[nextPositionLine, nextPositionColumn] = this._activePlayer;
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
        }
        
        private bool CheckValid(List<string> subGridReversi)
        {
            bool hasTokenActivePlayer = false;
            bool hasTokenPassivePlayer = false;
            //  for (int jj = ordonne; jj < 8; jj++)
            foreach (string boxValue in subGridReversi)
            {
                //string boxValue = 
                if (boxValue.Equals(emptyCell))
                {
                    break;
                }
                if (boxValue.Equals(this._activePlayer))
                {
                    hasTokenActivePlayer = true;
                    break;
                }
                hasTokenPassivePlayer = true;
            }
            return hasTokenActivePlayer && hasTokenPassivePlayer;
        }
        
        public bool CheckValidStroke(string character, int chiffre)
        {
            int positionLine = this._charactersPosition[character];
            int positionColumn = chiffre - 1;
            bool isValid = false;
            bool isValidCurrentPosition = this._gridReversi[positionLine, positionColumn].Equals(emptyCell);
          
            if (isValidCurrentPosition)
            {
                List<string> lineRight = GetSubGridReversi(positionLine, positionColumn,  0, 1);
                List<string> lineLeft = GetSubGridReversi(positionLine, positionColumn, 0, -1);
                List<string> columnTop = GetSubGridReversi(positionLine, positionColumn, -1,0);
                List<string> columnBottom = GetSubGridReversi(positionLine, positionColumn, 1, 0) ;
                List<string> diagonalTopLeft = GetSubGridReversi(positionLine, positionColumn, -1, -1);
                List<string> diagonalTopRight = GetSubGridReversi(positionLine, positionColumn, -1, 1);
                List<string> diagonalBottomLeft = GetSubGridReversi(positionLine, positionColumn, 1, -1);
                List<string> diagonalBottomRight =  GetSubGridReversi(positionLine, positionColumn, 1, 1);
                
                List<List<string>> gameOrientation = new List<List<string>> { lineRight, lineLeft, columnTop, columnBottom, diagonalTopLeft, diagonalTopRight, diagonalBottomLeft, diagonalBottomRight };
                foreach (List<string> orientation in gameOrientation)
                {
                    if (CheckValid(orientation))
                    {
                        isValid = true;
                        this._gameOrientation.Add(orientation);
                    }
                }
            }
            return isValid;
        }


        public void PlayStroke(string character, int chiffre)
        {
            // int abscisse = this._charactersPosition[character];
            // int ordonne = chiffre - 1;
            // this._gridReversi[abscisse, ordonne] = this._activePlayer;
            //
            // this._activePlayer = this._activePlayer.Equals("B ") ? "W " : "B ";
        }
        
        // private List<string> GetLine(int startColumn, int endColumn, int positionLine)
        // {
        //     //string[] lineArray = new string[] {};
        //     List<string> subGridReversi = new List<string>();
        //     for (int ii = startColumn; ii < endColumn; ii++)
        //     {
        //         subGridReversi.Add(this.GridReversi[positionLine, ii]);
        //     }
        //     return subGridReversi;
        // }
        //
        // private List<string> GetColumn(int startLine, int endLine, int positionColumn)
        // {
        //     //string[] columnArray = new string[] {};
        //     List<string> subGridReversi = new List<string>();
        //     for (int ii = startLine; ii < endLine; ii++)
        //     {
        //         subGridReversi.Add(this.GridReversi[ii, positionColumn]);
        //     }
        //     return subGridReversi;
        // }
        
        // private List<string> GetDiagonal(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        // {
        //     int nextPositionLine = positionLine + orientationLine;
        //     int nextPositionColumn = positionColumn + orientationColumn;
        //     //string[] subArray = new string[] {};
        //     List<string> subGridReversi = new List<string>();
        //     //int ii = 0;
        //     while (nextPositionLine < this._sideLength && nextPositionLine > 0 && nextPositionColumn < this._sideLength && nextPositionColumn > 0)
        //     {
        //         //subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
        //         subGridReversi.Add(this.GridReversi[nextPositionLine, nextPositionColumn]);
        //         nextPositionLine = nextPositionLine + orientationLine;
        //         nextPositionColumn = nextPositionColumn + orientationColumn;
        //     }
        //     return subGridReversi;
        // }
        
        // private string[] GetDiagonalBasDroite(int positionLine, int positionColumn)
        // {
        //     int nextPositionLine = positionLine +1 ;
        //     int nextPositionColumn = positionColumn + 1;
        //     string[] subArray = new string[] {};
        //     int ii = 0;
        //     while (nextPositionLine < 8 || nextPositionColumn < 8)
        //     {
        //          subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
        //          ii++;
        //          nextPositionLine++;
        //          nextPositionColumn++;
        //     }
        //     return subArray;
        // }

        // private string[] GetDiagonalHautDroite(int positionLine, int positionColumn)
        // {
        //     int nextPositionLine = positionLine - 1;
        //     int nextPositionColumn = positionColumn + 1;
        //     string[] subArray = new string[] {};
        //     int ii = 0;
        //     while ( 0 < nextPositionLine || nextPositionColumn < 8)
        //     {
        //         subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
        //         ii++;
        //         nextPositionLine--;
        //         nextPositionColumn++;
        //     }
        //     return subArray;
        // }
        //
        // private string[] GetDiagonalBasGauche(int positionLine, int positionColumn)
        // {
        //     int nextPositionLine = positionLine + 1;
        //     int nextPositionColumn = positionColumn - 1;
        //     string[] subArray = new string[] {};
        //     int ii = 0;
        //     while (nextPositionLine < 8 || nextPositionColumn > 0)
        //     {
        //         subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
        //         ii++;
        //         nextPositionLine++;
        //         nextPositionColumn--;
        //     }
        //     return subArray;
        // }
        
        // private string[] GetDiagonalHautGauche(int positionLine, int positionColumn)
        // {
        //     int nextPositionLine = positionLine - 1;
        //     int nextPositionColumn = positionColumn + 1;
        //     string[] subArray = new string[] {};
        //     int ii = 0;
        //     while (nextPositionLine > 0 || nextPositionColumn > 0)
        //     {
        //         subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
        //         ii++;
        //         nextPositionLine--;
        //         nextPositionColumn--;
        //     }
        //     return subArray;
        // }
        
        // private bool CheckLineOrdered(int abscisse, int ordonne)
        // {
        //     bool hasTokenActivePlayer = false;
        //     bool hasTokenPassivePlayer = false;
        //     for (int jj = ordonne; jj < 8; jj++)
        //     {
        //         string boxValue = this.GridReversi[abscisse, jj];
        //         if (boxValue.Equals(emptyCell))
        //         {
        //             break;
        //         }
        //         if (boxValue.Equals(this._activePlayer))
        //         {
        //             hasTokenActivePlayer = true;
        //             break;
        //         }
        //         hasTokenPassivePlayer = true;
        //     }
        //     return hasTokenActivePlayer && hasTokenPassivePlayer;
        // }

        // private bool CheckLineReversed(int abscisse, int ordonne)
        // {
        //     bool hasTokenActivePlayer = false;
        //     bool hasTokenPassivePlayer = false;
        //     for (int jj = ordonne; jj > -1; jj--)
        //     {
        //         string boxValue = this.GridReversi[abscisse, jj];
        //         if (boxValue.Equals(emptyCell))
        //         {
        //             break;
        //         }
        //         if (boxValue.Equals(this._activePlayer))
        //         {
        //             hasTokenActivePlayer = true;
        //             break;
        //         }
        //         hasTokenPassivePlayer = true;
        //     }
        //     return hasTokenActivePlayer && hasTokenPassivePlayer;
        // }

        // private void PlayLineOrdered(int abscisse, int ordonne)
        // {
        //     for (int jj = ordonne; jj < 8; jj++)
        //     {
        //         string boxValue = this.GridReversi[abscisse, jj];
        //         if (boxValue.Equals(emptyCell))
        //         {
        //             break;
        //         }
        //         if (boxValue.Equals(this._activePlayer))
        //         {
        //             break;
        //         }
        //
        //         this.GridReversi[abscisse, jj] = this._activePlayer;
        //     }
        // }

        // private void PlayLineReversed(int abscisse, int ordonne)
        // {
        //     for (int jj = ordonne; jj > -1; jj--)
        //     {
        //         string boxValue = this.GridReversi[abscisse, jj];
        //         if (boxValue.Equals(emptyCell))
        //         {
        //             break;
        //         }
        //         if (boxValue.Equals(this._activePlayer))
        //         {
        //             break;
        //         }
        //
        //         this.GridReversi[abscisse, jj] = this._activePlayer;
        //     }
        // }
        
    }
}