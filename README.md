# Rodjenihm.Lib.MojBroj
Biblioteka za rešavanje popularne igre "Moj Broj" napisana u programskom jeziku C#

6.20.2019 - MojBrojSolver pronalazi tačan broj ako rešenje postoji. Algoritam je prost ali i veoma neefikasan. Zahteva dalje optimizacije.  

6.21.2019 - Poboljšan algoritam. Upotrbom niza umesto steka i bez suvišnih kopiranja niza prilikom poziva rekurzivne metode.  


Primer:  
```csharp
    using Rodjenihm.Lib.MojBroj;
    ...
    var mbSolver = new MojBrojSolver()
    mbSolver.Solve(numbers: new int[] { 1, 5, 8, 2, 10, 50 },  target: 872);
    Console.WriteLine(mb.Solution.Postfix); // Ispisuje rešenje u postfix formi
    Console.WriteLine(mb.Solution.Infix); // Ispisuje rešenje u infix formi
```
