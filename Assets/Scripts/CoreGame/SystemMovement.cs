using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

namespace GameCore
{
    namespace SystemMovements
    {
        public class Movement 
        {
            public static Vector2 AxisDeltaTime
            {
                get
                {
                    return Controllers.GetJoystick(1,1) * Time.deltaTime;
                }
            }

            public static void MoveForward(Transform t, float speed)
            {
                t.Translate(Vector3.forward * speed * AxisDeltaTime.y);

            }

            public static void RotateUp(Transform t, float speed)
            {
                t.Rotate(t.up * speed * AxisDeltaTime.x);
            }

            public static void JumpUp(Rigidbody rb, float jumpForce)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            public static void Move2D(Transform t, float speed)
            {
                t.Translate(Vector3.forward * speed * AxisDeltaTime.x);

            }

            public static void RotateY(Transform t)
            {
                t.localScale = Controllers.GetJoystick(1, 1).x > 0 ? new Vector3(1f, 2.1439f, 1f) :
                    Controllers.GetJoystick(1, 1).x < 0 ? new Vector3(1f, 2.1439f, -1f) :
                    t.localScale;
            }

            public static void StartSlide(Rigidbody rb, GameObject standingCollider, GameObject slidingCollider
                , Vector3 forward, float dashForce)
            {
                standingCollider.SetActive(false);
                slidingCollider.SetActive(true);
                rb.AddForce(forward * dashForce, ForceMode.Impulse);
            }

            public static void EndSlide(Rigidbody rb, GameObject standingCollider, GameObject slidingCollider
                , Vector3 forward, float dashForce)
            {
                standingCollider.SetActive(true);
                slidingCollider.SetActive(false);
            }
        }
    }
}