using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootDrawer : MonoBehaviour
{
    [SerializeField] Boot boot;
    [SerializeField] DrawPanel panel;
    Texture2D panel_texture;
    void Start()
    {
        panel_texture = (Texture2D)panel.gameObject.GetComponent<Image>().mainTexture;
        
    }
    List<Vector3> boot_points = new List<Vector3>();
    bool is_pressed = false;
    const float point_distance = 10;
    void Update()
    {
        if (is_pressed)
            PanelMouseMove();
        
    }
    
    public void PanelMouseUp()
    {
        is_pressed = false;
        ConfirmBoot();
    }
    void ConfirmBoot()
    {
        Vector3 offset = boot_points[0];
        for (int i = 0; i < boot_points.Count; i++)
        {
            boot_points[i] -= offset;
            //Debug.Log(boot_points[i]);
        }
        boot.GetComponent<Boot>().CreateMesh(boot_points);
    }
    public void PanelMouseDown()
    {
        boot_points.Clear();
        is_pressed = true;
        boot_points.Add(Input.mousePosition);
    }
    public void PanelMouseMove()
    {
        if (Mathf.Abs((new Vector3(Input.mousePosition.x, Input.mousePosition.y) - boot_points[boot_points.Count - 1]).magnitude) >= point_distance)
        {
            //DrawLine(boot_points[boot_points.Count - 1], Input.mousePosition);
            boot_points.Add(Input.mousePosition);
        }
        
    }
    void DrawLine(Vector3 a, Vector3 b)
    {
        Vector2 forv = (a - b).normalized;

        //panel_texture = (Texture2D)panel.GetComponent<Image>().image;
        for (int i = 1; i < 10; i++)
            panel_texture.SetPixel((int)forv.x * i, (int)forv.y * i, Color.black, 3);
        
    }
}
