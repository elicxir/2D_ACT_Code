using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_sprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}