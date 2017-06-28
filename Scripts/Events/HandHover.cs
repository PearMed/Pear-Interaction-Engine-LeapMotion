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
		// Stores the event value that's handled by IEventListener classes
		public Property<GameObject> Event { get; set; }

		void Start()
		{
			ProximityDetector proximityDetector = Controller.gameObject.GetComponentInChildren<ProximityDetector>();

			// When the hand starts hovering over the object
			// let the object know
			proximityDetector.OnProximity.AddListener(hoverObj => Event.Value = hoverObj);

			// When the hand stops hovering over the object
			// let the object know
			proximityDetector.OnDeactivate.AddListener(() => Event.Value = null);
		}		
	}
}
