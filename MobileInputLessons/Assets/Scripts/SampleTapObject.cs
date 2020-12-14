using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTapObject : MonoBehaviour, ITapped, ISwiped, IDragged
{
    public Vector3 TargetPosition;
    public float speed = 10;

    public void OnEnable()
    {
        TargetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
    }

    public void OnDrag(DragEventArgs args)
    {
         
        Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
        Vector3 point = r.GetPoint(10);

        transform.position = point;
        TargetPosition = point;
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        Vector3 move_dir = Vector3.zero;
        /*
        switch(args.Direction)
        {
            case Directions.UP: move_dir.y = 1; break;
            case Directions.DOWN: move_dir.y = -1; break;
            case Directions.RIGHT: move_dir.x = 1; break;
            case Directions.LEFT: move_dir.x = -1; break;

        }*/

        move_dir = args.RawDirection.normalized;


        TargetPosition = TargetPosition + (move_dir * speed);
    }

    public void OnTap()
    {
        Destroy(gameObject);
    }
}
