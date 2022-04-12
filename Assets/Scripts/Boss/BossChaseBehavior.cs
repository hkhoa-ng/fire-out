using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    private Rigidbody2D rb;
    public float bossChaseSpeed = 4f;
    private SpriteRenderer spriteRenderer;
    public float chaseDuration = 3f;
    private IEnumerator StopChasing(Animator animator) {
        yield return new WaitForSeconds(chaseDuration);
        animator.SetBool("isChasing", false);
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       playerPos = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       spriteRenderer = animator.GetComponent<SpriteRenderer>();

    //    StartCoroutine(StopChasing(animator));
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 targetDir = playerPos.position - animator.transform.position;
        Vector2 moveDir = new Vector2(targetDir.x, targetDir.y).normalized;

        // Rotate the sprite accordingly to move direction (left-right)
        if (moveDir.x < 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }

       animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, bossChaseSpeed * Time.deltaTime);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
