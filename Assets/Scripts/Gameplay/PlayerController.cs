using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Control Settings")]
    public float speed = 10f;
    public float strafeSpeed = 8f;
    public float jumpPower = 5f;
    public bool isGrounded = true;

    [Header("Camera Settings")]
    public float minPitch = -85f;
    public float maxPitch = 25f;
    public float mouseSensitivity = 160f;
    public Camera cam;
    float pitch;
    Rigidbody rigidBody;

    [Header("Misc Settings")]
    public bool gameIsPaused = false;
    public PlayerData playerData;
    public float playerHeight = 1.5f;
    public GameObject pauseMenu;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // locks and hides mouse to the centre of the screen, so the player can use the mouse to look around
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // whenever escape is pressed, pause (or unpause, if paused already) the game
        {
            pauseGame();
        }
        lookCamera();
        movePlayer();
        updateHeight();
    }

    void lookCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX); // moving the mouse left to right rotates the player model

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // clamps the pitch to stop the camera from being able to look too far up and down
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f); // only the camera will move up and down when moving the mouse up and down, the entire player model does NOT rotate
    }

    void movePlayer()
    {
        float moveForward = Input.GetAxis("Vertical");
        float moveSideways = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * moveForward * speed * Time.deltaTime); // these 2 lines move the player using WASD
        transform.Translate(Vector3.right * moveSideways * strafeSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // this makes the player jump
        {
            rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void pauseGame() // handles pausing/unpausing of the game
    {
        if (gameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            gameIsPaused = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // allow player to move mouse around while paused, so they can interact with the UI
            Cursor.visible = true;
            Time.timeScale = 0f;
            gameIsPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    void updateHeight() // sends the players Y height above 0 (subtracted by character height) to playerData to determine the player's "true" height
    {
        float heightOffset = transform.position.y - playerHeight;
        playerData.offsetHeight(heightOffset);
    }
}
