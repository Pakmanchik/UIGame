using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;

namespace Back
{
    public class GeneratorChunk
    {
        public GeneratorChunk(SettingsTileMap settingsTileMap, SettingsNoise settingsNoise)
        {
            _sizeField = settingsTileMap.sizeField;
            _offset = settingsTileMap.offset;
            _ruleTiles = settingsTileMap.ruleTiles;
            _tilemap = settingsTileMap.tilemap;
            _positionZ = settingsTileMap.positionZ;

            _isLoot = settingsTileMap.isLoot;
            _upperThreshold = settingsTileMap.upperSearchThreshold;
            _lowerThreshold = settingsTileMap.lowerSearchThreshold;
            
            _seed = settingsTileMap.seed;

            _noiseMapGenerator = new NoiseMapGenerator(settingsNoise);

            GenerateChunk();
        }

        private readonly int _positionZ;

        private readonly Vector2 _sizeField;
        private readonly Vector2 _offset;

        private readonly List<RuleTile> _ruleTiles;

        private readonly Tilemap _tilemap;

        private readonly NoiseMapGenerator _noiseMapGenerator;

        private readonly bool _isLoot;
        private readonly float _upperThreshold;
        private readonly float _lowerThreshold;

        private readonly int _seed;

        private void GenerateChunk()
        {
            var noiseMap = _noiseMapGenerator.GenerateNoiseMap();

            var sizeX = (int)_sizeField.x;
            var sizeY = (int)_sizeField.y;

            var offsetX = (int)_offset.x;
            var offsetY = (int)_offset.y;

            var random = new Random((uint)_seed);

            for (var y = 0; y < sizeX; y++)
            {
                for (var x = 0; x < sizeX; x++)
                {
                    var noiseHeight = noiseMap[(sizeX * y + x)];

                    if (!(noiseHeight > _lowerThreshold) || !(noiseHeight < _upperThreshold)) 
                        continue;
                    
                    var colorHeight = noiseHeight * _ruleTiles.Count;

                    var colorIndex = Mathf.FloorToInt(colorHeight);

                    if (colorIndex == _ruleTiles.Count)
                    {
                        colorIndex = _ruleTiles.Count - 1;
                    }

                    var tile = _ruleTiles[colorIndex];

                    var position = new Vector3Int(
                        x - (sizeX + offsetX) / 2,
                        y - (sizeY + offsetY) / 2,
                        _positionZ
                    );

                    if (_isLoot)
                    {
                        var numberTile = random.NextInt(0, _ruleTiles.Count);
                        tile = _ruleTiles[numberTile];
                    }

                    _tilemap.SetTile(position, tile);
                }
            }
        }
    }
}