using UnityEngine;
using UnityEngine.Serialization;

namespace ShockEffect
{
    public class ShockOnCollision : MonoBehaviour
    {
        public Material ShockMaterial; 
        public float ShockDuration = 1f;

        private float _shockTimer = -1f;
        private Vector2 _shockCenterUV = new Vector2(0.5f, 0.5f);
        private Mesh _mesh;
        private MeshCollider _meshCollider;

        void Start()
        {
            _meshCollider = GetComponent<MeshCollider>();
            _mesh = GetComponent<MeshFilter>().mesh;
        }
        
        void Update()
        {
            if (_shockTimer >= 0f)
            {
                _shockTimer += Time.deltaTime;
                float t = _shockTimer / ShockDuration;

                ShockMaterial.SetFloat("_TimeParam", t);
                ShockMaterial.SetVector("_Center", _shockCenterUV);

                if (t >= 1f)
                    _shockTimer = -1f;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 point = collision.contacts[0].point;
            Vector2 uv = WorldToMeshUV(point);
            Debug.Log($"Converted UV: {uv}");
            TriggerShock(uv);
        }
        
        
        public void TriggerShock(Vector2 uv)
        {
            _shockCenterUV = uv;
            _shockTimer = 0f;
        }

        private Vector2 WorldToMeshUV(Vector3 worldPoint)
        {
            Ray ray = new Ray(worldPoint + Vector3.up * 0.01f, Vector3.down);
            RaycastHit hit;

            if (_meshCollider.Raycast(ray, out hit, 1f))
            {
                return hit.textureCoord;
            }

            // fallback
            Debug.Log("WorldToMeshUV falhou. A usar centro padrão.");
            return new Vector2(0.5f, 0.5f);
        }
    }
}
