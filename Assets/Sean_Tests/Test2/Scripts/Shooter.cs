using UnityEngine;

namespace KaiganTest.Test2
{
    public class Shooter : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float fireRate = 1f;
        
        [Header("Bullet")]
        [SerializeField] private Bullet bullet;

        [Header("Unity Stuff")]
        [SerializeField] private Transform target;
        [SerializeField] private Transform shootPos;
        [SerializeField] private Transform partToTurn;

        private float lastShot = 0.0f;
        private TankMover targetTankMover;

        void Start()
        {
            //Used for getting target's "TankMover"
            targetTankMover = target.GetComponent<TankMover>();
        }

        private void Update()
        {
            //Fire
            if (IsCanFire())
            {
                //Get target intercept position and shoot
                if (CalculateInterceptPosition(
                shootPos.position,
                target.position,
                target.forward * targetTankMover.MoveSpeed,
                bulletSpeed,
                out Vector3 interceptPosition))
                {
                    //Rotate turret to intercept position
                    partToTurn.rotation = Quaternion.LookRotation(interceptPosition);

                    //Spawn Bullet
                    Fire();
                }
            }
        }

        private bool IsCanFire()
        {
            if (Time.time > fireRate + lastShot)
            {
                lastShot = Time.time;
                return true;
            }
            return false;
        }

        private void Fire()
        {
            Bullet go = Instantiate(bullet, shootPos.position, shootPos.rotation);
            go.SetBulletSpeed(bulletSpeed);
        }

        private bool CalculateInterceptPosition(Vector3 selfPosition, Vector3 targetPosition, Vector3 targetVelocity, float bulletSpeed, out Vector3 interceptPosition)
        {
            Vector3 direction = targetPosition - selfPosition;

            float a = Vector3.Dot(targetVelocity, targetVelocity) - bulletSpeed * bulletSpeed;
            float b = Vector3.Dot(direction, targetVelocity);
            float c = Vector3.Dot(direction, direction);
            float x = b * b - a * c;

            //When getting very small and inaccurate,like target move too fast, then stop aiming
            if (x < 0.1f)
            {
                interceptPosition = Vector3.zero;
                return false;
            }

            float sqrt = Mathf.Sqrt(x);
            float time1 = (-b - sqrt) / c;
            float time2 = (-b + sqrt) / c;
            float timeWillMeet = 0;

            //When result is negative then is invalid
            if (time1 < 0 && time2 < 0)
            {
                interceptPosition = Vector3.zero;
                return false;
            }
            else if (time1 < 0)
            {
                timeWillMeet = time2;
            }
            else if (time2 < 0)
            {
                timeWillMeet = time1;
            }
            else
            {
                timeWillMeet = Mathf.Max(new float[] { time1, time2 });
            }

            interceptPosition = timeWillMeet * direction + targetVelocity;
            return true;
        }
    }
}
