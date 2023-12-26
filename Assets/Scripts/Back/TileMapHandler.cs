using System;
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
            [SerializeField] public GeneratorSettings _bottomTileSettings;
            [SerializeField] public LootSettings _lootSettings;
        }

        [SerializeField] private Tilemap _tileMap;

        [SerializeField, Space(5)] private RuleTile _defaultTile;

        [SerializeField] private SettingsMap _settingsMap;
        
        private GeneratorSettings BasicSetting => _settingsMap._basicTileSettings;
        private GeneratorSettings SpawnSetting => _settingsMap._spawnTileSettings;
        private GeneratorSettings BottomMapSetting => _settingsMap._bottomTileSettings;
        private LootSettings LootSettings => _settingsMap._lootSettings;

        private void OnValidate()
        {
            if (BasicSetting == null)
                Debug.LogError("Basic Setting is NULL");

            if (SpawnSetting == null)
                Debug.LogError("Spawn Setting is NULL");
            
            if (BottomMapSetting == null)
                Debug.LogError("Bottom Map Setting is NULL");

            if (_tileMap == null)
                Debug.LogError("TileMap is NULL");

            if (_defaultTile == null)
                Debug.LogError("Default Tile is NULL");
        }

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
            __CreateLoot();

            void __CreateBottomLayerMap()
            {
                var noiseBottomSettings = new SettingsNoise()
                {
                    scale = BottomMapSetting.Scale,
                    persistence = BottomMapSetting.Persistence,
                    lacunarity = BottomMapSetting.Lacunarity,
                    octaves = BottomMapSetting.Octaves,
                    seed = BottomMapSetting.Seed
                };
                

                builderTileMap
                    .AddTiles(BottomMapSetting.RuleTiles, _tileMap)
                    .AddSeed(BottomMapSetting.Seed)
                    .AddSettingsNoise(noiseBottomSettings)
                    .AddSizeField(BottomMapSetting.SizeAllField, BottomMapSetting.PositionZ)
                    .Build();
            }

            void __CreateBaseMap()
            {
                var noiseBasicSettings = new SettingsNoise()
                {
                    scale = BasicSetting.Scale,
                    persistence = BasicSetting.Persistence,
                    lacunarity = BasicSetting.Lacunarity,
                    octaves = BasicSetting.Octaves,
                    seed = BasicSetting.Seed
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
                    octaves = SpawnSetting.Octaves,
                    seed = SpawnSetting.Seed
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

            void __CreateLoot()
            {
                var noiseLootSettings = new SettingsNoise()
                {
                    scale = LootSettings.Scale,
                    persistence = LootSettings.Persistence,
                    lacunarity = LootSettings.Lacunarity,
                    octaves = LootSettings.Octaves,
                    seed = LootSettings.Seed
                };
                
                builderTileMap
                    .AddTiles(LootSettings.RuleTiles, _tileMap)
                    .AddSeed(LootSettings.Seed)
                    .AddSettingsNoise(noiseLootSettings)
                    .AddSizeField(LootSettings.SizeAllField, LootSettings.PositionZ)
                    .AddRuleIsLoot(LootSettings.LowerSearchThreshold,LootSettings.UpperSearchThreshold)
                    .Build();
            }
        }
    }
}