using System.Numerics;


// Use Parse method to convert string to BigInteger
string x ="3141592653589793238462643383279502884197169399375105820974944592";
string y = "2718281828459045235360287471352662497757247093699959574966967627";

//STEPS
// -- case scenario: MULTIPLYING 12345 BY 67899

/*
 * 
step 1
    split the numbers to two halves:
    12345 -> 12, 345
    67899 -> 67, 899

step 2
    compute the products of the first half of the 1st number and the 1st half of the second number
    Do the same for the second halves
    12 * 67 = 804;
    345 * 899 = 310155;
    Then get the sum of first and second halves f the first and second numbers. Then get the product of their sums
    (12 + 345) * (67 + 899) = 357 * 966 = 344742;

step 3
    Then subtract the first 2 products from the third product
    344742 - 804 - 310155 = 33783

step 4
    Finally get the final result by multiplying the first product by 10 to the power of twice the number of digits of each half mutiplied by 2 (in this case 10 power 6)
    Add the result to the step 3 result multiplied by 10 to the power of the number of digits of each half (in this case 10 power 3)
    Add the new result to the second product
    (804 * 10 power 6) + (33783 * 10 power 3) + 310155 = 837218355

*/

KaratsubaOperation operation = new KaratsubaOperation();
Console.WriteLine(operation.GetProduct(x, y).ToString());

public class KaratsubaOperation
{
    public string x {  get; init; }
    public string y { get; init; } //A shorter way of using constructor {get; init:}

    public BigInteger GetProduct(string x, string y)
    {
        // Step 1
        int n = Math.Max(x.Length, y.Length);
        if (n == 1)
            return BigInteger.Parse(x) * BigInteger.Parse(y);

        int mid = n / 2;
        string firstHalfX = x.Substring(0, Math.Max(mid, 0));
        string secondHalfX = x.Substring(Math.Max(x.Length - mid, 0));
        string firstHalfY = y.Substring(0, Math.Max( mid, 0));
        string secondHalfY = y.Substring(Math.Max(y.Length - mid, 0));

        // Step 2
        BigInteger firstHalfProduct = GetProduct(firstHalfX, firstHalfY);
        BigInteger secondHalfProduct = GetProduct(secondHalfX, secondHalfY);
        BigInteger sumOfHalvesProduct = GetProduct(AddStrings(firstHalfX, secondHalfX), AddStrings(firstHalfY, secondHalfY)) - firstHalfProduct - secondHalfProduct;

        // Step 3
        BigInteger result = firstHalfProduct * BigInteger.Pow(10, 2 * mid) + sumOfHalvesProduct * BigInteger.Pow(10, mid) + secondHalfProduct;
        return result;
    }

    private string AddStrings(string num1, string num2)
    {
        int maxLength = Math.Max(num1.Length, num2.Length);
        num1 = num1.PadLeft(maxLength, '0');
        num2 = num2.PadLeft(maxLength, '0');

        int carry = 0;
        string result = "";

        for (int i = maxLength - 1; i >= 0; i--)
        {
            int sum = (num1[i] - '0') + (num2[i] - '0') + carry;
            carry = sum / 10;
            result = (sum % 10) + result;
        }

        if (carry > 0)
            result = carry + result;

        return result;
    }

}