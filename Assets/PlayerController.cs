using UnityEngine;

public class PlayerController : MonoBehaviour {
    private void Update() {
        if (Input.GetButtonUp("Horizontal")) {
            float x = Input.GetAxis("Horizontal");
            if (x > 0f)
                transform.position += Vector3.right;
            else if (x < 0f)
                transform.position -= Vector3.right;
        } else if (Input.GetButtonUp("Vertical")) {
            float y = Input.GetAxis("Vertical");
            if (y > 0f)
                transform.position += Vector3.up;
            else if (y < 0f)
                transform.position -= Vector3.up;
        }
    }
}
