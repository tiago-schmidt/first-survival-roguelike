using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAttributes _playerAttributes;

    private float horizontalInput;
    private float verticalInput;

    private bool isFacingRight = true;

    private void Start()
    {
        _playerAttributes = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER)
            .GetComponent<PlayerManager>().playerAttributes;
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        transform.Translate(_playerAttributes.movementSpeed * horizontalInput * Time.deltaTime * Vector3.right);
        transform.Translate(_playerAttributes.movementSpeed * verticalInput * Time.deltaTime * Vector3.forward);

        if(horizontalInput != 0)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        bool shouldFlip = (horizontalInput == 1 && !isFacingRight) || (horizontalInput == -1 && isFacingRight);

        if (shouldFlip)
        {
            GameObject playerBody = GameObject.FindGameObjectWithTag(Tags.PLAYER_BODY);
            GameObject playerShadow = GameObject.FindGameObjectWithTag(Tags.PLAYER_SHADOW);

            playerBody.transform.Rotate(Vector3.down, 180);
            playerShadow.transform.Rotate(Vector3.down, 180);

            isFacingRight = !isFacingRight;
        }
    }
}
