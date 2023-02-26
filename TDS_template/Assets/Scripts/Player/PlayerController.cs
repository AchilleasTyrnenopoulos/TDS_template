using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterController CharController { get; private set; }

    public bool isGrounded = true;

    [Tooltip("Force applied downward when in the air")]
    [SerializeField] private float _gravityDownForce = 20f;
    public float GetGravityDownForce() => _gravityDownForce;

    private CollisionFlags _collisionFlags;
    private Vector3 _motion;
    private Vector3 _newVelocity;
    private Vector3 _horizontalVelocityDelta;
    private float _stickyOffset = 0f;

    [SerializeField] private LayerMask _groundCheckLayers = -1;
    [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
    public float groundCheckDistance = 0.05f;

    private const float GroundCheckDistanceInAir = 0.07f;
    private const float JumpGroundingPreventionTime = 0.02f;

    private Vector3 _groundNormal;
    public Vector3 GetGroundNormal() => _groundNormal;

    private void Awake()
    {
        //cache the CharacterContoller component
        CharController = this.gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {
        CharController.enableOverlapRecovery = true;
    }

    private void Update()
    {
        bool wasGrounded = isGrounded;
        GroundCheck();

        if (isGrounded && !wasGrounded)
        {
            //fall damage if game has it
            // Fall damage
            //float fallSpeed = -Mathf.Min(CharacterVelocity.y, m_LatestImpactSpeed.y);
            //float fallSpeedRatio = (fallSpeed - MinSpeedForFallDamage) /
            //                       (MaxSpeedForFallDamage - MinSpeedForFallDamage);
            //if (RecievesFallDamage && fallSpeedRatio > 0f)
            //{
            //    float dmgFromFall = Mathf.Lerp(FallDamageAtMinSpeed, FallDamageAtMaxSpeed, fallSpeedRatio);
            //    m_Health.TakeDamage(dmgFromFall, null);

            //    // fall damage SFX
            //    AudioSource.PlayOneShot(FallDamageSfx);
            //}
            //    else
            //{
            //    // land SFX
            //    AudioSource.PlayOneShot(LandSfx);
            //}

        }
    }

    private void GroundCheck()
    {
        // Make sure that the ground check distance while already in air is very small, to prevent suddenly snapping to ground
        float chosenGroundCheckDistance = isGrounded ? (CharController.skinWidth + groundCheckDistance) : GroundCheckDistanceInAir;

        // reset values before the ground check
        isGrounded = false;
        _groundNormal = Vector3.up;

        // only try to detect ground if it's been a short amount of time since last jump; otherwise we may snap to the ground instantly after we try jumping
        if (Time.time >= JumpGroundingPreventionTime)
        {
            // if we're grounded, collect info about the ground normal with a downward capsule cast representing our character capsule
            if (Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(CharController.height),
                CharController.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, _groundCheckLayers,
                QueryTriggerInteraction.Ignore))
            {
                // storing the upward direction for the surface found
                _groundNormal = hit.normal;

                // Only consider this a valid ground hit if the ground normal goes in the same direction as the character up
                // and if the slope angle is lower than the character controller's limit
                if (Vector3.Dot(hit.normal, transform.up) > 0f &&
                    IsNormalUnderSlopeLimit(_groundNormal))
                {
                    isGrounded = true;

                    // handle snapping to the ground
                    if (hit.distance > CharController.skinWidth)
                    {
                        CharController.Move(Vector3.down * hit.distance);
                    }
                }
            }
        }
    }

    private bool IsNormalUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(transform.up, normal) <= CharController.slopeLimit;
    }

    // Gets the center point of the bottom hemisphere of the character controller capsule    
    public Vector3 GetCapsuleBottomHemisphere()
    {
        return transform.position + (transform.up * CharController.radius);
    }

    // Gets the center point of the top hemisphere of the character controller capsule    
    public Vector3 GetCapsuleTopHemisphere(float atHeight)
    {
        return transform.position + (transform.up * (atHeight - CharController.radius));
    }

}
