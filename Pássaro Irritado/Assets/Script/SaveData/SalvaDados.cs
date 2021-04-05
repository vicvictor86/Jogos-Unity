using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SalvaDados : MonoBehaviour
{

    public Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Salvar();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Carregar();
        }
    }

    void Salvar()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/posData.data");

        SaveClass s = new SaveClass();
        s.posx = transform.position.x;

        bf.Serialize(fs, s);
        fs.Close();
    }

    void Carregar()
    {
        if(File.Exists(Application.persistentDataPath + "/posData.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/posData.data", FileMode.Open);

            SaveClass s = (SaveClass)bf.Deserialize(fs);
            fs.Close();

            temp.x = s.posx;
            transform.position = temp;
        }
    }

}

[Serializable]
class SaveClass
{
    public float posx;
}
