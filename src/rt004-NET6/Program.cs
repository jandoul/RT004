using System.Security.Principal;
using System.Text.Json;
using Util;
//using System.Numerics;

namespace rt004;

// Holds information about the generated image and all of the parameters
class Image
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string? Filename { get; set; }
    public int Parameter { get; set; }
}


internal class Program
{
    // Loads parameters into the Image from the command-line or config file
    public static bool GetCommandLineArguments(string[] args, out Image image)
    {
        image = new Image();

        try
        {
            if (args.Length == 4) // Command-line arguments
            {
                Console.WriteLine("Reading parameters from command-line.");
                image.Width = Int32.Parse(args[0]);
                image.Height = Int32.Parse(args[1]);
                image.Filename = args[2];
                image.Parameter = Int32.Parse(args[3]);
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
                Console.WriteLine("(Correct usage: 'image_width image_height image_path parameter' or 'config_file_path')");
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

        // Put an interesting pattern into the image
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                float R = (float)Math.Cos((float)i / (1+j & (i * j)));
                float G = (float)Math.Sin((float)j / (1+i & (i * j)));
                float B = (float)Math.Cos((float)(i+G*100)*(j+R*100) / (i*j));
                float[] pixel = new float[3] { R, G, B };
                fi.PutPixel(i, j, pixel);
            }
        }

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
