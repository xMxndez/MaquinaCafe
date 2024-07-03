public enum CupSize
{
    Small,
    Medium,
    Large
}

public class CoffeeMachine
{
    public int CoffeeAmount { get; private set; }
    public int SmallCups { get; private set; }
    public int MediumCups { get; private set; }
    public int LargeCups { get; private set; }
    public int SugarAmount { get; private set; }

    public CoffeeMachine()
    {
        CoffeeAmount = 100;
        SmallCups = 10;
        MediumCups = 10;
        LargeCups = 10;
        SugarAmount = 50;
    }

    public string DispenseCoffee(CupSize size, int sugar)
    {
        int requiredCoffee = size switch
        {
            CupSize.Small => 3,
            CupSize.Medium => 5,
            CupSize.Large => 7,
            _ => throw new ArgumentOutOfRangeException()
        };

        int requiredCup = size switch
        {
            CupSize.Small => SmallCups,
            CupSize.Medium => MediumCups,
            CupSize.Large => LargeCups,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (requiredCup <= 0)
        {
            return "No hay vasos disponibles.";
        }

        if (CoffeeAmount < requiredCoffee)
        {
            return "No hay café disponible.";
        }

        if (SugarAmount < sugar)
        {
            return "No hay azúcar disponible.";
        }

        CoffeeAmount -= requiredCoffee;

        switch (size)
        {
            case CupSize.Small:
                SmallCups--;
                break;
            case CupSize.Medium:
                MediumCups--;
                break;
            case CupSize.Large:
                LargeCups--;
                break;
        }

        SugarAmount -= sugar;

        return $"Felicitaciones! Aquí está tu vaso {size.ToString().ToLower()} de café con {sugar} cucharada(s) de azúcar.";
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        CoffeeMachine coffeeMachine = new CoffeeMachine();

        while (true)
        {
            Console.WriteLine("Selecciona el tamaño del vaso (1: Pequeño, 2: Mediano, 3: Grande):");
            if (!int.TryParse(Console.ReadLine(), out int sizeChoice) || sizeChoice < 1 || sizeChoice > 3)
            {
                Console.WriteLine("Opción no válida. Intenta de nuevo.");
                continue;
            }

            CupSize size = sizeChoice switch
            {
                1 => CupSize.Small,
                2 => CupSize.Medium,
                3 => CupSize.Large,
                _ => throw new ArgumentOutOfRangeException()
            };

            Console.WriteLine("Introduce la cantidad de cucharadas de azúcar:");
            if (!int.TryParse(Console.ReadLine(), out int sugar) || sugar < 0)
            {
                Console.WriteLine("Opción no válida. Intenta de nuevo.");
                continue;
            }

            string result = coffeeMachine.DispenseCoffee(size, sugar);
            Console.WriteLine(result);

            Console.WriteLine($"Café restante: {coffeeMachine.CoffeeAmount} Oz");
            Console.WriteLine($"Vasos pequeños restantes: {coffeeMachine.SmallCups}");
            Console.WriteLine($"Vasos medianos restantes: {coffeeMachine.MediumCups}");
            Console.WriteLine($"Vasos grandes restantes: {coffeeMachine.LargeCups}");
            Console.WriteLine($"Azúcar restante: {coffeeMachine.SugarAmount} cucharadas");
        }
    }
}