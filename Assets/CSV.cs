using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using UnityEngine;

public class CSV : MonoBehaviour
{
    static CSV instance;

    public static CSV Get()
    {
        if(instance == null)
        {
            GameObject go = new GameObject();
            go.name = nameof(CSV);
            go.AddComponent<CSV>();
            print("1111");
        }
        return instance;
    }

    private void Awake()
    {
        print("2222");
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public List<UserInfo> Parse(string fileName)
    {
        // fileName �� �ش�Ǵ� file �� �о����.
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; 
        print(path);

        string stringData = File.ReadAllText(path);
        print(stringData);

        // ���͸� �������� ���� ���� �ڸ���.
        // "\n" �� �������� �ڸ���.
        string [] lines = stringData.Split("\n");
        // "\r" �� �������� �ڸ���.
        for(int i = 0; i < lines.Length; i++)
        {
            string [] temp = lines[i].Split("\r");
            lines[i] = temp[0];
            print(lines[i]);
        }

        // , �� �������� lines �� ù��° ���� ������. (����)
        string[] variables = lines[0].Split(",");

        // ��ü UserInfo �� ������ �ִ� List
        List<UserInfo> list = new List<UserInfo>();

        // , �� �������� ������ ������ ������
        for(int i = 1; i < lines.Length; i++)
        {
            string[] value = lines[i].Split(",");
            // �߶��� �����͸� UserInfo �� ����
            UserInfo info = new UserInfo();
            info.name = value[0];
            info.phone = value[1];
            info.age = int.Parse(value[2]);

            // list �� ������ �߰�
            list.Add(info);
        }
        return list;
    }


    public List<T> Parse<T>(string fileName) where T : new()
    {
        // fileName �� �ش�Ǵ� file �� �о����.
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        print(path);

        string stringData = File.ReadAllText(path);
        print(stringData);

        // ���͸� �������� ���� ���� �ڸ���.
        // "\n" �� �������� �ڸ���.
        string[] lines = stringData.Split("\n");
        // "\r" �� �������� �ڸ���.
        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
            print(lines[i]);
        }

        // , �� �������� lines �� ù��° ���� ������. (����)
        string[] variables = lines[0].Split(",");

        // ��ü T �� ������ �ִ� List
        List<T> list = new List<T>();

        // , �� �������� ������ ������ ������
        for (int i = 1; i < lines.Length; i++)
        {
            string[] value = lines[i].Split(",");

            // �߶��� �����͸� ���� T ���� ������.
            T info = new T();

            for(int j = 0; j < variables.Length; j++)
            {
                // T �� �ִ� �������� ������ ��������.
                FieldInfo fieldInfo = typeof(T).GetField(variables[j]);
                // int.Parse, float.Parse �� ����� �� �� �ִ� ���� ��������.
                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                // value[i] ����  typeConverter �� �̿��ؼ� ������ ����
                fieldInfo.SetValue(info, typeConverter.ConvertFrom(value[j]));
            }

            list.Add(info);
        }

        return list;
    }
}
