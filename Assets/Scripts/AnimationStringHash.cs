using UnityEngine;

internal static class AnimationStringHash
{
    internal static int isMoving = Animator.StringToHash("isMoving");
    internal static int isRunning = Animator.StringToHash("isRunning");
    internal static int isGrounded = Animator.StringToHash("isGrounded");
    internal static int isOnWall = Animator.StringToHash("isOnWall");
    internal static int isOnCeiling = Animator.StringToHash("isOnCeiling");
    internal static int yVelocity = Animator.StringToHash("yVelocity");
    internal static int jumpTrigger = Animator.StringToHash("jump");
    internal static int attackTrigger = Animator.StringToHash("attack");
    internal static int canMove = Animator.StringToHash("canMove");
    internal static int hasTarget = Animator.StringToHash("hasTarget");
    internal static int isAlive = Animator.StringToHash("isAlive");
    internal static int isHit = Animator.StringToHash("isHit");
}
