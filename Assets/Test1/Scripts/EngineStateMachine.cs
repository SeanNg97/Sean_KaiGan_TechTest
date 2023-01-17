using UnityEngine;

namespace KaiganTest.Test1
{
    public class EngineStateMachine : MonoBehaviour
    {
        enum EngineState
        {
            Accelerate,
            Idle,
            Brake
        }

        [Header("Settings")]
        [SerializeField] private float maxSpeed = 0f;
        [SerializeField] private float force = 1;
        [SerializeField] private float mass = 10;
        [SerializeField] private Transform stopPoint;

        [Header("Unity Stuff")]
        [SerializeField] private Rigidbody rb;

        private float timeMaxToZero = 1f;
        private float currentSpeed = 0f;
        private float forwardAcceleration;
        private float brakeDeceleration;
        private float timeTravel;

        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
        }

        private void Start()
        {
            //Calculation acceleration
            forwardAcceleration = force / mass;
            brakeDeceleration = -forwardAcceleration;

            //Initialize
            currentSpeed = 0;
            currentSpeed += forwardAcceleration * 1.0f;
        }

        private void Update()
        {
            switch (DetermineEngineState(GetSqrDistanceToTarget(), currentSpeed, maxSpeed))
            {
                case EngineState.Accelerate:
                    //Start Accelerate
                    currentSpeed += forwardAcceleration;
                    break;
                case EngineState.Idle:
                    //Maintain current speed at max speed
                    currentSpeed = Mathf.Max(currentSpeed, maxSpeed);
                    break;
                case EngineState.Brake:
                    //Start Deceleration
                    currentSpeed += brakeDeceleration;
                    currentSpeed = Mathf.Max(currentSpeed, 0);
                    break;
            }
        }

        private void FixedUpdate()
        {
            //Tnly move x direction
            Vector3 dir = GetDirection();
            dir.y = 0;
            dir.z = 0;
            rb.velocity = currentSpeed * dir;
        }

        private EngineState DetermineEngineState(float distanceToTarget, float currentSpeed, float maxSpeed)
        {
            timeTravel = Mathf.Max(distanceToTarget / (currentSpeed * currentSpeed), 0);

            //When travelling is going near to stop point, then start braking or distance between target is super near then brake
            if (timeTravel <= timeMaxToZero * timeMaxToZero || distanceToTarget <= 0.1 * 0.1)
            {
                return EngineState.Brake;
            }

            //When current speed + acceleration reached max speed, then maintain current speed to max speed
            if (currentSpeed >= maxSpeed)
            {
                return EngineState.Idle;
            }

            //Else, start accelerate
            return EngineState.Accelerate;
        }

        private Vector3 GetDirection()
        {
            return (stopPoint.transform.position - transform.position).normalized;
        }

        private float GetSqrDistanceToTarget()
        {
            Vector3 dir = stopPoint.position - transform.position;
            return dir.sqrMagnitude;
        }
    }
}
