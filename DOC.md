# Documentation
**(This is where the documentation will be!)**

-----------

## Parameters
The program takes the following 4 arguments:
- Image width
- Image height
- Path of the output image
- Parameter ( doesn't do anything yet )

Example:
>rt004.exe 640 480 "output.pfm" 1

----

You can also use a JSON **config file** to parse the parameters.

Example of 'config.json':

```
{
   "Width":640,
   "Height":480,
   "Filename":"output.pfm",
   "Parameter":1
}
```
You can then run the program with the config file path as the single argument. Example:

>rt004.exe "config.json"

-----------