using Back;
using UnityEngine;

public  class NoiseMapGenerator
{
    public NoiseMapGenerator(SettingsNoise settingsNoise)
    {
        _octaves = settingsNoise.octaves;
        _persistence = settingsNoise.persistence;
        _lacunarity = settingsNoise.lacunarity;
        _sizeField = settingsNoise.sizeField;
        _seed = settingsNoise.seed;
        _scale = settingsNoise.scale;
    }
    
    private readonly int _octaves;
    private readonly float _persistence;
    private readonly float _lacunarity;
    private readonly Vector2 _sizeField;
    private readonly int _seed;
    private readonly float _scale;
    
    public  float[] GenerateNoiseMap()
    {
        // NOTE: Код генератора позаимствован с сайта https://ru.kihontekina.dev/posts/tile_maps_part_two/
        
        // Массив данных о вершинах, одномерный вид поможет избавиться от лишних циклов впоследствии
        var noiseMap = new float[(int)_sizeField.x * (int)_sizeField.y];

        // Порождающий элемент
        var rand = new System.Random(_seed);

        // Сдвиг октав, чтобы при наложении друг на друга получить более интересную картинку
        var octavesOffset = new Vector2[_octaves];
        for (var i = 0; i < _octaves; i++)
        {
            float xOffset = rand.Next(-100000, 100000);
            float yOffset = rand.Next(-100000, 100000);
            octavesOffset[i] = new Vector2(xOffset / _sizeField.x, yOffset / _sizeField.y);
        }

        // Учитываем половину ширины и высоты, для более визуально приятного изменения масштаба
        var halfWidth = _sizeField.x / 2f;
        var halfHeight = _sizeField.y / 2f;

        // Генерируем точки на карте высот
        for (var y = 0; y < _sizeField.y; y++)
        {
            for (var x = 0; x < _sizeField.x; x++)
            {
                // Задаём значения для первой октавы
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                float superpositionCompensation = 0;

                // Обработка наложения октав
                for (var i = 0; i < _octaves; i++)
                {
                    // Рассчитываем координаты для получения значения из Шума Перлина
                    var xResult = (x - halfWidth) / _scale * frequency + octavesOffset[i].x * frequency;
                    var yResult = (y - halfHeight) / _scale * frequency + octavesOffset[i].y * frequency;

                    // Получение высоты из ГПСЧ
                    var generatedValue = Mathf.PerlinNoise(xResult, yResult);
                    // Наложение октав
                    noiseHeight += generatedValue * amplitude;
                    // Компенсируем наложение октав, чтобы остаться в границах диапазона [0,1]
                    noiseHeight -= superpositionCompensation;

                    // Расчёт амплитуды, частоты и компенсации для следующей октавы
                    amplitude *= _persistence;
                    frequency *= _lacunarity;
                    superpositionCompensation = amplitude / 2;
                }

                // Сохраняем точку для карты высот
                // Из-за наложения октав есть вероятность выхода за границы диапазона [0,1]
                noiseMap[y * (int)_sizeField.x + x] = Mathf.Clamp01(noiseHeight);
            }
        }
        return noiseMap;
    }
}