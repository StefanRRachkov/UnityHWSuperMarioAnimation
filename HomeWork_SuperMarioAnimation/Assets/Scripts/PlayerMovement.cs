using UnityEngine;
using static UnityEngine.Mathf;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	[Range(0, 5)]
	private float moveSpeed = 2;

	[SerializeField]
	[Range(0.1f, 2)]
	public float gravity = 0.5f;

	private bool isCrouching = false;
	private bool isJumping = false;
	private bool canReceiveInput = true;
	private readonly float movementThreshold = 0.01f;
	private Vector2 velocity = Vector2.zero;

	[SerializeField]
	private KeyCode jumpKey = KeyCode.W;

	[SerializeField]
	private KeyCode crouchKey = KeyCode.S;

	private Animator animator;
	private new Rigidbody2D rigidbody;

	// We can use this param to change back to small mario
	private bool bBig;

	void Start() {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();

		bBig = false;
	}

	void Update() {
		if (canReceiveInput)
		{
			isCrouching = Input.GetKey(crouchKey);
			if (!isJumping) {
				animator.SetBool("IsCrouching", isCrouching);
				if (isCrouching) {
					return;
				}
			}

			velocity.x = Input.GetAxis("Horizontal");
			animator.SetFloat("HorizontalMovement", Abs(velocity.x));

			if (Abs(velocity.x) > movementThreshold) {
				transform.localScale = new Vector3(Sign(velocity.x), 1, 1);
			}

			if (!isJumping && Input.GetKeyDown(jumpKey)) {
				velocity.y = 1;
				isJumping = true;
				animator.SetBool("IsJumping", true);
			}

			rigidbody.MovePosition(rigidbody.position + velocity * moveSpeed * Time.deltaTime);

			if (isJumping) {
				velocity.y -= gravity * Time.deltaTime;
			}
		}
	}

	// Utility Function to use in Transition animation as Events

	private void EnableInput()
	{
		canReceiveInput = true;
	}

	private void DisableInput()
	{
		canReceiveInput = false;
	}

	
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Floor")) {
			isJumping = false;
			animator.SetBool("IsJumping", false);
			velocity.y = 0;
		}
		else if (collision.gameObject.CompareTag("QuestionBox"))
		{
			velocity.y = -1;
		}
		else if (collision.gameObject.CompareTag("Mushroom"))
		{
			Debug.Log("Go Big or go home!");
			// Triger is set to play the animation for transitioning
			animator.SetTrigger("Transition");
			animator.SetBool("IsBig", true);
			
			// Making the Collider bigger 'cause our character is bigger now
			GetComponent<BoxCollider2D>().size = new Vector2(0.16f, 0.28f);
			
			Destroy(collision.gameObject);
			
			//I will leave this for future purposes:
			bBig = true;
		}
	}
}
