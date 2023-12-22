using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Back
{
    public sealed class BuilderGeneratorMaps
    {
        private Vector2 _sizeField = new Vector2(1,1);

        private float _scale = 1;
        private int _positionZ = 1;

        private int _octaves = 1;
        private float _persistence = 0.5f;
        private float _lacunarity = 1;

        private int _seed = 1;
        private Vector2 _offset = Vector2.zero;

        private List<RuleTile> _ruleTiles;
        private Tilemap _tilemap;

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

        public void Build()
        {
            var settingsMap = new SettingsTileMap
            {
                sizeField = _sizeField,
                offset = _offset,
                ruleTiles = _ruleTiles,
                tilemap = _tilemap,
                positionZ = _positionZ
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
        }
    }

    public struct SettingsTileMap
    {
        public Vector2 sizeField;
        public int positionZ;
        public Vector2 offset;
        public List<RuleTile> ruleTiles;
        public Tilemap tilemap;
    }
}