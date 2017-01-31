using UnityEngine;
using System.Collections;

public interface IRightControllerState
{
    void PressDownAction(RightViveController controller);
    void BeforeUpdateAction(RightViveController controller);
    void PressUpAction(RightViveController controller);
}

namespace RightControllerState
{
    public abstract class RightControllerStateSingleton<T> : IRightControllerState where T : new()
    {
        public static T Instance { get; } = new T();
        public virtual void PressDownAction(RightViveController controller)
        {

        }

        public virtual void BeforeUpdateAction(RightViveController controller)
        {

        }
        public virtual void PressUpAction(RightViveController controller)
        {

        }
    }

    public class FreeState : RightControllerStateSingleton<FreeState>
    {
        public override void PressDownAction(RightViveController controller)
        {
            controller.Reciever?.Select(controller);
        }
    }

	public class DrawState : RightControllerStateSingleton<DrawState>
	{
		public override void BeforeUpdateAction(RightViveController controller)
		{
			controller.HoldItem.Drawing(controller);
		}
		public override void PressUpAction(RightViveController controller)
		{
			controller.HoldItem.Release(controller);
			controller.State = FreeState.Instance;
			controller.HoldItem = null;
		}
	}

    public class HoldState : RightControllerStateSingleton<HoldState>
    {
        public override void BeforeUpdateAction(RightViveController controller)
        {
            controller.HoldItem.Holding(controller);
        }

        public override void PressUpAction(RightViveController controller)
        {
            controller.HoldItem.Release(controller);
            controller.State = FreeState.Instance;
            controller.HoldItem = null;
        }
    }
}