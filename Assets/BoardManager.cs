using UnityEngine;

public class BoardManager : MonoBehaviour {
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;

    public TileType[,] TilesList { get; private set; }
    
    private void Start() {
        // Créer le niveau
        TilesList = new TileType[10, 10];
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (x == 0 || y == 0 || x == 9 || y == 9)
                    TilesList[x, y] = TileType.Wall;
                else
                    TilesList[x, y] = TileType.Floor;
            }
        }

        TilesList[4, 5] = TileType.Wall;

        playerController.Init(new Vector2Int(4, 4), this);

        // Afficher le niveau
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
