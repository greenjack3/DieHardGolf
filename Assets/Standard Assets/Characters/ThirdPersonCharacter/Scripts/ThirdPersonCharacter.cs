using UnityEngine;

	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		public float animSpeed = 1f;
		public float groundCheck = 0.1f;

		Rigidbody rb;
		Animator anim;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		
		void Start()
		{
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_OrigGroundCheckDistance = groundCheck;
		}


		public void Move(Vector3 move, bool jump)
		{

			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if (m_IsGrounded)
			{
				HandleJump(jump);
			}
			else
			{
				HandleAirborneMovement();
			}

				
			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}

		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			anim.SetFloat("Speed", m_ForwardAmount, 0.1f, Time.deltaTime);
			anim.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			anim.SetBool("Grounded", m_IsGrounded);
			if (!m_IsGrounded)
			{
				anim.SetFloat("Jump", rb.velocity.y);
			}

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					anim.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				anim.SetFloat("LegJ", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (m_IsGrounded && move.magnitude > 0)
			{
				anim.speed = animSpeed;
			}
			else
			{
				// don't use that while airborne
				anim.speed = 1;
			}
		}


		void HandleAirborneMovement()
		{
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			rb.AddForce(extraGravityForce);

			groundCheck = rb.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}


		void HandleJump(bool jump)
		{
			if (jump && anim.GetCurrentAnimatorStateInfo(0).IsName("Ground"))
			{
				rb.velocity = new Vector3(rb.velocity.x, m_JumpPower, rb.velocity.z);
				m_IsGrounded = false;
				anim.applyRootMotion = false;
				groundCheck = 0.1f;
			}
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (anim.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = rb.velocity.y;
				rb.velocity = v;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;

			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheck))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
			}
		}
	}
