using Leap.Unity;
using Pear.InteractionEngine.Controllers;
using Pear.InteractionEngine.Properties;
using UnityEngine;

namespace Pear.InteractionEngine.Events
{
	/// <summary>
	/// Detects when the leap hand starts grabbing an object and lets the object know
	/// </summary>
	[RequireComponent(typeof(HandHover))]
	public class HandGrab : ControllerBehavior<LeapMotionController>, IEvent<GameObject>
	{
		// Detects whether the hand is pinching
		private PinchDetector _pinchDetector;

		// Detects hovers
		private HandHover _handHover;

		// Stores the event value that's handled by IEventListener classes
		public Property<GameObject> Event { get; set; }

		// Use this for initialization
		void Start()
		{
			_pinchDetector = Controller.gameObject.GetComponentInChildren<PinchDetector>();
			_handHover = GetComponent<HandHover>();
		}

		void Update()
		{
			// If we started pinching and we're hoving over an object...grab it
			if (_pinchDetector.DidStartPinch && _handHover.Event.Value != null)
			{
				Event.Value = _handHover.Event.Value;
			}
			// Otherwise, if we just stopped pinching let go of all objects
			else if (_pinchDetector.DidEndPinch)
			{
				Event.Value = null;
			}
		}
	}
}
