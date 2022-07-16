using MergeMechanic.Core;
using UnityEditor;
using UnityEngine;

namespace MergeMechanic.Editor
{
    public class BoardGeneratorWindow : EditorWindow
    {
        private int Width = 3;
        private string BasePrefabPath = "Assets/Prefabs/Base.prefab";

        private GameObject _tile;
        private Vector3 _spriteSize;

        [MenuItem("Level Editor/Board/Board Generator", false, 1)]
        private static void Init()
        {
            GetWindow(typeof(BoardGeneratorWindow));
        }

        public void Awake()
        {
            _tile = AssetDatabase.LoadAssetAtPath<GameObject>(BasePrefabPath);
            _spriteSize = _tile.GetComponent<SpriteRenderer>().bounds.size;
        }

        public void OnGUI()
        {
            Width = EditorGUILayout.IntSlider(
                "Width",
                Width,
                0,
                9);

            BasePrefabPath = EditorGUILayout.TextField("Base Tile Prefab Path", BasePrefabPath);

            if (GUILayout.Button("Generate Board"))
            {
                GenerateBoard();
                Close();
            }
        }

        private void GenerateBoard()
        {
            var board = new GameObject("Board");

            BoardGenerator.Instance.CreateBoard(
                Width,
                _spriteSize,
                board.transform,
                _tile);
        }
    }
}
