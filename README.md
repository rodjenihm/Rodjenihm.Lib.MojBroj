# Rodjenihm.Lib.MojBroj
Biblioteka za rešavanje popularne igre "Moj Broj" napisana u programskom jeziku C#

6.20.2019 - MojBrojSolver pronalazi tačan broj ako rešenje postoji. Algoritam je prost ali i veoma neefikasan. Zahteva dalje optimizacije.  

6.21.2019 - Poboljšan algoritam. Upotrbom niza umesto steka i bez suvišnih kopiranja niza prilikom poziva rekurzivne metode.    

6.26.2019 - Novi dizajn i funkcionalnost. MojBrojSolver sada ima mogućnost da pronalazi sva rešenja.  

6.27.2019 - Implementiran SolverEngine2. I do tri puta brži zahvaljujući pametnijoj eliminaciji suvišnih proračuna.  

7.1.2019 - SolverEngine3. 

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

Primer:
```csharp
    var stopwatch = new Stopwatch();
    
    var mbSolver1 = new MojBrojSolver(new SolverEngine());
    var mbSolver2 = new MojBrojSolver(new SolverEngine2());

    // 1
    stopwatch.Start();
    var solutions1 = mbSolver1.Solve(numbers: new int[] { 1, 5, 8, 2, 10, 50 }, target: 872).ToList();
    stopwatch.Stop();
    Console.WriteLine($"Broj resenja: {solutions1.Count} ; Vreme: {stopwatch.ElapsedMilliseconds}");

    stopwatch.Reset();

    // 2
    stopwatch.Start();
    var solutions2 = mbSolver2.Solve(numbers: new int[] { 1, 5, 8, 2, 10, 50 }, target: 872).ToList();
    stopwatch.Stop();
    Console.WriteLine($"Broj resenja: {solutions2.Count} ; Vreme: {stopwatch.ElapsedMilliseconds}");
```   
In progress, will be improved in the future...

