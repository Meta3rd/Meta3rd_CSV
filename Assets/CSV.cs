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
        // fileName 에 해당되는 file 을 읽어오자.
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; 
        print(path);

        string stringData = File.ReadAllText(path);
        print(stringData);

        // 엔터를 기준으로 한줄 한줄 자르자.
        // "\n" 을 기준으로 자르자.
        string [] lines = stringData.Split("\n");
        // "\r" 을 기준으로 자르자.
        for(int i = 0; i < lines.Length; i++)
        {
            string [] temp = lines[i].Split("\r");
            lines[i] = temp[0];
            print(lines[i]);
        }

        // , 를 기준으로 lines 의 첫번째 값을 나누자. (변수)
        string[] variables = lines[0].Split(",");

        // 전체 UserInfo 를 가지고 있는 List
        List<UserInfo> list = new List<UserInfo>();

        // , 를 기준으로 나머지 값들을 나누자
        for(int i = 1; i < lines.Length; i++)
        {
            string[] value = lines[i].Split(",");
            // 잘라진 데이터를 UserInfo 에 셋팅
            UserInfo info = new UserInfo();
            info.name = value[0];
            info.phone = value[1];
            info.age = int.Parse(value[2]);

            // list 에 정보를 추가
            list.Add(info);
        }
        return list;
    }


    public List<T> Parse<T>(string fileName) where T : new()
    {
        // fileName 에 해당되는 file 을 읽어오자.
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        print(path);

        string stringData = File.ReadAllText(path);
        print(stringData);

        // 엔터를 기준으로 한줄 한줄 자르자.
        // "\n" 을 기준으로 자르자.
        string[] lines = stringData.Split("\n");
        // "\r" 을 기준으로 자르자.
        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
            print(lines[i]);
        }

        // , 를 기준으로 lines 의 첫번째 값을 나누자. (변수)
        string[] variables = lines[0].Split(",");

        // 전체 T 를 가지고 있는 List
        List<T> list = new List<T>();

        // , 를 기준으로 나머지 값들을 나누자
        for (int i = 1; i < lines.Length; i++)
        {
            string[] value = lines[i].Split(",");

            // 잘라진 데이터를 담을 T 변수 만들자.
            T info = new T();

            for(int j = 0; j < variables.Length; j++)
            {
                // T 에 있는 변수들의 정보를 가져오자.
                FieldInfo fieldInfo = typeof(T).GetField(variables[j]);
                // int.Parse, float.Parse 의 기능을 할 수 있는 변수 가져오자.
                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                // value[i] 값을  typeConverter 를 이용해서 변수에 셋팅
                fieldInfo.SetValue(info, typeConverter.ConvertFrom(value[j]));
            }

            list.Add(info);
        }

        return list;
    }
}
