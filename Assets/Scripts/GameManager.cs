using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // delegate methods
    public delegate void OnPlayerDie();    
    internal OnPlayerDie onPlayerDieCallback;

    public delegate void OnSceneChange(bool is3D);
    internal OnSceneChange OnSceneChangeCallback;


    private const string _playerSaveCoordinate = "PlayerLastCoordinate";


    public bool gameIsOver;
    public bool gameIsPaused;

    public List<GameObject> gameObjects; // Including enemy, player, and environment
    public GameObject gameOverPanel; // TODO
    [SerializeField] private Sprite healthImage;

    internal bool is3D;

    public List<Image> livesUI;

    public GameObject player;
    public int playerLives = 3;
    public float widthHigherPoint = 9f;
    public float widthLowerPoint = 2f;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        CreatePlayerSaveSpot();

        RefreshHeartUi();
        OnSceneChangeCallback += RearrangeObjectsBasedOnScene;
        OnSceneChangeCallback?.Invoke(false);

        onPlayerDieCallback += Die;
        onPlayerDieCallback += RefreshHeartUi;
    }

    public void CreatePlayerSaveSpot()
    {
        var playerPosition = player.transform.position;

        PlayerPrefs.SetString(_playerSaveCoordinate, $"{playerPosition.x},{playerPosition.y},{playerPosition.z}");
    }

    private void RearrangeObjectsBasedOnScene(bool changeTo3D)
    {
        if (!changeTo3D)
        {
            is3D = false;
            foreach (var obj in gameObjects)
                if (obj.layer == LayerMask.NameToLayer("Enemy"))
                {
                    obj.GetComponent<Rigidbody>().constraints =
                        RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                else if (obj.layer == LayerMask.NameToLayer("Player"))
                {
                    obj.GetComponent<Rigidbody>().constraints =
                        RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                else if (obj.layer == LayerMask.NameToLayer("Environment"))
                {
                }
        }
        else
        {
            is3D = true;
            foreach (var o in gameObjects)
            {
                o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y,
                    Random.Range(widthHigherPoint, widthLowerPoint));
                o.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        
    }

    private void Die()
    {
        playerLives -= 1;
        if (playerLives <= 0)
            Gameover();
        else
            RefreshHeartUi();
    }

    private void Gameover()
    {
        gameOverPanel.SetActive(true); // Turn on the game over menu
        PlayerPrefs.DeleteAll(); // Delete all player data when game is turned off
    }

    private void RefreshHeartUi()
    {
        for (var i = 0; i < playerLives; i++) livesUI[i].sprite = healthImage;

        for (var i = playerLives; i < livesUI.Count; i++) livesUI[i].sprite = null;
    }
}