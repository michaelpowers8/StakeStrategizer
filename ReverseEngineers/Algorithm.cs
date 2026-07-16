using System.Security.Cryptography;
using System.Text;
namespace ProvablyFairSimulation.ReverseEngineers;
public class ProvablyFairAlgorithm
{
    private const int ServerSeedLength = 64;
    private const int ByteCount = ServerSeedLength / 2;
    private readonly string _serverSeed;
    private readonly string _serverSeedHash;
    private readonly string _clientSeed;
    private readonly long _startingNonce;
    private long _nonce;

    public ProvablyFairAlgorithm()
    {
        _serverSeed = GenerateRandomSeed();
        _serverSeedHash = Sha256HashSeed(_serverSeed);
        _clientSeed = GenerateRandomSeed();
        _nonce = 1;
        _startingNonce = _nonce;
    }
    
    public ProvablyFairAlgorithm(string clientSeed)
    {
        _serverSeed = GenerateRandomSeed();
        _serverSeedHash = Sha256HashSeed(_serverSeed);
        _clientSeed = clientSeed;
        _nonce = 1;
        _startingNonce = _nonce;
    }
    
    public ProvablyFairAlgorithm(string serverSeed, string clientSeed)
    {
        _serverSeed =  serverSeed;
        _serverSeedHash = Sha256HashSeed(_serverSeed);
        _clientSeed = clientSeed;
        _nonce = 1;
        _startingNonce = _nonce;
    }
    
    public ProvablyFairAlgorithm(string serverSeed, string clientSeed, int nonce)
    {
        _serverSeed =  serverSeed;
        _serverSeedHash = Sha256HashSeed(_serverSeed);
        _clientSeed = clientSeed;
        _nonce = nonce;
        _startingNonce = _nonce;
    }

    internal string GetServerSeed()
    {
        return _serverSeed;
    }

    internal string GetServerSeedHash()
    {
        return _serverSeedHash;
    }

    internal string GetClientSeed()
    {
        return _clientSeed;
    }

    internal long GetNonce()
    {
        return _nonce;
    }

    internal long GetStartingNonce()
    {
        return _startingNonce;
    }
    
    private List<byte[]> SeedsToBytes(int numMessages)
    {
        if (numMessages < 1)
        {
            throw new ArgumentException("Number of messages must be greater than or equal to 1.");
        }

        List<byte[]> bytesList = new List<byte[]>();
        var encodedServerSeed = Encoding.UTF8.GetBytes(_serverSeed);
        for (int i = 0; i < numMessages; i++)
        {
            var message = $"{_clientSeed}:{_nonce}:{i}";
            var encodedMessage = Encoding.UTF8.GetBytes(message);
            using (var hmacObj = new HMACSHA256(encodedServerSeed))
            {
                var hmacResults = hmacObj.ComputeHash(encodedMessage);
                bytesList.Add(hmacResults);
            }
        }
        return bytesList;
    }

    private string CrashSeedsToBytes()
    {
        var encodedServerSeed = Encoding.UTF8.GetBytes(_serverSeed);
        var encodedServerSeedHash = Encoding.UTF8.GetBytes(_clientSeed);
        using (var hmacObj = new HMACSHA256(encodedServerSeedHash))
        {
            var hmacResults = hmacObj.ComputeHash(encodedServerSeed);
            return Convert.ToHexString(hmacResults);
        }
    }
    
    private double CrashBytesToDecimal(string bytes)
    {
        double number = 0;
        int exponent = 7;
        foreach (var currentByte in bytes)
        {
            int currentByteDecimal = Convert.ToInt32(currentByte.ToString(), 16);
            number += currentByteDecimal * Math.Pow(16, exponent);
            exponent--;
            if (exponent < 0)
            {
                break;
            }
        }
        return number;
    }
    
    private double BytesToNumber(byte[] bytes)
    {
        double number = 0;
        int exponent = 1;
        foreach (var currentByte in bytes)
        {
            number += currentByte / Math.Pow(256, exponent);
            exponent++;
        }
        return number;
    }
    
    private static string GenerateRandomSeed()
    {
        byte[] secureRandomBytes = new byte[ByteCount];
        RandomNumberGenerator.Fill(secureRandomBytes);
        string hexString = BitConverter.ToString(secureRandomBytes).Replace("-", "");
        return hexString;
        
    }

    private static string Sha256HashSeed(string seed)
    {
        byte[] seedBytes = Encoding.UTF8.GetBytes(seed);
        return BitConverter.ToString(SHA256.HashData(seedBytes)).Replace("-", "");
    }
    
    internal List<double> RandomStakeNumbers(List<double> multipliers)
    {
        var seedBytesList = SeedsToBytes(multipliers.Count);
        List<double> numbers = new List<double>();
        foreach (var seedBytes in seedBytesList)
        {
            for (int i = 0; i < seedBytes.Length; i += 4)
            {
                numbers.Add(BytesToNumber(seedBytes.Skip(i).Take(4).ToArray()) * multipliers[numbers.Count]);
                if (numbers.Count == multipliers.Count) 
                    break;
            }
            if (numbers.Count == multipliers.Count) break;
        }
        _nonce++;
        return numbers;
    }
    
    internal List<double> RandomStakeNumbers(double multiplier)
    {
        var seedBytes = SeedsToBytes(1).First();
        List<double> numbers = new List<double>();
        for (int i = 0; i < seedBytes.Length; i += 4)
        {
            numbers.Add(BytesToNumber(seedBytes.Skip(i).Take(4).ToArray()) * multiplier);
        }
        _nonce++;
        return numbers;
    }

    internal double GetCrashDecimal()
    {
        string crashBytes = CrashSeedsToBytes();
        return CrashBytesToDecimal(crashBytes);
    }
}