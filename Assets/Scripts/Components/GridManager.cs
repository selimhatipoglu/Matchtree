using System;
using System.Collections.Generic;
using Events;
using Extensions.System;
using Extensions.Unity;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Components
{
    public class GridManager : SerializedMonoBehaviour
    {
        [Inject] private InputEvents InputEvents { get; set; }
        [BoxGroup(Order = 999)][TableMatrix(SquareCells = true) /*
        (DrawElementMethod = nameof(DrawTile))*/, OdinSerialize]
        private Tile[,] _grid;
        [SerializeField] private List<GameObject> _tilePrefabs;
        private int _gridSizeX;
        private int _gridSizeY;
        
        private Vector3 _mouseUpPos;
        private Tile _selectedTile;
        private Vector3 _mouseDownPos;
        
        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private Tile DrawTile(Rect rect, Tile tile)
        {
            UnityEditor.EditorGUI.DrawRect(rect, Color.blue);
            return tile;
        }

        [Button]
        private void CreateGrid(int sizeX, int sizeY)
        {
            List<int> prefabIds = new();

            for (int id = 0; id < _tilePrefabs.Count; id++) prefabIds.Add(id);
            
            
            _gridSizeX = sizeX;
            _gridSizeY = sizeY;

            if (_grid != null)
            {
                foreach (Tile o in _grid)
                {
                    DestroyImmediate(o.gameObject);
                }
            }

            _grid = new Tile[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            for (int y = 0; y < _gridSizeY; y++)
            {
                List<int> spawnableIds = new(prefabIds);
                
                Vector2Int coord = new Vector2Int(x, _gridSizeY - y - 1);
                Vector3 pos = new Vector3(coord.x, coord.y, 0f);
                
                _grid.GetSpawnableColors(coord, spawnableIds);

               int randomId = spawnableIds.Random();
                
                GameObject tilePrefabRandom = _tilePrefabs[randomId];
                GameObject tileNew = Instantiate(tilePrefabRandom, pos, 
                    Quaternion.identity);
                
                Tile tile = tileNew.GetComponent<Tile>();
                tile.Construct(coord);
                
                _grid[coord.x, coord.y] = tile;
                
            }
        }

        private void RegisterEvents()
        {
            InputEvents.MouseDownGrid += OnMouseDownGrid;
            InputEvents.MouseUpGrid += OnMouseUpGrid;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnMouseDownGrid(Tile arg0, Vector3 arg1)
        {
            _selectedTile = arg0;
            _mouseDownPos = arg1;
            EDebug.Method();

        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnMouseUpGrid(Vector3 arg0)
        {
            _mouseUpPos = arg0;

            if(_selectedTile)
            {
                EDebug.Method();

                Debug.DrawLine(_mouseDownPos, _mouseUpPos, Color.blue, 2f);
            }
        }

        private void UnRegisterEvents()
        {
            InputEvents.MouseDownGrid -= OnMouseDownGrid;
            InputEvents.MouseUpGrid -= OnMouseUpGrid;
        }

    }
}
