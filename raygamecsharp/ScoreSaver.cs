using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static RGCore.Core_basic_window;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace RGCore
{
    class ScoreSaver
    {

        public static void LoadScores() 
        {
            FileStream fs = new FileStream("SavedScores.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                savedScores = (ScoreData)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                savedScores = new ScoreData();
            }
            finally
            {
                fs.Close();
            }
        }
        public static void SaveScores()
        {
            FileStream fs = new FileStream("SavedScores.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, savedScores);
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
    public class ScoreData 
    {
        public int BreakoutScore = 0;
        public string BreakoutName = "";
        public int AsteroidsScore = 0;
        public string AsteroidsName = "";
    }

}
