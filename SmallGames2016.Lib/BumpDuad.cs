using System;
using static System.Math;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Games2016 {
    public class Cell {
        public int mark;
        public int x, y;
    }
    public class BumpDuad {
        //public int Count { get { return (from i in map where i.mark != 0 select i).Count(); } }
        Cell[,] map;
        public BumpDuad(Cell[,] map) {
            this.map = map;
        }
        public Cell[] FindPath(Cell a, Cell b) {
            var width = Abs(a.x - b.x);
            var height = Abs(a.y - b.y);
            var xlen = map.GetLength(0);
            var ylen = map.GetLength(1);
            //首先固定width 查找height
            var x = FindPass(true, a, b, width, ylen, FindHorizontalBrachium);
            if (x != null) return x;
            return FindPass(false, a, b, height, xlen, FindVerticleBrachium);
        }

        private Cell[] FindPass(bool horizontal, Cell a, Cell b, int fix, int len, Func<Cell, int, int, int, Cell[]> f) {
            var pass = default(Cell[]);
            if (fix > 0) {
                var mincount = default(int);
                var outcount = default(int);
                int x3, x4; x3 = x4 = 0;
                if (horizontal) {
                    if (b.y > a.y) {
                        mincount = len - a.y; outcount = -a.y;
                    } else {
                        mincount = -a.y; outcount = len - a.y;
                    }
                    foreach (var item in GameCom.Deputy.QueryStep(mincount)) {
                        x3 = a.y + item;
                        x4 = b.y - x3;
                        pass = f(a, x3, fix, x4);
                        if (IsPass(pass)) { return pass; }
                    }
                    foreach (var item in GameCom.Deputy.QueryStep(outcount)) {
                        x3 = a.y + item;
                        x4 = b.y - x3;
                        pass = f(a, x3, fix, x4);
                        if (IsPass(pass)) { return pass; }
                    }
                } else {
                    if (b.x > a.x) {
                        mincount = len - a.x; outcount = -a.x;
                    } else {
                        mincount = -a.x; outcount = len - a.x;
                    }
                    foreach (var item in GameCom.Deputy.QueryStep(mincount)) {
                        x3 = a.x + item;
                        x4 = b.x - x3;
                        pass = f(a, x3, fix, x4);
                        if (IsPass(pass)) { return pass; }
                    }
                    foreach (var item in GameCom.Deputy.QueryStep(outcount)) {
                        x3 = a.x + item;
                        x4 = b.x - x3;
                        pass = f(a, x3, fix, x4);
                        if (IsPass(pass)) { return pass; }
                    }
                }
            }
            return pass;
        }

        Cell[] FindHorizontalBrachium(Cell a, int w1, int h, int w2) {
            var r = new List<Cell>();
            var current = a;
            foreach (var item in GameCom.Deputy.QueryStep(w1)) {
                current = map[current.x + item, current.y];
                r.Add(current);
            }
            foreach (var item in GameCom.Deputy.QueryStep(h)) {
                current = map[current.x, current.y += item];
                r.Add(current);
            }
            foreach (var item in GameCom.Deputy.QueryStep(w2.RemoveOne())) {
                current = map[current.x += item, current.y];
                r.Add(current);
            }
            return r.ToArray();
        }
        Cell[] FindVerticleBrachium(Cell a, int h1, int w, int h2) {
            var r = new List<Cell>();
            var current = a;
            foreach (var item in GameCom.Deputy.QueryStep(h1)) {
                current = map[current.x, current.y += item];
                r.Add(current);
            }
            foreach (var item in GameCom.Deputy.QueryStep(w)) {
                current = map[current.x += item, current.y];
                r.Add(current);
            }
            foreach (var item in GameCom.Deputy.QueryStep(h2.RemoveOne())) {
                current = map[current.x, current.y += item];
                r.Add(current);
            }
            return r.ToArray();
        }
        bool IsPass(Cell[] path) {
            return (from i in path where i.mark == 0 select i).Count() == path.Length;
        }
    }
}
