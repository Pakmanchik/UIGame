using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Back
{
    public class TileMapHandler : MonoBehaviour
    {
        [Serializable]
        private struct SettingsMap
        {
            [SerializeField] public GeneratorSettings _basicTileSettings;
            [SerializeField] public GeneratorSettings _spawnTileSettings;
        }

        [SerializeField] private Tilemap _tileMap;

        [SerializeField, Space(5)] private RuleTile _defaultTile;

        [SerializeField] private SettingsMap _settingsMap;

        [SerializeField] private int _outlineCounter = 2;

        private GeneratorSettings BasicSetting => _settingsMap._basicTileSettings;
        private GeneratorSettings SpawnSetting => _settingsMap._spawnTileSettings;

        private void Start()
        {
            GenerateTileMap();
        }

        private void GenerateTileMap()
        {
            var builderTileMap = new BuilderGeneratorMaps();

            __CreateBottomLayerMap();
            __CreateBaseMap();
            __CreateSpawnPlace();

            void __CreateBottomLayerMap()
            {
                var noiseBottomSettings = new SettingsNoise()
                {
                    scale = BasicSetting.Scale,
                    persistence = BasicSetting.Persistence,
                    lacunarity = BasicSetting.Lacunarity,
                    octaves = BasicSetting.Octaves
                };

                var size = BasicSetting.SizeAllField + _outlineCounter;

                builderTileMap
                    .AddTiles(new List<RuleTile> { _defaultTile }, _tileMap)
                    .AddSeed(BasicSetting.Seed)
                    .AddSettingsNoise(noiseBottomSettings)
                    .AddSizeField(size, 0)
                    .Build();
            }

            void __CreateBaseMap()
            {
                var noiseBasicSettings = new SettingsNoise()
                {
                    scale = BasicSetting.Scale,
                    persistence = BasicSetting.Persistence,
                    lacunarity = BasicSetting.Lacunarity,
                    octaves = BasicSetting.Octaves
                };

                builderTileMap
                    .AddTiles(BasicSetting.RuleTiles, _tileMap)
                    .AddSeed(BasicSetting.Seed)
                    .AddSettingsNoise(noiseBasicSettings)
                    .AddSizeField(BasicSetting.SizeAllField)
                    .Build();
            }

            void __CreateSpawnPlace()
            {
                var seed = SpawnSetting.Seed;

                var noiseSpawnSettings = new SettingsNoise()
                {
                    scale = SpawnSetting.Scale,
                    persistence = SpawnSetting.Persistence,
                    lacunarity = SpawnSetting.Lacunarity,
                    octaves = SpawnSetting.Octaves
                };

                var halfX = BasicSetting.SizeAllField - SpawnSetting.SizeAllField;
                var halfY = BasicSetting.SizeAllField - SpawnSetting.SizeAllField;

                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        if (x == 0 || y == 0)
                            continue;

                        var offset = new Vector2(
                            halfY,
                            halfX
                        );

                        offset.x *= x;
                        offset.y *= y;

                        seed++;

                        builderTileMap
                            .AddTiles(SpawnSetting.RuleTiles, _tileMap)
                            .AddSeed(seed)
                            .AddOffset(offset)
                            .AddSettingsNoise(noiseSpawnSettings)
                            .AddSizeField(SpawnSetting.SizeAllField)
                            .Build();
                    }
                }
            }
        }
    }
}