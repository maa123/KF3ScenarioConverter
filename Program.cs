using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AssetStudio;

namespace KF3ScenarioConverter {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                return;
            }
            bool textMode = false;
            if (args.Length > 1) {
                if (args[1] == "text") {
                    textMode = true;
                }
                if (args[1] == "image") {
                    Img(args);
                    return;
                }
            }
            AssetsManager aM = new AssetsManager();
            aM.LoadFiles(args[0]);
            if (aM.assetsFileList.Count == 1) {
                MonoBehaviour mB = null;
                foreach (var asset in aM.assetsFileList[0].Objects) {
                    switch (asset) {
                        case MonoBehaviour m_MonoBehaviour:
                            mB = m_MonoBehaviour;
                            break;
                    }
                }
                if (mB != null) {
                    if (textMode) {
                        Console.Write(mB.DumpText());
                    } else {
                        Console.Write(mB.Dump());
                    }
                }
            }
        }
        static void Img(string[] args) {
            AssetsManager aM = new AssetsManager();
            aM.LoadFiles(args[0]);
            if (aM.assetsFileList.Count == 1) {
                MonoBehaviour mB = null;
                foreach (var asset in aM.assetsFileList[0].Objects) {
                    switch (asset) {
                        case Texture2D m_Texture2D:
                            var filename = "./output/" + m_Texture2D.m_Name + ".png";
                            var bitmap = m_Texture2D.ConvertToBitmap(true);
                            if (ExportFileExists(filename)) {
                                continue;
                            }
                            bitmap.Save(filename, ImageFormat.Png);
                            bitmap.Dispose();
                            break;
                    }
                }
            }
            return;
        }
        private static bool ExportFileExists(string filename) {
            if (File.Exists(filename)) {
                return true;
            }
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            return false;
        }
    }
}
