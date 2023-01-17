using UnityEngine;

namespace KaiganTest.Test2
{
    public class TankMover : MonoBehaviour
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;

        void Update()
        {
            //Move the tank forward
            transform.Translate(0, 0, Time.deltaTime * MoveSpeed);
        }
    }
}

