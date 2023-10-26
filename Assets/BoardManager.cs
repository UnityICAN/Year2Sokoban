using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite boxSprite;

    [SerializeField] private Level levelToLoad;

    public TileType[,] TilesList { get; private set; }
    public GameObject[,] BoardTilesList { get; private set; }
    public List<Vector2Int> BoxesList { get; private set; }

    private void Start() {
        // Lire l'info du niveau
        string levelString = levelToLoad.Content;

        string[] levelLines = levelString.Split('\n');

        // Créer le niveau
        TilesList = new TileType[10, 10];
        BoxesList = new List<Vector2Int>();
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (levelLines[y][x] == 'W')
                    TilesList[x, y] = TileType.Wall;
                else {
                    TilesList[x, y] = TileType.Floor;
                    if (levelLines[y][x] == 'B')
                        BoxesList.Add(new Vector2Int(x, y));
                }
            }
        }

        playerController.Init(new Vector2Int(4, 4), this);

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

    public void UpdateVisuals() {
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                
                if (TilesList[x, y] == TileType.Wall)
                    BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = wallSprite;
                else if (TilesList[x, y] == TileType.Floor) {
                    if (BoxesList.Contains(new Vector2Int(x, y)))
                        BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = boxSprite;
                    else
                        BoardTilesList[x, y].GetComponent<SpriteRenderer>().sprite = floorSprite;
                }
            }
        }
    }
}
