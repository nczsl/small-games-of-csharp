using System;
using System.Collections.Generic;
using System.Text;

namespace Games2016 {
    public class GameCom {
        static GameCom cc;
        public static GameCom Deputy {
            get { if (cc == null) cc = new GameCom(); return cc; }
        }
        //
        public IEnumerable<int> QueryStep(int t) {
            var idx = 0;
            if (t > 0) {
                while (t > 0) {
                    yield return idx++;t--;
                }
            } else {
                while (t <= 0) {
                    yield return idx--;t++;
                }
            }
        }

    }
    static public class GameEx {
        static public int RemoveOne(this int i) {
            if (i > 0) return i - 1;
            else if (i < 0) return i + 1;
            else return i;
        }
    }
}
