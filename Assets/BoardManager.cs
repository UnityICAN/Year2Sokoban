using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite crateSprite;

    public TileType[,] TilesList { get; private set; }
    private GameObject[,] tileGameObjects;
    public List<Vector2Int> CratesPositions { get; private set; }

    private void Start() {
        playerController.Init(this);
        
        // Create hard-coded level (to be replaced with level-loading logic)
        TilesList = new TileType[10, 10];
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (x == 0 || y == 0 || x == 9 || y == 9)
                    TilesList[x, y] = TileType.Wall;
                else
                    TilesList[x, y] = TileType.Floor;
            }
        }

        TilesList[3, 2] = TileType.Wall;
        CratesPositions = new List<Vector2Int>();
        CratesPositions.Add(new Vector2Int(6, 6));

        // Display level
        tileGameObjects = new GameObject[10, 10];
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                tileGameObjects[x, y] = Instantiate(tilePrefab,
                    new Vector3(x, -y, 0f),
                    Quaternion.identity,
                    boardTransform);
                UpdateTileDisplay(new Vector2Int(x, y), tileGameObjects[x, y]);
            }
        }
    }

    private void UpdateTileDisplay(Vector2Int position, GameObject tile) {
        if (TilesList[position.x, position.y] == TileType.Wall)
            tile.GetComponent<SpriteRenderer>().sprite = wallSprite;
        else if (TilesList[position.x, position.y] == TileType.Floor) {
            if (CratesPositions.Contains(position))
                tile.GetComponent<SpriteRenderer>().sprite = crateSprite;
            else
                tile.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
    }
}
