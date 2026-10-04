using UnityEngine;

public class myPlayerMovement : MonoBehaviour
{
    public float myMoveSpeed = 10.0f;
    public float myJumpForce = 5.0f;
    public float myJumpHeight = 100.0f;
    public float myTurnSpeed = 100.0f;
 
    void Start()
    {
        Rigidbody myRb = this.GetComponent<Rigidbody>();
        if (myRb != null)
        {
           myRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

     void Update()
    {
         float myHorizontalInput = Input.GetAxis("Horizontal");
         float myVerticalInput = Input.GetAxis("Vertical");
 
         Vector3 myMovementDirection = new Vector3(myHorizontalInput, 0, myVerticalInput);
         this.transform.Translate(myMovementDirection * myMoveSpeed * Time.deltaTime);
 
        if (Input.GetKeyDown(KeyCode.Space))
         {
             this.transform.Translate(Vector3.up * myJumpHeight * myJumpForce * Time.deltaTime);
         }

        if (Input.GetKey(KeyCode.Q))
          {
              this.transform.Rotate(Vector3.up * -myTurnSpeed * Time.deltaTime);
          }
 
        if (Input.GetKey(KeyCode.E))
          {
              this.transform.Rotate(Vector3.up * myTurnSpeed * Time.deltaTime);
          }
     }
 }