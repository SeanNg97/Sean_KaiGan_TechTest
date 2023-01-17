using UnityEngine;

namespace KaiganTest.Test4
{
    public class Smoldering : MonoBehaviour
    {
        [Header("Check this to test")]
        [SerializeField] private bool isStartSmoldering;

        [Header("Settings")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float maxSmoderingValue;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float minSmoderingValue;
 
        [SerializeField] private Material smoderingMaterial;
        [SerializeField] private Renderer thisRenderer;
        private float smoderingValue = 1;
        private Material _smoderingMaterial;
        private const string heatNoise = "_HeatNoise";

        void Start()
        {
            //Assign an instance material to the renderer, avoid change the value of other object with same material
            Material newMaterial = new Material(smoderingMaterial);
            thisRenderer.material = newMaterial;
            _smoderingMaterial = thisRenderer.material;

            //Initialize
            smoderingValue = maxSmoderingValue;
        }

        // Update is called once per frame
        void Update()
        {
            if (_smoderingMaterial == null) { return; }

            if (isStartSmoldering)
            {
                if (smoderingValue <= maxSmoderingValue)
                {
                    smoderingValue += Time.deltaTime;
                    _smoderingMaterial.SetFloat(heatNoise, smoderingValue);
                }
            }
            else
            {
                if (smoderingValue >= minSmoderingValue)
                {
                    smoderingValue -= Time.deltaTime;
                    _smoderingMaterial.SetFloat(heatNoise, smoderingValue);
                }
            }
        }
    }
}

