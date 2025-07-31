using UnityEngine;
using UnityEngine.Tilemaps;

public class transparentWalls : MonoBehaviour
{
    Tilemap tilemap;
    float hiddenOpacity = 0.3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Color color = tilemap.color;
            color.a = hiddenOpacity;
            tilemap.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Color color = tilemap.color;
            color.a = 1;
            tilemap.color = color;
        }
    }
}
