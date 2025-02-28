void RectangleArea(int length, int wide) {
    Console.WriteLine($"Area of {length}x{wide} rectangle = {length * wide}");
}

void RectanglePerimeter(int length, int wide) {
    Console.WriteLine($"Perimeter of {length}x{wide} rectangle = {2*(length + wide)}");
}

Geom Rectangle = new Geom(RectangleArea);
Rectangle += RectanglePerimeter;

Rectangle(3,5);

void CircleArea(int radius, int diameter) {
    Console.WriteLine($"Area of circle with radius {radius} = {Math.PI * radius * radius}");
}

void CirclePerimeter(int radius, int diameter) {
    Console.WriteLine($"Perimeter of circle with radius {radius} = {Math.PI * diameter}");
}

Geom Circle = CircleArea;
Circle += CirclePerimeter;

Circle(5,10);

int IntegerMethod1(int x) => x;
int IntegerMethod2(int x) => 2*x;
int IntegerMethod3(int x) => 3*x;

IntegerOutputDelegate Test = IntegerMethod1;
Test += IntegerMethod2;
Test += IntegerMethod3;

Console.WriteLine(Test(2));

List<List<int>> rectangles = [
    [2,3],
    [4,4],
    [8,7]
];

Shape.Geometrics(rectangles, Rectangle);

Func<string, int> len = (text) => text.Length;
Console.WriteLine(len("four"));

Action<string> print = (text) => Console.WriteLine(text);
print($"3x5 = {3*5} is a true statement.");

