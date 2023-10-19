using UnityEngine;

public class PlayerController : MonoBehaviour {
    private BoardManager boardManager;
    private Vector2Int position;

    public void Init(Vector2Int initialPosition, BoardManager boardManager) {
        this.boardManager = boardManager;
        position = initialPosition;
        ApplyVisualMovement();
    }

    private void ApplyVisualMovement() {
        transform.position = new Vector2(position.x, -position.y);
    }
    
    private void Update() {
        // Calculer la position désirée par le joueur
        Vector2Int desiredPosition = position;
        if (Input.GetButtonUp("Horizontal")) {
            float x = Input.GetAxis("Horizontal");
            if (x > 0f)
                desiredPosition.x += 1;
            else if (x < 0f)
                desiredPosition.x -= 1;
        } else if (Input.GetButtonUp("Vertical")) {
            float y = Input.GetAxis("Vertical");
            if (y > 0f)
                desiredPosition.y -= 1;
            else if (y < 0f)
                desiredPosition.y += 1;
        }
        
        // Vérifier si position est possible
        if (boardManager.TilesList[desiredPosition.x, desiredPosition.y] == TileType.Floor) {
            position = desiredPosition;
            // Appliquer la position
            ApplyVisualMovement();
        }
    }
}
