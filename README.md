# MultiPrecisionAlgebra 
 Float multi precision algebra 

## Requirement
.NET 6.0

AVX2 suppoted CPU. (Intel:Haswell(2013)-, AMD:Excavator(2015)-)

## Install

[Download DLL](https://github.com/tk-yoshimura/MultiPrecision/releases)  
[Download Nuget package](https://www.nuget.org/packages/tyoshimura.multiprecision.algebra/)

- Import MultiPrecision(https://github.com/tk-yoshimura/MultiPrecision)
- To install, just import the DLL.
- This library does not change the environment at all.

## Usage

```csharp
// solve for v: Av=x
Matrix<Pow2.N4> a = new(new double[,] { { 1, 2 }, { 3, 4 } });
Vector<Pow2.N4> x = new(4, 3);

Vector<Pow2.N4> v = Matrix<Pow2.N4>.Solve(a, x);
```

## Licence
[MIT](https://github.com/tk-yoshimura/MultiPrecisionAlgebra/blob/master/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
