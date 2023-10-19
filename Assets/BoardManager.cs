using UnityEngine;

public class BoardManager : MonoBehaviour {
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardTransform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;

    private TileType[,] tilesList;
    
    private void Start() {
        // Créer le niveau
        tilesList = new TileType[10, 10];
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                if (x == 0 || y == 0 || x == 9 || y == 9)
                    tilesList[x, y] = TileType.Wall;
                else
                    tilesList[x, y] = TileType.Floor;
            }
        }

        playerController.Init(new Vector2Int(4, 4));

        // Afficher le niveau
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                GameObject tile = Instantiate(tilePrefab,
                    new Vector3(x, -y, 0f),
                    Quaternion.identity,
                    boardTransform);
                if (tilesList[x,y] == TileType.Wall)
                    tile.GetComponent<SpriteRenderer>().sprite = wallSprite;
                else if (tilesList[x,y] == TileType.Floor)
                    tile.GetComponent<SpriteRenderer>().sprite = floorSprite;
            }
        }
    }
}
