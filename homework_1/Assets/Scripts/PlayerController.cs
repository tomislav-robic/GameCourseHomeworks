using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementPower = 120f;

    [SerializeField]
    private float jumpPower = 2.8f;

    private float powerMultiplyer = 2f;

    private Rigidbody rigidBody;
    private bool isGrounded => rigidBody.velocity.y < 0.1 && rigidBody.velocity.y > -0.1;

    private bool shouldJump = false;
    private Vector3 movementVector = Vector3.zero;

    public ICollectionHandler collectionHandler;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementPower *= powerMultiplyer;
        jumpPower *= powerMultiplyer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //go to next level
            SceneManager.LoadScene(SceneNames.Level2);
        }

        float horizontalInput = Input.GetAxis(InputStrings.Horizontal);
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis(InputStrings.Vertical);
        //Get the value of the Vertical input axis.

        movementVector = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            shouldJump = true;
        }
    }

    //50 times per second
    private void FixedUpdate()
    {
        rigidBody.AddForce(movementVector * Time.deltaTime * movementPower);

        if (shouldJump)
        {
            rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            shouldJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Collectable))
        {
            collectionHandler.PlayerDidCollectItem(other.gameObject);
        }
    }

}

public struct InputStrings
{
    public static string Horizontal = "Horizontal";
    public static string Vertical = "Vertical";
}

public struct Tags
{
    public static string Collectable = "Collectable";
}

public struct SceneNames
{
    public static string Level1 = "Level1";
    public static string Level2 = "Level2";
}
