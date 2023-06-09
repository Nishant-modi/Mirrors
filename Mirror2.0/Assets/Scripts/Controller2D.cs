using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public LayerMask collisionMask;
    public LayerMask moveBoxMask;
    public DeathLine death;
    public FinishLine finish;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public float missedJumpDistance;
    public float missedJumpSpeedMultiplier;
    public int dir;

    float horizontalRaySpacing, verticalRaySpacing;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

    public CollisionInfo collisions;
    Player player;



    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        player = GetComponent<Player>();
        CalculateRaySpacing();
    }



    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }


    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            RaycastHit2D moveHit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, moveBoxMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;

                if(hit.transform.tag == "StaticObstacle")
                {
                    if ((hit.distance - skinWidth) < missedJumpDistance && (hit.distance - skinWidth) > 0.001 && dir == 1 && player.isGrounded == false)
                    {
                        velocity.y = -Mathf.Abs((hit.distance - skinWidth) * missedJumpSpeedMultiplier);


                        Debug.Log("" + hit.distance);
                    }
                    if ((hit.distance - skinWidth) < missedJumpDistance && (hit.distance - skinWidth) > 0.001 && dir == -1 && player.isGrounded == false)
                    {
                        velocity.y = Mathf.Abs((hit.distance - skinWidth) * missedJumpSpeedMultiplier);

                        Debug.Log("" + hit.distance);
                    }
                }
                

            }
            if(hit && hit.transform.tag == "MoveObstacle")
            {
                Debug.Log("moving obstacle hit");


                velocity.x = (hit.distance - skinWidth) * directionX;
                hit.rigidbody.AddForce(new Vector2(velocity.x*100, 0), ForceMode2D.Force);
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }

            if (hit && hit.transform.tag == "DeathObstacle")
            {
                Debug.Log("death obstacle hit");
                finish.countPlayer = 0;
                death.Death();
            }

            if (hit && hit.transform.tag == "FinishObstacle")
            {
                this.gameObject.SetActive(false);
                Debug.Log("finish obstacle hit");
                finish.Finish(this.gameObject);
            }

        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y)+ skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            RaycastHit2D moveHit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, moveBoxMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit || moveHit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }

            if (hit && hit.transform.tag == "DeathObstacle")
            {
                Debug.Log("death obstacle hit");
                finish.countPlayer = 0;
                death.Death();
            }

            if (hit && hit.transform.tag == "FinishObstacle")
            {
                this.gameObject.SetActive(false);
                Debug.Log("finish obstacle hit");
                finish.Finish(this.gameObject);
            }
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }

}
