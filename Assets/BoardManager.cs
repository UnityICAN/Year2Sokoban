using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameObject tilePrefab;
    public Transform boardTransform;

    public Sprite wallSprite;
    public Sprite floorSprite;

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
