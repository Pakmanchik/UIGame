using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Back
{
    public sealed class BuilderGeneratorMaps
    {
        public BuilderGeneratorMaps()
        {
            ResetData();
        }
        
        private Vector2 _sizeField = new Vector2(1, 1);

        private float _scale = 1;
        private int _positionZ = 1;

        private int _octaves = 1;
        private float _persistence = 0.5f;
        private float _lacunarity = 1;

        private int _seed = 1;
        private Vector2 _offset = Vector2.zero;

        private List<RuleTile> _ruleTiles;
        private Tilemap _tilemap;

        private bool _isLoot;
        private float _lowerSearchThreshold;
        private float _upperSearchThreshold;


        private void ResetData()
        {
            _sizeField = new Vector2(1, 1);

            _scale = 1;
            _positionZ = 1;

            _octaves = 1;
            _persistence = 0.5f;
            _lacunarity = 1;

            _seed = 1;

            _offset = Vector2.zero;

            _isLoot = false;
            _lowerSearchThreshold = -2;
            _upperSearchThreshold = 2;
        }

        public BuilderGeneratorMaps AddSizeField(int size, int positionZ = 1)
        {
            if (positionZ < 0) positionZ = 1;
            _positionZ = positionZ;

            if (size < 0) size = 1;
            _sizeField = new Vector2(size, size);

            return this;
        }

        public BuilderGeneratorMaps AddSettingsNoise(SettingsNoise settingsNoise)
        {
            _scale = settingsNoise.scale;
            if (_scale <= 0) _scale = 1;

            _persistence = settingsNoise.persistence;
            if (_persistence <= 0) _persistence = 0.5f;

            _lacunarity = settingsNoise.lacunarity;
            if (_lacunarity <= 0) _lacunarity = 1;


            _octaves = settingsNoise.octaves;
            if (_octaves <= 0) _octaves = 1;

            return this;
        }

        public BuilderGeneratorMaps AddSeed(int seed = 1)
        {
            if (seed <= 0) seed = 1;
            _seed = seed;

            return this;
        }

        public BuilderGeneratorMaps AddOffset(Vector2 offset)
        {
            _offset = offset;

            return this;
        }

        public BuilderGeneratorMaps AddTiles(List<RuleTile> ruleTiles, Tilemap tilemap)
        {
            _ruleTiles = ruleTiles;
            _tilemap = tilemap;

            return this;
        }

        public BuilderGeneratorMaps AddRuleIsLoot(float lowerSearchThreshold, float upperSearchThreshold)
        {
            if (lowerSearchThreshold > upperSearchThreshold)
            {
                (lowerSearchThreshold, upperSearchThreshold) = (upperSearchThreshold, lowerSearchThreshold);
            }

            _lowerSearchThreshold = lowerSearchThreshold;
            _upperSearchThreshold = upperSearchThreshold;

            _isLoot = true;
            
            return this;
        }

        public void Build()
        {
            var settingsMap = new SettingsTileMap
            {
                sizeField = _sizeField,
                offset = _offset,
                ruleTiles = _ruleTiles,
                tilemap = _tilemap,
                positionZ = _positionZ,
                isLoot = _isLoot,
                lowerSearchThreshold = _lowerSearchThreshold,
                upperSearchThreshold = _upperSearchThreshold,
                seed = _seed
            };
            var settingNoise = new SettingsNoise()
            {
                sizeField = _sizeField,
                octaves = _octaves,
                persistence = _persistence,
                seed = _seed,
                lacunarity = _lacunarity,
                scale = _scale
            };

            var unused = new GeneratorChunk(settingsMap, settingNoise);
            
            ResetData();
        }
    }

    public struct SettingsTileMap
    {
        public Vector2 sizeField;
        public int positionZ;
        public Vector2 offset;
        public List<RuleTile> ruleTiles;
        public Tilemap tilemap;
        public bool isLoot;
        public float lowerSearchThreshold;
        public float upperSearchThreshold;
        public int seed;
    }
}