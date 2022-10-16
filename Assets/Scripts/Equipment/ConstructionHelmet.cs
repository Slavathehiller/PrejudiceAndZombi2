using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionHelmet : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Строительная каска";
        prefab = prefabsController.constructionHelmet;
        Description = "Пластиковая строительная каска. Не лучшее чем можно защитить голову, но иногда выбирать не приходится, правда?";
    }
}
