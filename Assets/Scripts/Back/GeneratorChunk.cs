using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

            _noiseMapGenerator = new NoiseMapGenerator(settingsNoise);

            GenerateChunk();
        }

        private readonly int _positionZ;

        private readonly Vector2 _sizeField;
        private readonly Vector2 _offset;

        private readonly List<RuleTile> _ruleTiles;

        private readonly Tilemap _tilemap;

        private readonly NoiseMapGenerator _noiseMapGenerator;
        
        private void GenerateChunk()
        {
            var noiseMap = _noiseMapGenerator.GenerateNoiseMap();

            var sizeX = (int)_sizeField.x;
            var sizeY = (int)_sizeField.y;

            var sizeOffsetX = (int)_offset.x;
            var sizeOffsetY = (int)_offset.y;

            for (var y = 0; y < sizeX; y++)
            {
                for (var x = 0; x < sizeX; x++)
                {
                    var noiseHeight = noiseMap[(sizeX * y + x)];

                    var colorHeight = noiseHeight * _ruleTiles.Count;

                    var colorIndex = Mathf.FloorToInt(colorHeight);

                    if (colorIndex == _ruleTiles.Count)
                    {
                        colorIndex = _ruleTiles.Count - 1;
                    }

                    var tile = _ruleTiles[colorIndex];

                    var position = new Vector3Int(
                        x - (sizeX + sizeOffsetX) / 2,
                        y - (sizeY + sizeOffsetY) / 2,
                        _positionZ
                    );

                    _tilemap.SetTile(position, tile);
                }
            }
        }
    }
}