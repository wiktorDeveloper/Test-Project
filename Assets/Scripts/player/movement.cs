 using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    float playerSpeed = 4f;
    float gravity = -9.81f;
    Vector3 velocity;

    void Update()
    {
        // Prosty skrypt na poruszanie siê. Nic zaawansowanego

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move = move.normalized * playerSpeed;

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        velocity.y += gravity * Time.deltaTime;

        Vector3 totalMove = move * Time.deltaTime;
        totalMove.y = velocity.y * Time.deltaTime;

        cc.Move(totalMove);
    }
}
