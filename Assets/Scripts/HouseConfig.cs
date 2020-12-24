using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseConfig : MonoBehaviour
{

    public static Material material;
    public static bool materialChanged = false;
    List<Material> materials;


    void Start()
    {
        materials = new List<Material>();
        materials.Add(Resources.Load("Materials/WallMat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/1Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/2Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/3Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/4Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/5Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/6Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/7Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/8Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/9Mat", typeof(Material)) as Material);
        materials.Add(Resources.Load("Materials/10Mat", typeof(Material)) as Material);

        //Invoke("m1", 1.0f);
    }


    public void SetMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }


    public void ChangeMaterial(Material mat)
    {
        material = mat;
        materialChanged = true;
    }


    public void m1()
    {
        //gameObject.GetComponent<MeshRenderer>().material = newMat1;
        //Renderer r = GetComponent<Renderer>();
        //Material[] mats = r.materials;  // copy of materials array.
        //mats[2] = newMat1; // set new material
        //r.materials = mats; // assign updated array to materials array

        //Invoke("m2", 1.0f);
    }

    void m2()
    {
        //gameObject.GetComponent<MeshRenderer>().material = newMat2;
        //Renderer r = GetComponent<Renderer>();
        //Material[] mats = r.materials;  // copy of materials array.
        //mats[2] = newMat2; // set new material
        //r.materials = mats; // assign updated array to materials array

        //Invoke("m1", 1.0f);
    }

}
