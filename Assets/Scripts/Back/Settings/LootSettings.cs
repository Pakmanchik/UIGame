using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Back
{
    [CreateAssetMenu(fileName = "New Loot Settings", menuName = "Loot Settings", order = 52)]
    public class LootSettings : GeneratorSettings
    {
        [SerializeField,Range(0,0.86f)] private float _lowerSearchThreshold;
        [SerializeField,Range(0,0.86f)] private float _upperSearchThreshold;
        
        public float LowerSearchThreshold => _lowerSearchThreshold;
        public float UpperSearchThreshold => _upperSearchThreshold;

        private void OnValidate()
        {
            if (_lowerSearchThreshold < 0)
                _lowerSearchThreshold = 0;
            
            if (_upperSearchThreshold < 0)
                _upperSearchThreshold = 0;
        }
    }
}