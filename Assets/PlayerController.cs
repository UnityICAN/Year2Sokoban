using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    private Vector2Int position;
    
    public void Init() {
        position = new Vector2Int(4, -4);
        MakeVisualMovement();
    }
    
    private void MakeVisualMovement() {
        playerTransform.position = new Vector3(position.x, position.y);
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
                position.y += 1;
            else if (y < 0f)
                position.y -= 1;
        }
        
        MakeVisualMovement();
    }
}
