using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class PlayerControllerInput : MonoBehaviour
	{
		[Header("Player Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

		void Awake(){
			InputActionMap playerInput = GetComponent<PlayerInput>().actions.FindActionMap("Player");
			playerInput.FindAction("Move").performed += OnMove;
			playerInput.FindAction("Look").performed += OnLook;
			playerInput.FindAction("Jump").performed += OnJump;
			playerInput.FindAction("Sprint").performed += OnSprint;
			playerInput.FindAction("Sprint").canceled += OnSprint;
		}
		public void OnMove(InputAction.CallbackContext value)
		{
			if (!value.performed){
				return;
			}
			MoveInput(value.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext value)
		{
			if (!value.performed){
				return;
			}
			if(cursorInputForLook)
			{
				LookInput(value.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext value)
		{
			JumpInput(value.performed);
		}

		public void OnSprint(InputAction.CallbackContext value)
		{
			SprintInput(value.performed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}