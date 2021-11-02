using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boot : MonoBehaviour
{
    Mesh mesh;
    public Mesh Mesh
    {
        get
        {
            return mesh;
        }
    }
    public void CreateMesh(List<Vector3> vertices)
    {
        
        mesh.Clear();
        Vector3 one_boung_vector = new Vector3();
        Vector3 buff = new Vector3();

        List<Vector3> done_vertices = new List<Vector3>();
        List<Vector3> done_normals = new List<Vector3>();
        List<int> triangles = new List<int>();

        void AddVerticesToPoint(Vector3 vert_1, Vector3 vert_2)
        {
            one_boung_vector = vert_2 - vert_1;
            //buff = Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * one_boung_vector.normalized * 5 + vert_1;
            buff = vert_1;
            done_vertices.Add(buff);

            //buff = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * one_boung_vector.normalized * 5 + vert_1;
            buff = vert_1;
            done_vertices.Add(buff);

            done_normals.Add(new Vector3(0, 0, -1));
            done_normals.Add(new Vector3(0, 0, -1));
        }
        void RotateCouple(Vector3 direct_start, Vector3 direct_end, int index_first, float second_angle)
        {
            one_boung_vector = (direct_end - direct_start).normalized * 5;
            done_vertices[index_first + 1] = Quaternion.AngleAxis(90 + second_angle / 2, new Vector3(0, 0, 1)) * one_boung_vector + direct_start;
            done_vertices[index_first] = Quaternion.AngleAxis(-90 + second_angle / 2, new Vector3(0, 0, 1)) * one_boung_vector + direct_start;
        }

        AddVerticesToPoint(vertices[0], vertices[1]);

        for (int i = 2; i < vertices.Count; i++)
        {
            AddVerticesToPoint(vertices[i - 1], vertices[i]);
            float angle = Vector3.SignedAngle(vertices[i] - vertices[i - 1], vertices[i - 1] - vertices[i - 2], new Vector3(0, 0, 1));

            RotateCouple(vertices[i - 1], vertices[i], done_vertices.Count - 2, angle);
            
            for (int j = 1; j <= 3; j++)
                triangles.Add(done_vertices.Count - j);
            for (int j = 4; j >= 2; j--)
                triangles.Add(done_vertices.Count - j);
        }
        RotateCouple(vertices[vertices.Count - 2], vertices[vertices.Count - 1], done_vertices.Count - 2, 0);
        RotateCouple(vertices[0], vertices[1], 0, 0);
        int vert_count = done_vertices.Count;
        for (int i = 0; i < vert_count; i++)
        {
            done_vertices.Add(done_vertices[i]);
            done_vertices[i] = new Vector3(done_vertices[i].x, done_vertices[i].y, -2);
            done_vertices[done_vertices.Count - 1] = new Vector3(done_vertices[done_vertices.Count - 1].x, done_vertices[done_vertices.Count - 1].y, 2);
            done_normals.Add(new Vector3(0, 0, 1));
        }
        for (int i = (done_vertices.Count / 2) + 4; i <= done_vertices.Count; i+=2)
        {
            for (int j = 3; j >= 1; j--)
                triangles.Add(i - j);
            for (int j = 2; j <= 4; j++)
                triangles.Add(i - j);
        }

        mesh.vertices = done_vertices.ToArray();
        mesh.SetTriangles(triangles.ToArray(), 0);
        mesh.SetNormals(done_normals);
        mesh.SetTriangles(triangles.ToArray(), 0);
    }

    //private void OnDrawGizmos()
    //{
    //    if (mesh == null || mesh.vertexCount == 0)
    //        return;

    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < mesh.vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(mesh.vertices[i], 0.5f);
    //    }
    //}
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
