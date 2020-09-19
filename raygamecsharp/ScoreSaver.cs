using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static RGCore.Core_basic_window;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace MiniAtariArcade
{
    class ScoreSaver
    {
        public static void LoadSavedData() 
        {
            FileStream fs = new FileStream(@"SavedScores.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                savedData = (SavedData)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                savedData = new SavedData();
                Console.WriteLine(e.Message + "\nDefault data used.");
            }
            finally
            {
                fs.Close();
            }
        }
        public static void SaveScores()
        {
            FileStream fs = new FileStream(@"SavedScores.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, savedData);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    [Serializable]
    public class SavedData 
    {
        public int BreakoutScore = 0;
        public string BreakoutName = "";
        public int AsteroidsScore = 0;
        public string AsteroidsName = "";
        public float Volume = 1f;
    }

}
