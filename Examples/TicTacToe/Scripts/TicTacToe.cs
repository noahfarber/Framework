using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;
using TMPro;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private Button[] _TicTacToeTiles;
    [SerializeField] private TextMeshProUGUI _GameMessageText;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TicTacToeSounds GameSoundData;

    private TextMeshProUGUI[] _TicTacToeTileTexts;
    private int[] _GameBoard = new int[9];

    public int EmptyTileID = -1;
    public int PlayerTileID = 1;
    public int ComputerTileID = 2;

    private int[][] _WinningCombinations;
    private int[] _WinningTiles = new int[3] { -1, -1, -1 };


    private int _PlayerScore = 0;
    private int _ComputerScore = 0;

    private const string _PlayerTurnMessage = "PLAYER TURN";
    private const string _PlayerWinMessage = "YOU WIN!!!";
    private const string _ComputerTurnMessage = "COMPUTER TURN";
    private const string _ComputerWinMessage = "COMPUTER WON :(";
    private const string _StalemateMessage = "STALEMATE";

    #region Public Functions 
    public void Build()
    {
    }

    public void Init()
    {
        // Reset win tiles
        _WinningTiles = new int[3] { -1, -1, -1 };
        InitWinningCombinations();

        // Reset game board data
        for (int i = 0; i < _GameBoard.Length; i++)
        {
            _GameBoard[i] = EmptyTileID;
        }

        // Reset gamme board texts
        for (int i = 0; i < _TicTacToeTileTexts.Length; i++)
        {
            _TicTacToeTileTexts[i].text = "";
            _TicTacToeTileTexts[i].color = Color.black;
        }

        // Init player input buttons and save text references
        _TicTacToeTileTexts = new TextMeshProUGUI[_TicTacToeTiles.Length];
        for (int i = 0; i < _TicTacToeTiles.Length; i++)
        {
            int x = i;

            // Add tile behaviours if they don't exist
            if(_TicTacToeTiles[i].onClick.GetPersistentEventCount() == 0)
            {
                _TicTacToeTiles[i].onClick.AddListener(delegate { TilePressed(x); });
            }

            // Get references to tile texts
            if(_TicTacToeTileTexts[i] == null)
            {
                _TicTacToeTileTexts[i] = _TicTacToeTiles[i].GetComponentInChildren<TextMeshProUGUI>();
            }
        }


        _GameMessageText.text = _PlayerTurnMessage;
        SoundManager.Instance.PlayOneShot(GameSoundData.StartGame, 1f);
    }
    #endregion

    #region Private Functions
    private void InitWinningCombinations()
    {
        if(_WinningCombinations == null)
        {
            _WinningCombinations = new int[8][];
            _WinningCombinations[0] = new int[3] { 0, 1, 2 };
            _WinningCombinations[1] = new int[3] { 3, 4, 5 };
            _WinningCombinations[2] = new int[3] { 6, 7, 8 };
            _WinningCombinations[3] = new int[3] { 0, 3, 6 };
            _WinningCombinations[4] = new int[3] { 1, 4, 7 };
            _WinningCombinations[5] = new int[3] { 2, 5, 8 };
            _WinningCombinations[6] = new int[3] { 0, 4, 8 };
            _WinningCombinations[7] = new int[3] { 2, 4, 6 };
        }
    }

    private void TilePressed(int selection)
    {
        if (State == GameState.PlayerTurn)
        {
            // If valid input
            if (_GameBoard[selection] == EmptyTileID)
            {
                UpdateBoard(selection, PlayerTileID);

                SoundManager.Instance.PlayOneShot(GameSoundData.ClickedTile, 1f);

                if (EvaluateWin(PlayerTileID))
                {
                    _PlayerScore++;
                    OnGameOver(_PlayerWinMessage);
                }
                else if (Stalemate())
                {
                    OnGameOver(_StalemateMessage);
                }
                else
                {
                    State = GameState.ComputerTurn;
                    _GameMessageText.text = _ComputerTurnMessage;
                }
            }
        }
    }

    private void OnGameOver(string gameOverMessage)
    {
        // Set winning tiles green
        for (int i = 0; i < _WinningTiles.Length; i++)
        {
            if (_WinningTiles[i] != -1)
            {
                _TicTacToeTileTexts[_WinningTiles[i]].color = Color.green;
            }
        }

        PlayGameOverSound(gameOverMessage);

        _GameMessageText.text = gameOverMessage;
        _ScoreText.text = "Player: " + _PlayerScore + "    Computer: " + _ComputerScore;
        _RestartButton.gameObject.SetActive(true);
        State = GameState.GameOver;
    }

    private void PlayGameOverSound(string gameOverMessage)
    {
        if(gameOverMessage == _PlayerWinMessage)
        {
            SoundManager.Instance.PlayOneShot(GameSoundData.WinGame, 1f);
        }
        else if(gameOverMessage == _ComputerWinMessage)
        {
            SoundManager.Instance.PlayOneShot(GameSoundData.LoseGame, 1f);
        }
        else if(gameOverMessage == _StalemateMessage)
        {
            SoundManager.Instance.PlayOneShot(GameSoundData.Stalemate, 1f);
        }
    }

    public void UpdateBoard(int selection, int playerID)
    {
        _GameBoard[selection] = playerID;
        _TicTacToeTileTexts[selection].text = playerID == PlayerTileID ? "X" : "O";
    }
    #endregion

    #region Helper Functions

    private bool Stalemate()
    {
        for (int i = 0; i < _GameBoard.Length; i++)
        {
            if (_GameBoard[i] == EmptyTileID)
            {
                return false;
            }
        }

        return true;
    }

    public int ComputerStrategy()
    {
        int rtn = -1;

        int winningTilePosition= -1;
        int defensiveTilePosition = -1;

        for (int c = 0; c < _WinningCombinations.Length; c++)
        {
            int numPlayerTiles = 0;
            int numComputerTiles = 0;
            int emptyTilePosition = -1;

            // Check each winning combination and evaluate
            for (int i = 0; i < _WinningCombinations[c].Length; i++)
            {
                if (_GameBoard[_WinningCombinations[c][i]] == PlayerTileID)
                {
                    numPlayerTiles++;
                }
                else if (_GameBoard[_WinningCombinations[c][i]] == ComputerTileID)
                {
                    numComputerTiles++;
                }
                else if(_GameBoard[_WinningCombinations[c][i]] == EmptyTileID)
                {
                    emptyTilePosition = _WinningCombinations[c][i];
                }
            }

            if (numComputerTiles == 2 && numPlayerTiles == 0)
            {
                winningTilePosition = emptyTilePosition;
            }

            if (numPlayerTiles == 2 && numComputerTiles == 0)
            {
                defensiveTilePosition = emptyTilePosition;
            }
        }

        // If a win exists, sieze it!
        if (winningTilePosition != -1)
        {
            rtn = winningTilePosition;
        }
        // If player will win, block it!
        else if (defensiveTilePosition != -1)
        {
            rtn = defensiveTilePosition;
        }
        // If middle tile isn't taken, take it!
        else if (_GameBoard[4] == EmptyTileID)
        {
            rtn = 4;
        }
        // No strategy found
        else
        {
            // Random if no strategy found
            if (rtn == -1)
            {
                rtn = Random.Range(0, _TicTacToeTiles.Length);

                while (_GameBoard[rtn] != EmptyTileID)
                {
                    rtn = Random.Range(0, _TicTacToeTiles.Length);
                }
            }
        }
        
        return rtn;
    }

    public bool EvaluateWin(int playerID)
    {
        for (int c = 0; c < _WinningCombinations.Length; c++)
        {
            int numTiles = 0;

            for (int i = 0; i < _WinningCombinations[c].Length; i++)
            {
                if (_GameBoard[_WinningCombinations[c][i]] == playerID)
                {
                    numTiles++;
                }
            }

            if (numTiles == 3)
            {
                _WinningTiles = new int[3] { _WinningCombinations[c][0], _WinningCombinations[c][1], _WinningCombinations[c][2] };
                return true;
            }
        }

        return false;
    }
    #endregion
}


[System.Serializable]
public class TicTacToeSounds
{
    public AudioClip StartGame;
    public AudioClip ClickedTile;
    public AudioClip PlayerTurn;
    public AudioClip ComputerTurn;
    public AudioClip WinGame;
    public AudioClip LoseGame;
    public AudioClip Stalemate;
}
