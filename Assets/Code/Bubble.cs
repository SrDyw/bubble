using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Item
{
    [SerializeField] private BubbleType _type;

    public BubbleType BubbleType { get => _type; set => _type = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override string ToString()
    {
        return $"@Bubble {_type.ToString()} ID {IdInInventory}";
    }
}
