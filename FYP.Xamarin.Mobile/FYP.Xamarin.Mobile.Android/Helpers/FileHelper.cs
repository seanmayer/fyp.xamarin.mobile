using System.IO;
using FYP.Xamarin.Mobile.Droid.Helpers;
using FYP.Xamarin.Mobile.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace FYP.Xamarin.Mobile.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}