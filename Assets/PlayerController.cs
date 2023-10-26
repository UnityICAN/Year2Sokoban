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
        if (boardManager.TilesList[desiredPosition.x, desiredPosition.y] == TileType.Floor
            && !CheckIfPositionOutOfBounds(desiredPosition)
            && position != desiredPosition) {
            // Vérifier si on doit pousser une boite
            if (boardManager.BoxesList.Contains(new Vector2Int(desiredPosition.x, desiredPosition.y))) {
                Vector2Int positionDelta = desiredPosition - position;
                Vector2Int boxDesiredPosition = desiredPosition + positionDelta;
                if (boardManager.TilesList[boxDesiredPosition.x, boxDesiredPosition.y] == TileType.Wall
                    || boardManager.BoxesList.Contains(boxDesiredPosition)
                    || CheckIfPositionOutOfBounds(boxDesiredPosition))
                    return;

                boardManager.BoxesList.Remove(desiredPosition);
                boardManager.BoxesList.Add(boxDesiredPosition);
            }
            position = desiredPosition;
            // Appliquer la position
            ApplyVisualMovement();
            boardManager.HandleMove();
        }
    }

    private bool CheckIfPositionOutOfBounds(Vector2Int position) {
        return position.x < 0 || position.x > 9 || position.y < 0 || position.y > 9;
    }
}
