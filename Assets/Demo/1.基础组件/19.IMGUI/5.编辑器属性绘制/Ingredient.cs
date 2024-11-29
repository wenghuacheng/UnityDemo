using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

    [Serializable]
    public class Ingredient
    {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }
}