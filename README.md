# Rodjenihm.Lib.MojBroj
Biblioteka za rešavanje popularne igre "Moj Broj" napisana u programskom jeziku C#

6.20.2019 - MojBrojSolver pronalazi tačan broj ako rešenje postoji. Algoritam je prost ali i veoma neefikasan. Zahteva dalje optimizacije.  

6.21.2019 - Poboljšan algoritam. Upotrbom niza umesto steka i bez suvišnih kopiranja niza prilikom poziva rekurzivne metode.    

6.26.2019 - Novi dizajn i funkcionalnost. MojBrojSolver sada ima mogućnost da pronalazi sva rešenja. 

Primer:  
```csharp
    using Rodjenihm.Lib.MojBroj;
    ...
    var mbSolver = new MojBrojSolver();
    var solutions = mbSolver.Solve(numbers: new int[] { 1, 5, 8, 2, 10, 50 }, target: 872);

    // Ispisuje jedno resenje
    Console.WriteLine(solutions.FirstOrDefault().Postfix); // U postfix formi
    Console.WriteLine(solutions.FirstOrDefault().Infix); // U infix formi

    // Ispisuje sva resenja
    foreach (var solution in solutions)
    {
        Console.WriteLine(solution.Postfix); // U postfix formi
        Console.WriteLine(solution.Infix); // U infix formi
    }
```
