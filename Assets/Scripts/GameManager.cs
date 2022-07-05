using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")] public GameObject playerSpawn;
    public GameObject playerObject;

    private static GameManager _instance;
    private readonly PlayerController _thePlayer;
    private Vector2 _playerStart;

    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    // public ScriptableInteger life;
    // public ScriptableInteger coin;

    public List<GameObject> items;

    public bool isPlay = false;

    public readonly UnityAction OnGameOverAction;
    public GameManager(PlayerController thePlayer, Vector2 playerStart, UnityAction onGameOverAction)
    {
        this._thePlayer = thePlayer;
        this._playerStart = playerStart;
        OnGameOverAction = onGameOverAction;
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameOverScreen);
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        items = new List<GameObject>();
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        _thePlayer.SetActive(false);
    }

    public void StartGame()
    {
        isPlay = true;
        SpawnPlayer();
    }

    public void PauseGame()
    {
        isPlay = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPlay = true;
        Time.timeScale = 1;
    }

    private void SpawnPlayer()
    {
        playerObject.transform.position = playerSpawn.transform.position;
    }

    public void GameOver()
    {
        isPlay = false;
        OnGameOverAction?.Invoke();
    }

    public void Reset()
    {
        // life.Reset();
        // coin.Reset();

    }

    public void AddItem(GameObject go)
    {
        items.Add(go);
    }

    public void ClearAllItems()
    {
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        items.Clear();
    }
}