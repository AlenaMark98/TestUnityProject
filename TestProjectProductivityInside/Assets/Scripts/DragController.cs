using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Player character;

    private Vector2 direction;
    private Vector2 pos;

    void Start()
    {
        character = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        pos = character.transform.position;
        DefineDirection(eventData.delta.x, eventData.delta.y);

    }

    private void DefineDirection(float deltaX, float deltaY)
    {
        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            direction = deltaX > 0 ? Vector2.right : Vector2.left;
    }

    private void Move()
    {
        var targetPosition = pos + direction * character.distanceDrag;

        if (Mathf.Abs(targetPosition.x) >= character.limit)
            targetPosition.x = direction.x * character.limit;

        character.transform.position = Vector2.Lerp(character.transform.position, targetPosition, character.speed * Time.deltaTime);
    }

}
