using UnityEngine;

namespace TargetLocked
{
    public class Imploder : MonoBehaviour
    {
        private const int LEFT_CLICK = 0;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                var go = Instantiate(ExplosionGrenade.PREFAB);
                go.transform.position = transform.position + GetComponentInChildren<Camera>().transform.forward;
                go.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 1, 0, ForceMode.Impulse);
            }
        }
    }
}