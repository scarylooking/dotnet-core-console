using System;
using System.IO;

namespace example.Utilities
{
    public interface IUtility
    {
       string GetDateString();

        string GetTimeString();

        string GenerateTemporaryDirectory();
    }
    
    public class Utility : IUtility
    {
        public string GetDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public string GetTimeString()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        
        public string GenerateTemporaryDirectory()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), "temp", Guid.NewGuid().ToString());
            
            Directory.CreateDirectory(path);

            return path;
        }
    }
}