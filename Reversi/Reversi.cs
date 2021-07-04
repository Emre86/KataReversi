using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Reversi
{
    public class Reversi
    {
        private readonly string emptyCell = ". ";
        private string _activePlayer = "B ";

        private int _sideLength = 8;
        //private string passivePlayer = "W ";
        private string [,]GridReversi = new string [8, 8];
        private Dictionary<string, int> _charactersPosition;
        private Dictionary<int, string> _positionCharacters;
        
        public Reversi()
        {
            this.InitCharactersPosition();
            this.InitGridReversi();
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
                    this.GridReversi[ii, jj] = emptyCell;
                }
            }

            this.GridReversi[this._charactersPosition["D"], 3] = "W ";
            this.GridReversi[this._charactersPosition["E"], 4] = "W ";
            this.GridReversi[this._charactersPosition["D"], 4] = this._activePlayer;
            this.GridReversi[this._charactersPosition["E"], 3] = this._activePlayer;
        }

        public void DisplayGridReversi()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            for (int ii = 0; ii < this._sideLength; ii++)
            {
                Console.Write(this._positionCharacters[ii]);
                for (int jj = 0; jj < this._sideLength; jj++)
                {
                    Console.Write(this.GridReversi[ii,jj]);
                }
                Console.WriteLine("");
            }
        }

        private List<string> GetLine(int startColumn, int endColumn, int positionLine)
        {
            //string[] lineArray = new string[] {};
            List<string> subGridReversi = new List<string>();
            for (int ii = startColumn; ii < endColumn; ii++)
            {
                subGridReversi.Add(this.GridReversi[positionLine, ii]);
            }
            return subGridReversi;
        }

        private List<string> GetColumn(int startLine, int endLine, int positionColumn)
        {
            //string[] columnArray = new string[] {};
            List<string> subGridReversi = new List<string>();
            for (int ii = startLine; ii < endLine; ii++)
            {
                subGridReversi.Add(this.GridReversi[ii, positionColumn]);
            }
            return subGridReversi;
        }

        private List<string> GetDiagonal(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        {
            int nextPositionLine = positionLine + orientationLine;
            int nextPositionColumn = positionColumn + orientationColumn;
            //string[] subArray = new string[] {};
            List<string> subGridReversi = new List<string>();
            //int ii = 0;
            while (nextPositionLine < this._sideLength && nextPositionLine > 0 && nextPositionColumn < this._sideLength && nextPositionColumn > 0)
            {
                //subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
                subGridReversi.Add(this.GridReversi[nextPositionLine, nextPositionColumn]);
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
            return subGridReversi;
        }

        private List<string> GetGrid(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        {
            int nextPositionLine = positionLine + orientationLine;
            int nextPositionColumn = positionColumn + orientationColumn;
            //string[] subArray = new string[] {};
            List<string> subGridReversi = new List<string>();
            //int ii = 0;
            while (nextPositionLine < this._sideLength && nextPositionLine > 0 && nextPositionColumn < this._sideLength && nextPositionColumn > 0)
            {
                //subArray[ii] = this.GridReversi[nextPositionLine, nextPositionColumn];
                subGridReversi.Add(this.GridReversi[nextPositionLine, nextPositionColumn]);
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
            return subGridReversi;
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
            
            Console.WriteLine(character);
            Console.WriteLine(chiffre);
            int positionLine = this._charactersPosition[character];
            int positionColumn = chiffre - 1;
            bool isValid = false;
            bool isValidCurrentPosition = this.GridReversi[positionLine, positionColumn].Equals(emptyCell);
            // bool checkValidLineOrdered = CheckLineOrdered(abscisse, ordonne + 1);
            // bool checkValidLineReversed = CheckLineReversed(abscisse, ordonne - 1);
            // bool checkValid = checkValidLineOrdered || checkValidLineReversed;
            if (isValidCurrentPosition)
            {
                List<string> lineRight = GetLine(positionColumn + 1, this._sideLength, positionLine);
                List<string> lineLeft = GetLine(0, positionColumn - 1, positionLine);
                List<string> columnTop = GetColumn(0, positionLine - 1, positionColumn);
                List<string> columnBottom = GetColumn(positionLine + 1, this._sideLength, positionColumn);
                List<string> topLeft = GetDiagonal(positionLine, positionColumn, -1, -1);
                List<string> topRight =  GetDiagonal(positionLine, positionColumn, -1, 1);
                List<string> bottomLeft = GetDiagonal(positionLine, positionColumn, 1, -1);
                List<string> bottomRight =  GetDiagonal(positionLine, positionColumn, 1, 1);
                lineLeft.Reverse();
                columnTop.Reverse();
                
                List<string> lineRight2 = GetLine(positionColumn + 1, this._sideLength, positionLine);
                List<string> lineLeft2 = GetLine(0, positionColumn - 1, positionLine);
                List<string> columnTop2 = GetColumn(0, positionLine - 1, positionColumn);
                List<string> columnBottom2 = GetColumn(positionLine + 1, this._sideLength, positionColumn);
                List<string> topLeft2 = GetDiagonal(positionLine, positionColumn, -1, -1);
                List<string> topRight2 =  GetDiagonal(positionLine, positionColumn, -1, 1);
                List<string> bottomLeft2 = GetDiagonal(positionLine, positionColumn, 1, -1);
                List<string> bottomRight2 =  GetDiagonal(positionLine, positionColumn, 1, 1);
                

                List<List<string>> gameOrientation = new List<List<string>> { lineRight, lineLeft, columnTop, columnBottom, topLeft, topRight, bottomLeft, bottomRight };
                foreach (List<string> orientation in gameOrientation)
                {
                    if (CheckValid(orientation))
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }


        public void PlayStroke(string character, int chiffre)
        {
            int abscisse = this._charactersPosition[character];
            int ordonne = chiffre - 1;
            this.GridReversi[abscisse, ordonne] = this._activePlayer;
            // PlayLineOrdered(abscisse, ordonne + 1);
            // PlayLineReversed(abscisse, ordonne - 1);
            this._activePlayer = this._activePlayer.Equals("B ") ? "W " : "B ";
        }
        
        
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