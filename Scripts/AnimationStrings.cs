using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// holds animation strings, so when the string is called, it looks for the string in the animator parameter to see if it matches
internal class AnimationStrings
{
    internal static string isMoving = "isMoving";
    internal static string isGrounded = "isGrounded";
    internal static string yVelocity = "yVelocity";
    internal static string jump = "jump";
    internal static string isOnWall = "isOnWall";
    internal static string isOnCeiling = "isOnCeiling";
    internal static string attack = "attack";
    internal static string rangedAttack = "rangedAttack";
    internal static string canMove = "canMove";
    internal static string hasTarget = "hasTarget";
    internal static string isHit = "isHit";
    internal static string attackCooldown = "attackCooldown";
    internal static string isAlive = "isAlive";
}