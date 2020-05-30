using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    public IPlayerInput ControllerInput { get; set; } = new PlayerInput();

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        //_playerInput = new PlayerInput();
    }

    private void Update()
    {
        Vector3 movementInput = new Vector3(ControllerInput.Horizontal, 0, ControllerInput.Vertical);
        Vector3 movement = transform.rotation * movementInput;
        _characterController.SimpleMove(movement);
    }
}

public class PlayerInput : IPlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
}

public interface IPlayerInput
{
    float Vertical { get; }
    float Horizontal { get; }
}