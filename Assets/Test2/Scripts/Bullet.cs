using UnityEngine;

namespace KaiganTest.Test2
{
    public class Bullet : MonoBehaviour
    {
        private float _bulletSpeed = 0;

        public void SetBulletSpeed(float speed)
        {
            this._bulletSpeed = speed;
        }

        private void Update()
        {
            //Move bullet forward
            transform.Translate(0, 0, Time.deltaTime * _bulletSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TankMover>(out TankMover targetTank))
            {
                Destroy(gameObject);
            }
        }
    }

}
