using UnityEngine;
using UnityEngine.Serialization;

namespace ShockEffect
{
    public class ShockOnCollision : MonoBehaviour
    {
        public Material ShockMaterial; 
        public float ShockDuration = 1f;

        private float _shockTimer = -1f;
        private Vector2 _shockCentreUV = new Vector2(0.5f, 0.5f);

        void Update()
        {
            if (_shockTimer >= 0f)
            {
                _shockTimer += Time.deltaTime;
                float t = _shockTimer / ShockDuration;

                ShockMaterial.SetFloat("_TimeParam", t);
                ShockMaterial.SetVector("_Center", _shockCentreUV);

                if (t >= 1f)
                    _shockTimer = -1f;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            TriggerShock();
        }
        
        
        public void TriggerShock()
        {
            _shockCentreUV = new Vector2(0.5f, 0.5f);
            _shockTimer = 0f;
        }
    }
}
