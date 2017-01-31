using UnityEngine;
using System.Collections;
using RightControllerState;

public interface IItemState
{
    void MoveOnUpdate(Item item);
    void OnCollisionEnter(Item item, Collision collision);
    void OnTriggerEnter(Item item, Collider collider);
}

namespace ItemState
{
    public abstract class ItemStateSingleton<T> : IItemState where T : new()
    {
        public static T Instance { get; } = new T();
        public RightViveController Controller { get; set; }

        public virtual void MoveOnUpdate(Item item)
        {

        }

        public virtual void OnCollisionEnter(Item item, Collision collision)
        {

        }

        public virtual void OnTriggerEnter(Item item, Collider collider)
        {

        }

        protected void PositionTracking(Item item, Vector3 position)
        {
            item.transform.position = Vector3.Lerp(position, position, 0.2f);
            item.Rigidbody.isKinematic = true;
        }

        protected void PositionHolding(Item item, Vector3 position)
        {
            item.transform.position = position;
            item.Rigidbody.isKinematic = true;
        }
    }

    public class StaticState: ItemStateSingleton<StaticState>
    {

    }

    public class DrawState: ItemStateSingleton<DrawState>
    {
        public override void MoveOnUpdate(Item item)
        {
            PositionTracking(item, Controller.transform.position);
            if (Vector3.Distance (item.transform.position, Controller.transform.position) <= 0.01f)
            {
                item.State = HoldState.Instance;
            }
        }
    }

    public class HoldState: ItemStateSingleton<HoldState>
    {
        public override void MoveOnUpdate(Item item)
        {
            PositionHolding(item, Controller.transform.position);
        }
    }

    public class DropState: ItemStateSingleton<DropState>
    {

    }

}