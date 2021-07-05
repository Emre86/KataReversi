using System;
using System.Collections.Generic;

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
        private List<(int,int)> _gameOrientation;
        private List<(int,int)> _selectedGameOrientation;
        
        public Reversi()
        {
            this.InitEnvironment();
            this.InitGridReversi();
        }

        public Reversi(string[,] gridReversi, string activePlayer)
        {
            this.InitEnvironment();
            this._gridReversi = gridReversi;
            this._activePlayer = activePlayer;
        }
        
        private void InitEnvironment()
        {
            this._gameOrientation = new List<(int, int)>()
            {
                ( 0,  1), // right
                ( 0, -1), // left
                (-1,  0), // top
                ( 1,  0), // bottom
                (-1, -1), // topLeft
                (-1,  1), // topRight
                ( 1, -1), // bottomLeft
                ( 1,  1)  // bottomRight
            };

            this._selectedGameOrientation = new List<(int, int)>();
            
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
            this._gridReversi = new string [this._sideLength, this._sideLength];
            
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
            Console.WriteLine($"Joueur actif : {this._activePlayer}");
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
            List<string> subGridReversi = new List<string>();
            while (nextPositionLine < this._sideLength && nextPositionLine > -1 && nextPositionColumn < this._sideLength && nextPositionColumn > -1)
            {
                subGridReversi.Add(this._gridReversi[nextPositionLine, nextPositionColumn]);
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
            return subGridReversi;
        }

       
        private bool CheckValid(List<string> subGridReversi)
        {
            bool hasTokenActivePlayer = false;
            bool hasTokenPassivePlayer = false;
            foreach (string boxValue in subGridReversi)
            {
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
        
        public bool CheckValidStroke(string character, int digit)
        {
            int positionLine = this._charactersPosition[character];
            int positionColumn = digit - 1;
            bool isValid = false;
            bool isValidCurrentPosition = this._gridReversi[positionLine, positionColumn].Equals(emptyCell);
          
            if (isValidCurrentPosition)
            {
                this._selectedGameOrientation.Clear();
                foreach((int, int)orientation in this._gameOrientation)
                {
                    List<string> subGridReversi =  GetSubGridReversi(positionLine, positionColumn,  orientation.Item1, orientation.Item2);
                    if (CheckValid(subGridReversi))
                    {
                        this._selectedGameOrientation.Add(orientation);
                    }
                }
                isValid = this._selectedGameOrientation.Count > 0;
            }
            return isValid;
        }

        private void PlayGrid(int positionLine, int positionColumn, int orientationLine, int orientationColumn)
        {
            int nextPositionLine = positionLine + orientationLine;
            int nextPositionColumn = positionColumn + orientationColumn;
            while (nextPositionLine < this._sideLength && nextPositionLine > -1 && nextPositionColumn < this._sideLength && nextPositionColumn > -1)
            {
                if (this._gridReversi[nextPositionLine, nextPositionColumn].Equals(emptyCell) || this._gridReversi[nextPositionLine, nextPositionColumn].Equals(this._activePlayer))
                {
                    break;
                }
                this._gridReversi[nextPositionLine, nextPositionColumn] = this._activePlayer;
                nextPositionLine = nextPositionLine + orientationLine;
                nextPositionColumn = nextPositionColumn + orientationColumn;
            }
        }

        public void PlayStroke(string character, int digit)
        {
            int positionLine = this._charactersPosition[character];
            int positionColumn = digit - 1;
            this._gridReversi[positionLine, positionColumn] = this._activePlayer;
            foreach ((int,int) orientation in this._selectedGameOrientation)
            {
                PlayGrid(positionLine, positionColumn, orientation.Item1, orientation.Item2);
            }
            this._activePlayer = this._activePlayer.Equals("B ") ? "W " : "B ";
        }
        
    }
}