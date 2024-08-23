# MultiPrecisionAlgebra
 MultiPrecision Algebra 

## Requirement
.NET 8.0  
AVX2 suppoted CPU. (Intel:Haswell(2013)-, AMD:Excavator(2015)-)  
[MultiPrecision](https://github.com/tk-yoshimura/MultiPrecision)

## Install

[Download DLL](https://github.com/tk-yoshimura/MultiPrecisionAlgebra/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.multiprecision.algebra/)

## Usage

```csharp
// solve for v: Av=x
Matrix<Pow2.N4> a = new double[,] { { 1, 2 }, { 3, 4 } };
Vector<Pow2.N4> x = (4, 3);

Vector<Pow2.N4> v = Matrix<Pow2.N4>.Solve(a, x);
```

## Licence
[MIT](https://github.com/tk-yoshimura/MultiPrecisionAlgebra/blob/master/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
