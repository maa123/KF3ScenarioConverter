using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AssetStudio;

namespace KF3ScenarioConverter {
    class Program {
        static void Main(string[] args) {
            //全てTEXTMODEとして扱う
            //Output以下に出力し、拡張子は.txtとする
            if (args.Length < 1) {
                return;
            }
            foreach(var filepath in args) {
                Convert(filepath);
            }
            Console.ReadKey();
        }

        static void Convert(string filepath) {
            AssetsManager aM = new AssetsManager();
            aM.LoadFiles(filepath);
            if (aM.assetsFileList.Count == 1) {
                MonoBehaviour mB = null;
                foreach (var asset in aM.assetsFileList[0].Objects) {
                    switch(asset) {
                        case MonoBehaviour m_MonoBehaviour:
                            mB = m_MonoBehaviour;
                            break;
                    }
                }
                if(mB != null) {
                    var filename = "./output/" + Path.GetFileNameWithoutExtension(filepath) + ".txt";
                    if (ExportFileExists(filename)) {
                        aM.Clear();
                        return;
                    }
                    var file = new System.IO.StreamWriter(filename);
                    Console.WriteLine(filename);
                    file.Write(mB.DumpText());
                    file.Close();
                    aM.Clear();
                    return;
                } else {
                    aM.Clear();
                }
            } else {
                aM.Clear();
            }
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
