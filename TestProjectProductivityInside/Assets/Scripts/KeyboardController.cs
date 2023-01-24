using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Player character;

    private Vector3 direction;

    void Start()
    {
        character = FindObjectOfType<Player>();
    }

    void Update()
    {
        Move();
    }
    public void StatusUpdate()
    {
        Debug.Log(gameObject.name + " - Получил новость");
    }
    public void UpdateControlType(string text)
    {
        Debug.Log(text);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (character.isGrounded)
        {
            direction = new Vector3(horizontal, 0, 0);
            direction = character.transform.TransformDirection(direction);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                character.transform.position = Vector3.Lerp(character.transform.position, direction * character.limit, character.speed * Time.deltaTime);
        }

    }

}
