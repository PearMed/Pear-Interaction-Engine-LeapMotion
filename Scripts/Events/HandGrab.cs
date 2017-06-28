using Leap.Unity;
using Pear.InteractionEngine.Controllers;
using Pear.InteractionEngine.Properties;
using UnityEngine;

namespace Pear.InteractionEngine.Events
{
	/// <summary>
	/// Detects when the leap hand starts grabbing an object and lets the object know
	/// </summary>
	public class HandGrab : ControllerBehavior<LeapMotionController>, IEvent<GameObject>
	{
		// Detects whether the hand is pinching
		private PinchDetector _pinchDetector;

		// The last object that was hovered over
		private GameObject _lastHovered;

		// Stores the event value that's handled by IEventListener classes
		public Property<GameObject> Event { get; set; }

		// Use this for initialization
		void Start()
		{
			_pinchDetector = Controller.gameObject.GetComponentInChildren<PinchDetector>();

			// Detects when objects are in close proximity
			ProximityDetector proximityDetector = Controller.gameObject.GetComponentInChildren<ProximityDetector>();

			// Save the last RTS we hovered over
			proximityDetector.OnProximity.AddListener(hovered => _lastHovered = hovered);

			// Detect when we stop hovering
			proximityDetector.OnDeactivate.AddListener(() => _lastHovered = null);
		}

		void Update()
		{
			// If we started pinching and we're hoving over an object...grab it
			if (_pinchDetector.DidStartPinch && _lastHovered != null)
			{
				Event.Value = _lastHovered;
			}
			// Otherwise, if we just stopped pinching let go of all objects
			else if (_pinchDetector.DidEndPinch)
			{
				Event.Value = null;
			}
		}
	}
}
