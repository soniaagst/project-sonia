public delegate void Geom(int x, int y);
delegate int IntegerOutputDelegate(int x);

public static class Shape {
    public static void Geometrics(List<List<int>> shapeParameters, Geom geom) {
        foreach (List<int> item in shapeParameters) {
            geom(item[0], item[1]);
        }
    }
}