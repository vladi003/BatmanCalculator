using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    public class BatmanCalculatorService : IBatmanCalculatorService
    {
        public int Calculate(int n, int k)
        {
            var minNumber = Math.Min(n, k);
            var maxNumber = Math.Max(n, k);

            var numbers = Enumerable.Range(minNumber, maxNumber).ToList();

            while (numbers.Count != 1)
            {
                var numbersCount = numbers.Count;
                var isCountEven = numbersCount % 2 == 0;

                for (int i = 1; i < numbersCount - 1; i += 2)
                {
                    numbers[i - 1] = numbers[i - 1] + numbers[i];
                    numbers[i] = numbers[i] + numbers[i + 1];
                }

                if (isCountEven)
                {
                    numbers[numbersCount - 2] = numbers[numbersCount - 2] + numbers[numbersCount - 1];
                }

                numbers.RemoveAt(numbersCount - 1);
            }

            return numbers[0];
        }
    }
}