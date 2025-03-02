Vector v1 = new(1,1,0);
Vector v2 = new(1,2,0);

Vector sum = v1 + v2;
sum.PrintValue();

int test = 5 + 3;
Console.WriteLine(test);

bool isEqual = v1 == v2;
Console.WriteLine(isEqual);

Console.WriteLine(v1.Equals(v2));

Console.WriteLine(v1 != v2);

Vector dot = v1 * v2;
dot.PrintValue();

Vector cross = v1 & v2;
cross.PrintValue();

Vector v3 = new(1,2,3);
Vector cross2 = v1 & v2 & v3;
cross2.PrintValue();

Vector scalar = 3 ^ v1;
scalar.PrintValue();

Vector neg = ~v1;
neg.PrintValue();

v1++;
v1.PrintValue();

Vector inc = v1++;
inc.PrintValue();
v1.PrintValue();

++v1;
v1.PrintValue();

Vector inc2 = ++v1;
inc2.PrintValue();
v1.PrintValue();

(--v2).PrintValue();