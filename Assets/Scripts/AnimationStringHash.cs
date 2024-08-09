using UnityEngine;

internal static class AnimationStringHash
{
    internal static int isMoving = Animator.StringToHash("isMoving");
    internal static int isRunning = Animator.StringToHash("isRunning");
    internal static int isGrounded = Animator.StringToHash("isGrounded");
    internal static int isOnWall = Animator.StringToHash("isOnWall");
    internal static int isOnCeiling = Animator.StringToHash("isOnCeiling");
    internal static int yVelocity = Animator.StringToHash("yVelocity");
    internal static int jump = Animator.StringToHash("jump");
}
