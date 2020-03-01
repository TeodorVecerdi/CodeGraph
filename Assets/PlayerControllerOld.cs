using UnityEngine;

public class PlayerControllerOld : MonoBehaviour {
    public float Speed = 5f;
    private void Update() {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f) * (Speed * Time.deltaTime);
    }
}
