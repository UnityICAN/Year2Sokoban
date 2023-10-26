using System.Collections.Generic;
using System.Linq;
using UndoSystem;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public static BoardManager instance;
    
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite boxSprite;
    [SerializeField] private Sprite endPositionSprite;

    [SerializeField] private Level levelToLoad;

    public TileType[,] TilesList { get; private set; }
    public GameObject[,] BoardTilesList { get; private set; }
    public List<Vector2Int> BoxesList { get; private set; }
    private List<Vector2Int> endPositionsList;

    private Stack<PlayerAction> playerActions;

    private void Start() {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

        playerActions = new Stack<PlayerAction>();
        
        // Lire l'info du niveau
        string levelString = levelToLoad.Content;

        string[] levelLines = levelString.Split('\n');

        // Créer le niveau
        TilesList = new TileType[10, 10];
        BoxesList = new List<Vector2Int>();
        endPositionsList = new List<Vector2Int>();
        Vector2Int startPosition = new Vector2Int(0, 0);
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (levelLines[y][x] == 'W')
                    TilesList[x, y] = TileType.Wall;
                else {
                    TilesList[x, y] = TileType.Floor;
                    if (levelLines[y][x] == 'B')
                        BoxesList.Add(new Vector2Int(x, y));
                    else if (levelLines[y][x] == 'S')
                        startPosition = new Vector2Int(x, y);
                    else if (levelLines[y][x] == 'E')
                        endPositionsList.Add(new Vector2Int(x, y));
                }
            }
        }

        playerController.Init(startPosition);

        // Initialiser le plateau
        BoardTilesList = new GameObject[10, 10];
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                BoardTilesList[x, y] = Instantiate(tilePrefab,
                    new Vector3(x, -y, 0f),
                    Quaternion.identity,
                    boardTransform);
            }
        }
        
        UpdateVisuals();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && playerActions.Count > 0) {
            playerActions.Pop().Undo();
        }
    }

    private void UpdateVisuals() {
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (TilesList[x, y] == TileType.Wall)
                    BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = wallSprite;
                else if (TilesList[x, y] == TileType.Floor) {
                    if (BoxesList.Contains(new Vector2Int(x, y)))
                        BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = boxSprite;
                    else if (endPositionsList.Contains(new Vector2Int(x, y)))
                        BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = endPositionSprite;
                    else
                        BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = floorSprite;
                }
            }
        }
    }

    public void HandleMove(PlayerAction playerAction) {
        playerActions.Push(playerAction);
        UpdateVisuals();
        CheckVictory();
    }

    private void CheckVictory() {
        foreach (Vector2Int endPosition in endPositionsList) {
            if (!BoxesList.Contains(endPosition))
                return;
        }
        
        Debug.Log("Victory!");
    }

    public void UpdateEntitiesPositions(Vector2Int playerPosition, IReadOnlyList<Vector2Int> boxesPositions) {
        playerController.ForceChangePosition(playerPosition);
        BoxesList = boxesPositions.ToList();
        UpdateVisuals();
    }
}
