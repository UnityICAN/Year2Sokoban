using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector2Int position;

    public void Init(Vector2Int initialPosition) {
        position = initialPosition;
        ApplyVisualMovement();
    }

    private void ApplyVisualMovement() {
        transform.position = new Vector2(position.x, -position.y);
    }
    
    private void Update() {
        if (Input.GetButtonUp("Horizontal")) {
            float x = Input.GetAxis("Horizontal");
            if (x > 0f)
                position.x += 1;
            else if (x < 0f)
                position.x -= 1;
        } else if (Input.GetButtonUp("Vertical")) {
            float y = Input.GetAxis("Vertical");
            if (y > 0f)
                position.y -= 1;
            else if (y < 0f)
                position.y += 1;
        }
        
        ApplyVisualMovement();
    }
}
