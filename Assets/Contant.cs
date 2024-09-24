using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{
    public string name;
    public string phone;
    public int age;
}

[Serializable]
public class ProductInfo
{
    public string name;
    public string description;
    public int price;
}

public class Contant : MonoBehaviour
{
    public List<UserInfo> allUser = new List<UserInfo>();
    //public List<ProductInfo> allProduct = new List<ProductInfo>();

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            allUser = CSV.Get().Parse<UserInfo>("UserInfo");
            //allProduct = CSV.Get().Parse<ProductInfo>("ªÛ«∞CSV");
        }
    }
}
