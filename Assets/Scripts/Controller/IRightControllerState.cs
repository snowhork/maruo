using UnityEngine;
using System.Collections;

public interface IRightControllerState {
    void BeforeUpdateAction(RightViveController controller);
    void PressDownAction(RightViveController controller);
    void PressUpAction(RightViveController controller);
}

public class Singleton<T> where T : new()
{
    private static T _instance = new T();
    public static T Instance { get { return _instance; } }
}

namespace RightControllerState
{
    public class FreeState : Singleton<FreeState>, IRightControllerState
    {
        public void PressDownAction(RightViveController controller)
        {
            if (controller.Reciever == null)
            {
                return;
            }
            controller.Reciever.Select(controller);
        }

        public void BeforeUpdateAction(RightViveController controller)
        {

        }
        public void PressUpAction(RightViveController controller)
        {
            return;
        }
    }

	public class DrawState : Singleton<DrawState>, IRightControllerState
	{
		public void PressDownAction(RightViveController controller)
		{
			
		}

		public void BeforeUpdateAction(RightViveController controller)
		{
			controller.HoldItem.Drawing(controller);
		}
		public void PressUpAction(RightViveController controller)
		{
			controller.HoldItem.Release(controller);
			controller.State = FreeState.Instance;
			controller.HoldItem = null;
		}
	}

    public class HoldState : Singleton<HoldState>, IRightControllerState
    {
        public void BeforeUpdateAction(RightViveController controller)
        {
            controller.HoldItem.Holding(controller);
        }
        public void PressDownAction(RightViveController controller)
        {
            return;
        }

        public void PressUpAction(RightViveController controller)
        {
            controller.HoldItem.Release(controller);
            controller.State = FreeState.Instance;
            controller.HoldItem = null;
        }
    }
}