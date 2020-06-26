using System;
using AssetStudio;

namespace KF3ScenarioConverter {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                return;
            }
            bool textMode = false;
            if(args.Length > 1) {
                if(args[1] == "text") {
                    textMode = true;
                }
            }
            AssetsManager aM =  new AssetsManager();
            aM.LoadFiles(args[0]);
            if(aM.assetsFileList.Count == 1) {
                MonoBehaviour mB = null;
                foreach(var asset in aM.assetsFileList[0].Objects) {
                    switch (asset) {
                        case MonoBehaviour m_MonoBehaviour:
                            mB = m_MonoBehaviour;
                            break;
                    }
                }
                if(mB != null) {
                    if (textMode) {
                        Console.Write(mB.DumpText());
                    } else {
                        Console.Write(mB.Dump());
                    }
                }
            }
        }
    }
}
