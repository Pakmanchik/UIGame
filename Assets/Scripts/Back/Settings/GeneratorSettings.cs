using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Back
{
    [CreateAssetMenu(fileName = "New Map Settings", menuName = "Map Settings", order = 51)]
    public class GeneratorSettings : ScriptableObject
    {
        [SerializeField] private int _sizeAllField;
        [SerializeField, Space(10)] private int _positionZ = 1;
        [SerializeField, Space(10)] private int _outline = 0;

        [SerializeField, Space(10)] private float _scale;
        [SerializeField] private int _octaves;
        [SerializeField, Range(0.1f, 1)] private float _persistence;
        [SerializeField] private float _lacunarity;
        [SerializeField, Space(10)] private int _seed;

        [SerializeField, Space(10)] private List<RuleTile> _ruleTiles;

        public int SizeAllField => _sizeAllField + _outline;
        public float Scale => _scale;
        public int Octaves => _octaves;
        public float Persistence => _persistence;
        public float Lacunarity => _lacunarity;
        public int Seed => _seed;
        public List<RuleTile> RuleTiles => _ruleTiles;
        
        public int PositionZ => _positionZ;

        private void OnValidate()
        {
            if (_positionZ < 0)
                _positionZ = 0;
            
            if (_sizeAllField <= 0)
                _sizeAllField = 1;

            if (_scale <= 0)
                _scale = 1;

            if (_octaves <= 0)
                _octaves = 1;

            if (_lacunarity <= 0)
                _lacunarity = 1;

            if (_seed <= 0)
                _seed = 1;
            
            if(_ruleTiles.Count <= 0 )
                Debug.LogError("List<RuleTile> is NULL");
        }
    }
}