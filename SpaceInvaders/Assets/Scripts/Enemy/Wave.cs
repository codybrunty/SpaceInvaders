using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave{
    public int rows;
    public int enemiesInRow;
    public List<float> enemySpeed = new List<float> { 0f,0f,0f,0f,0f};
}
