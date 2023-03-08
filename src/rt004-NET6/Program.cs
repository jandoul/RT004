using System.Security.Principal;
using System.Text.Json;
using Util;
//using System.Numerics;

namespace rt004;

// Holds information about the generated image
class Image
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string? Filename { get; set; }
}


internal class Program
{
    public static bool GetCommandLineArguments(string[] args, out Image image)
    {
        image = new Image();

        try
        {
            if (args.Length == 3) // Command-line arguments
            {
                Console.WriteLine("Reading parameters from command-line.");
                image.Width = Int32.Parse(args[0]);
                image.Height = Int32.Parse(args[1]);
                image.Filename = args[2];
            }
            else if (args.Length == 1) // Config file
            {
                string configFile = args[0];
                Console.WriteLine($"Reading parameters from config file ({configFile}).");
                string json = File.ReadAllText(configFile);
                image = JsonSerializer.Deserialize<Image>(json);
            }
            else
            {
                throw new IOException();
            }
        }
        catch (Exception ex)
        {
            if (ex is IOException || ex is IndexOutOfRangeException || ex is FormatException)
            {
                Console.WriteLine("Argument error.");
                return false;
            }
        }
        Console.WriteLine("Success!");
        return true;
    }

    static void Main(string[] args)
    {
        // Parameters.
        Image image;

        // Parse command-line arguments or config file.
        if (GetCommandLineArguments(args, out image) == false)
            return;


        // HDR image.
        FloatImage fi = new FloatImage(image.Width, image.Height, 3);

        // TODO: put anything interesting into the image.
        // TODO: use fi.PutPixel() function, pixel should be a float[3] array [R, G, B]

        //fi.SaveHDR(fileName);   // Doesn't work well yet...
        if (image.Filename is null)
        {
            Console.WriteLine("Invalid or missing filename.");
            return;
        }
        fi.SavePFM(image.Filename);

        Console.WriteLine("HDR image is finished.");
    }
}
