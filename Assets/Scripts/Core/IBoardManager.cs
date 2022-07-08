﻿using System;
using UnityEngine;

namespace MergeMechanic.Core
{
    public interface IBoardManager
    {
        void CreateBoard(
            int width,
            int height,
            Vector3 tileSize,
            Transform parentTransform,
            GameObject cell,
            Func<GameObject, ITile> getTileFunc);

        void PopulateTile(GameObject gameObjectToGenerate);
    }
}
