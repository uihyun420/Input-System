using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeTest : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Color defaultColor;

    private Vector2 cubeMove;
    private Vector2 cubeRotate;
    private Color cubeColor;

    private void Start()
    {
        SetColor(defaultColor);
        cubeColor = defaultColor;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        cubeMove = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        cubeRotate = context.ReadValue<Vector2>();
    }

    public void OnChangeColor(InputAction.CallbackContext context)
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            cubeColor = Color.red;
        else if (Gamepad.current.buttonEast.wasPressedThisFrame)
            cubeColor = Color.blue;
        else if (Gamepad.current.buttonWest.wasPressedThisFrame)
            cubeColor = Color.green;
        else if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            cubeColor = Color.yellow;
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }

        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Rotate(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }

        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        transform.Rotate(0, direction.x * scaledRotateSpeed, 0);
    }

    private void SetColor(Color color)
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }


    private void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
    private void Update()
    {
        Move(cubeMove);
        Rotate(cubeRotate);
        ChangeColor(cubeColor);
    }
}
