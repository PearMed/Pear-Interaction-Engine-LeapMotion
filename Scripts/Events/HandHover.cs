using Pear.InteractionEngine.Controllers;
using Leap.Unity;
using Pear.InteractionEngine.Properties;
using UnityEngine;

namespace Pear.InteractionEngine.Events
{
	/// <summary>
	/// Update the event value when the leap motion starts and stops hovering over a game object
	/// </summary>
	public class HandHover : ControllerBehavior<LeapMotionController>, IEvent<GameObject>
	{
		private ProximityDetector _detector;

		// Stores the event value that's handled by IEventListener classes
		public Property<GameObject> Event { get; set; }

		private void OnEnable()
		{
			_detector = Controller.gameObject.GetComponentInChildren<ProximityDetector>();

			// When the hand starts hovering over the object
			// let the object know
			_detector.OnProximity.AddListener(OnHoverStart);

			// When the hand stops hovering over the object
			// let the object know
			_detector.OnDeactivate.AddListener(OnHoverEnd);
		}

		private void OnDisable()
		{
			_detector.OnProximity.RemoveListener(OnHoverStart);
			_detector.OnDeactivate.RemoveListener(OnHoverEnd);
		}

		private void OnHoverStart(GameObject hovered)
		{
			Event.Value = hovered;
		}

		private void OnHoverEnd()
		{
			Event.Value = null;
		}
	}
}
