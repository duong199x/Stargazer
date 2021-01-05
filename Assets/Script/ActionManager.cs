using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SocialManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class ActionManager : MonoBehaviour
{
    public enum Behaviour { WANDERING, CHATTING}
    private Behaviour behaviour;
    [SerializeField] private float perceiveInterval;
    private float timeCounterPerceive;

    [SerializeField] private float detectPeopleRadius;
    [SerializeField] private float interationRadius;
    private Vector2 desireVelocity = new Vector2(0f,0f);
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxSteeringForce;
    private Vector2 steeringForce;
    [SerializeField] private float avoidObstacleFactor;

    private SocialManager socialManager;
    private Rigidbody2D rb2d;
    private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        socialManager = GetComponent<SocialManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        behaviour = Behaviour.WANDERING;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCounterPerceive = 0f;
    }

    private float CalculateEulerDistance(float[] array1, float[] array2)
    {
        float minLength = Mathf.Min(array1.Length, array2.Length);
        float sqrDistance = 0f;
        for (int i=0; i<minLength; i++)
        {
            sqrDistance += ((array1[i] - array2[i]) * (array1[i] - array2[i]));
        }
        return Mathf.Sqrt(sqrDistance);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timeCounterPerceive -= Time.fixedDeltaTime;
        if (timeCounterPerceive < 0f)
        {
            timeCounterPerceive = perceiveInterval;

            GameObject otherObject;
            Collider2D[] listCollision = Physics2D.OverlapCircleAll((Vector2)this.transform.position, interationRadius * transform.localScale.y);

            bool isNearSomeone = false;
            for (int i = 0; i < listCollision.Length; i++)
            {
                if (listCollision[i].gameObject != this.gameObject && listCollision[i].gameObject.tag != "scene_boundary") {
                    isNearSomeone = true;
                    break;
                }
            }
            SetBehaviour((isNearSomeone && socialManager.SocialFactor>0) ? Behaviour.CHATTING : Behaviour.WANDERING);

            desireVelocity.x = 0f;
            desireVelocity.y = 0f;

            if (behaviour == Behaviour.WANDERING)
            {
                //if (socialManager.SocialFactor > 0)
                {
                    // re-calculate desire velocity
                    SocialManager otherSocialManager;

                    listCollision = Physics2D.OverlapCircleAll((Vector2)this.transform.position, detectPeopleRadius * transform.localScale.y);
                    for (int i = 0; i < listCollision.Length; i++)
                    {
                        otherObject = listCollision[i].gameObject;
                        if (otherObject == this.gameObject) continue;
                        if (otherObject.tag == "scene_boundary")
                        {
                            SceneBoundary sceneBoundary = otherObject.GetComponent<SceneBoundary>();
                            if (sceneBoundary)
                            {
                                Vector2 delta = Vector2.zero;
                                switch (sceneBoundary.BoundaryType)
                                {
                                    case SceneBoundary.Type.TOP:
                                    case SceneBoundary.Type.BOTTOM:
                                        delta.y = this.transform.position.y - otherObject.transform.position.y;
                                        break;
                                    case SceneBoundary.Type.LEFT:
                                    case SceneBoundary.Type.RIGHT:
                                        delta.x = this.transform.position.x - otherObject.transform.position.x;
                                        break;
                                }

                                desireVelocity += delta.normalized * avoidObstacleFactor / delta.sqrMagnitude;
                            }
                        }
                        else
                        {
                            otherSocialManager = otherObject.GetComponent<SocialManager>();
                            if (otherSocialManager)
                            {
                                desireVelocity += 1 / CalculateEulerDistance(socialManager.CharacteristicVector, otherSocialManager.CharacteristicVector)
                                    * ((Vector2)(otherObject.transform.position - this.transform.position)).normalized
                                    * socialManager.SocialFactor;
                            }
                        }
                    }
                }

                if (desireVelocity.sqrMagnitude == 0f)
                {
                    desireVelocity.x = Random.Range(-1.0f, 1.0f);
                    desireVelocity.y = Random.Range(-1.0f, 1.0f);
                }

                desireVelocity = desireVelocity.normalized * maxSpeed;
            }
            else if (behaviour == Behaviour.CHATTING)
            {
                // nothing happen
            }
        }

        steeringForce = rb2d.mass * (desireVelocity * transform.localScale.y - rb2d.velocity);
        if (steeringForce.sqrMagnitude > maxSteeringForce * maxSteeringForce)
        {
            steeringForce = steeringForce.normalized * maxSteeringForce;
        }
        rb2d.AddForce(steeringForce * transform.localScale.y);
    }

    private void Update()
    {
        if (animator)
        {
            animator.SetFloat("WalkSpeed", rb2d.velocity.sqrMagnitude);
        }
        if (spriteRenderer)
        {
            spriteRenderer.flipX = rb2d.velocity.x < 0;
        }
    }

    public void SetBehaviour(Behaviour newBehaviour)
    {
        behaviour = newBehaviour;
        if (animator)
        {
            switch (behaviour)
            {
                case Behaviour.WANDERING:
                    animator.SetTrigger("Walk");
                    break;
                case Behaviour.CHATTING:
                    animator.SetTrigger("Chat");
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color drawColor = Color.white;
        drawColor.a = 0.2f;
        Gizmos.color = drawColor;
        Gizmos.DrawWireSphere(transform.position, detectPeopleRadius * transform.localScale.y);

        drawColor = Color.blue;
        drawColor.a = 0.2f;
        Gizmos.color = drawColor;
        Gizmos.DrawWireSphere(transform.position, interationRadius * transform.localScale.y);

        drawColor = Color.green;
        drawColor.a = 0.5f;
        Gizmos.color = drawColor;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)desireVelocity);
    }
}
