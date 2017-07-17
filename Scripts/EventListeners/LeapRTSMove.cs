using Leap.Unity;
using UnityEngine;
using Pear.InteractionEngine.Utils;
using Pear.InteractionEngine.Events;

namespace Pear.InteractionEngine.EventListeners
{
	public class LeapRTSMove : MonoBehaviour, IEventListener<GameObject>
	{
		[Tooltip("Method used to rotate with one hand")]
		public LeapRTSMoveHelper.RotationMethod OneHandedRotationMethod;

		[Tooltip("Method used to rotate with two hands")]
		public LeapRTSMoveHelper.RotationMethod TwoHandedRotationMethod;

		// Handles core logic to link Leap to PIE
		private LeapRTSMoveHelper _moveHelper;

		public void Start()
		{
			// LeapRTSMoveHelper is what actually moves the object. We need to update it to make
			// sure it's in the correct state
			_moveHelper = transform.GetOrAddComponent<LeapRTSMoveHelper>();
			_moveHelper.OneHandedRotationMethod = OneHandedRotationMethod;
			_moveHelper.TwoHandedRotationMethod = TwoHandedRotationMethod;
		}

		public void ValueChanged(EventArgs<GameObject> args)
		{
			// LeapRTS uses the PinchDetector to track movement
			PinchDetector detector = null;
			if (args.NewValue == gameObject)
				detector = args.Source.gameObject.GetComponentInChildren<PinchDetector>();

			bool isLeftHand = args.Source.GetComponent<IHandModel>().GetLeapHand().IsLeft;
			if (isLeftHand)
				_moveHelper.PinchDetectorA = detector;
			else
				_moveHelper.PinchDetectorB = detector;
		}
	}
}
