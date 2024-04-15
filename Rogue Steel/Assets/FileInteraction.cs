using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileInteraction
{
    public int score;
    public List<inventory> inventory;
    public List<TnkModList> mods;
    static string path = "Assets/Resources/Save.txt";
    public void GetFile()
    {
        StreamReader reader = new StreamReader(path);
        
        reader.Close();
    }
    public static void SetFile()
    {

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Score:" + GameData.score);
        writer.WriteLine("Modules{");
        /*
        foreach(inventory inv in GameData.inventory)
        {
            writer.WriteLine(inv.name+"|"+inv.type+"|"+inv.amount);
        }
        */
        writer.WriteLine("}");
        writer.WriteLine("Inventory{");
        foreach (TnkModList mod in GameData.tnk)
        {
            //writer.WriteLine(mod.type+mod.position+mod.rotation+mod.variant+mod.CHP+mod.CA+mod.CD);
            writer.WriteLine(mod);
        }
        writer.WriteLine("}");
        writer.Close();
    }
    public int returnScore()
    { return score; }
    public List<inventory> returnInventory()
    { return inventory; }
    public List<TnkModList> returnTnkModList() {  return mods; }
}
