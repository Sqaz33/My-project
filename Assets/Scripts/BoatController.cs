using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    public Vector3 resetPosition = new Vector3(2323.22f, 5848.29f, -6413.29f);


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Замораживаем все оси вращения, КРОМЕ Y, и замораживаем ось Y перемещения (чтобы не взлетала)
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {
        Move();
        Rotate();
        CheckResetPosition();
    }

    void Move()
    {
        float vertical = Input.GetAxis("Vertical"); // W/S

        // "Вперёд" на плоскости XZ (игнорируем наклон лодки по X)
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
        Vector3 moveDirection = forwardOnPlane * (-vertical) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveDirection);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X"); // движение мыши влево/вправо
        float rotationY = mouseX * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, 0f, rotationY);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    
        // Метод для сброса позиции лодки
    void CheckResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ResetBoatPosition();
        }
    }

    void ResetBoatPosition()
    {
        // Сбрасываем позицию лодки на resetPosition
        rb.position = resetPosition;

        // Сбрасываем скорость и угловую скорость, чтобы лодка не продолжала движение
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // При необходимости сбросить поворот (например, на начальный)
        rb.rotation = Quaternion.Euler(-90f, 0f, -123.452f);
    }
}
