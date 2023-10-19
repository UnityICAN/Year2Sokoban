using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    private Vector2Int position;
    private BoardManager boardManager;
    
    public void Init(BoardManager boardManager) {
        this.boardManager = boardManager;
        position = new Vector2Int(4, 4);
        MakeVisualMovement();
    }
    
    private void MakeVisualMovement() {
        playerTransform.position = new Vector3(position.x, -position.y);
    }
    
    private void Update() {
        Vector2Int projectedPosition = position;
        
        if (Input.GetButtonDown("Horizontal")) {
            float x = Input.GetAxis("Horizontal");
            if (x > 0f)
                projectedPosition.x += 1;
            else if (x < 0f)
                projectedPosition.x -= 1;
        } else if (Input.GetButtonDown("Vertical")) {
            float y = Input.GetAxis("Vertical");
            if (y > 0f)
                projectedPosition.y -= 1;
            else if (y < 0f)
                projectedPosition.y += 1;
        }

        if (boardManager.TilesList[projectedPosition.x, projectedPosition.y] == TileType.Wall)
            projectedPosition = position;

        if (projectedPosition != position) {
            position = projectedPosition;
            MakeVisualMovement();
        }
    }
}
