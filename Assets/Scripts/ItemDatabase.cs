#region[Code]

#region[Namespaces]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;//gettin lit
#endregion

public class Stat
{
    public int Power { get; set; }
    public int Defense { get; set; }
    public int Vitality { get; set; }
    public Stat(Stat stats)
    {
        this.Power = stats.Power;
        this.Defense = stats.Defense;
        this.Vitality = stats.Vitality;
    }
    public Stat(JsonData data)
    {
        Power = (int)data["power"];
        Defense = (int)data["defense"];
        Vitality = (int)data["vitality"];
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public Stat stat { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public Sprite sprite { get; set; }
    public GameObject gameObject { get; set; }
    //sic burn
    public Item()
    {
        ID = -1;
    }
    public Item(JsonData data)
    {
        ID = (int)data["id"];
        Title = (string)data["title"];
        Value = (int)data["value"];
        stat = new Stat(data["stats"]);
        Description = (string)data["description"];
        Stackable = (bool)data["stackable"];
        Rarity = (int)data["rarity"];
        string filename = data["sprite"].ToString();
        sprite = Resources.Load<Sprite>("Sprite/Items/" + filename);
    }
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance = null;
    private Dictionary<string, Item> database = new Dictionary<string, Item>();
    //hot box
    private JsonData itemData;

    void Awake()
    {
        if (instance == null)
        {
            string jsonFilePath = Application.dataPath + "/StreamingAssets/Items.JSON";
            string jsonText = File.ReadAllText(jsonFilePath);
            itemData = JsonMapper.ToObject(jsonText);
            ConstructDataBase();
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //it burns
    void Update()
    {
        
    }
    void ConstructDataBase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            JsonData data = itemData[i];
            Item newitem = new Item(data);
            database.Add(newitem.Title, newitem);
        }
    }
    public static Item GetItem(string n)
    {
        Dictionary<string, Item> db = instance.database;
        if (db.ContainsKey(n))
        {
            return db[n];
        }
        return null;
    }
    public static Dictionary<string, Item> GetDb()
    {
        return instance.database;
    }
}

#endregion
