using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
	[SerializeField] float jumpHeight;
	[SerializeField] float runSpeed;
	float x;
	float y;

	Animator animator;
	Rigidbody2D rb;
	SpriteRenderer[] sprites;
	bool defaultFacingRight = true;
	bool facingRight = true;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody2D>();
		sprites = GetComponentsInChildren<SpriteRenderer>();
	}
	void Update()
	{
		Inputs();
		Move();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
			animator.SetBool("Jumping", true);
		}

	}


	void Inputs()
	{
		x = Input.GetAxisRaw("Horizontal") * runSpeed;
		y = Input.GetAxisRaw("Vertical");
		animator.SetFloat("Running", Mathf.Abs(x));
	}
	void Move()
	{
		float yPos = transform.position.y;
		Vector3 movement = new Vector2(x, yPos);
		movement = Vector3.ClampMagnitude(movement, 1);
		rb.velocity = movement.normalized * runSpeed;
		if (movement.x < 0)
		{
			foreach (SpriteRenderer rend in sprites)
			{
				rend.flipX = (defaultFacingRight != facingRight);
			}
		}
		else if (movement.x > 0)
		{
			foreach (SpriteRenderer rend in sprites)
			{
				rend.flipX = (defaultFacingRight = facingRight);
			}
		}
	}

}
