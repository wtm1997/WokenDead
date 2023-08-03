using UnityEngine;

namespace CommonUnity
{
    public class UpdateRotateEffect : MonoBehaviour
    {
        public float rotSpeed = 0;

        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, -rotSpeed));
        }
    }
}