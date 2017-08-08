using Leap.Unity;
using Pear.InteractionEngine.Events;
using Pear.Models;
using UnityEngine;

namespace Pear.InteractionEngine.Controllers
{
	/// <summary>
	/// Default leap motion controller
	/// </summary>
	[RequireComponent(typeof(IHandModel))]
	[RequireComponent(typeof(HandHover))]
	public class LeapMotionController : Controller
	{
		[Tooltip("The hand that effects physics in the world")]
		[SerializeField]
		private RigidHand _physicsHand;

		/// <summary>
		/// The hand associated with this leap motion controller
		/// NOTE:
		///		Each hand will have its own leap motion controller object
		/// </summary>
		private IHandModel _hand;
		public IHandModel Hand
		{
			get
			{
				return _hand ?? (_hand = GetComponent<IHandModel>());
			}
		}

		// Hook up events
		protected override void Start()
		{
			base.Start();

			// Enable or disable this controller based on its InUse state
			InUseChangedEvent += inUse => SetActiveFromEnabledDisabled(inUse);
		}

		private void OnHover(GameObject oldValue, GameObject newValue)
		{
			SetActive(newValue ?? ModelLoader.Instance.LoadedModel);
		}

		void OnEnable()
		{
			GetComponent<HandHover>().Event.ValueChangeEvent += OnHover;

			if (!InUse)
				SetActiveFromEnabledDisabled(false);
		}

		private void OnDisable()
		{
			GetComponent<HandHover>().Event.ValueChangeEvent -= OnHover;
		}

		/// <summary>
		/// Update whether this hand is enabled or disabled, both logically and visually
		/// </summary>
		/// <param name="active">Is the hand active?</param>
		void SetActiveFromEnabledDisabled(bool active)
		{
			gameObject.SetActive(active);
			_physicsHand.gameObject.SetActive(active);
		}
	}
}
