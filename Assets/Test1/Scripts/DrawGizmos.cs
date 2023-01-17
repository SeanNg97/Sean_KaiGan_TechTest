using UnityEngine;

namespace KaiganTest.Test1.Utilities
{
    public class DrawGizmos : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
