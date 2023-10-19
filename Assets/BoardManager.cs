using UnityEngine;

public class BoardManager : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;

    public TileType[,] TilesList { get; private set; }
    
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

        // Display level
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                GameObject tile = Instantiate(tilePrefab,
                    new Vector3(x, -y, 0f),
                    Quaternion.identity,
                    boardTransform);
                if (TilesList[x,y] == TileType.Wall)
                    tile.GetComponent<SpriteRenderer>().sprite = wallSprite;
                else if (TilesList[x,y] == TileType.Floor)
                    tile.GetComponent<SpriteRenderer>().sprite = floorSprite;
            }
        }
    }
}
