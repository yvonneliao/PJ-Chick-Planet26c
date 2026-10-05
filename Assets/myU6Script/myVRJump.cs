using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class VRJump : MonoBehaviour
{
    public float _myJumpForce = 5f;
    public float _myGravity = 9.81f;
    private CharacterController _myController;
    private float _myVerticalVelocity;

    void Start()
    {
        _myController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_myController.isGrounded)
        {
            _myVerticalVelocity = -0.5f; 

            if (OVRInput.GetDown(OVRInput.Button.One)) 
            {
                _myVerticalVelocity = _myJumpForce;
            }
        }

        _myVerticalVelocity -= _myGravity * Time.deltaTime;
        Vector3 _myJumpMovement = new Vector3(0, _myVerticalVelocity, 0);
        _myController.Move(_myJumpMovement * Time.deltaTime);
    }
}