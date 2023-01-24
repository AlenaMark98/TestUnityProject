using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Player character;

    private Vector2 direction;
    private Vector2 pos;
    private Vector2 posBeginSwipe;
    private Vector2 posEndSwipe;
    private float startTime;
    private float endTime;
    float time;

    void Start()
    {
        character = FindObjectOfType<Player>();
    }

    private void Update()
    {  
        Move();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        pos = character.transform.position;
        startTime = time;
        posBeginSwipe = new Vector2(eventData.delta.x, eventData.delta.y);
        
        DefineDirection(eventData.delta.x, eventData.delta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endTime = time;
        posEndSwipe = new Vector2(eventData.delta.x, eventData.delta.y);
    }

    private void DefineDirection(float deltaX, float deltaY)
    {
        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            direction = deltaX > 0 ? Vector2.right : Vector2.left;
    }

    private void Move()
    {
        var distance = Mathf.Abs(posBeginSwipe.x - posEndSwipe.x);
        var timeSwipe = Mathf.Abs(endTime - startTime);

        if (distance >= character.minDistanceSwipe && timeSwipe <= character.maxTimeSwipe)
        {
            var targetPosition = pos + direction * distance;
            if (Mathf.Abs(targetPosition.x) >= character.limit)
                targetPosition.x = direction.x * character.limit;

            character.transform.position = Vector2.Lerp(character.transform.position, targetPosition, character.speed * Time.deltaTime);
        }
        
        
    }


}
