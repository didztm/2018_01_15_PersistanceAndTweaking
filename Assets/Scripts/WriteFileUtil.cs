using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteFileUtil {


    private class ListWrapper<T>
    {
        public List<T> _list;
    }

    public static void ListToJson<T>(string path, List<T> _list)
    {
        string json = "";
        ListWrapper<T> w = new ListWrapper<T>
        {
            _list = _list
        };
        json = JsonUtility.ToJson(w);
        StreamWriter file = new StreamWriter(path);
        file.Write(json);
        file.Close();
    }

    public static List<T> ListFromJson<T>(string path)
    {
        string text = System.IO.File.ReadAllText(path);
        List<T> _myList = JsonUtility.FromJson<ListWrapper<T>>(text)._list;
        Debug.Log(_myList.Count);
        return _myList;
    }

}
