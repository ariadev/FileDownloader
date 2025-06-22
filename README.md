# FileDownloader

A simple C# application for downloading multiple files from direct URLs using .NET 7.

## Features

* Downloads files from a list of direct URLs.
* Saves downloaded files in a custom-named folder within the project directory.
* Lightweight and easy to use.

## Requirements

* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) installed on your system.

## Usage

1. **Install .NET 7**
   Make sure you have the .NET 7 SDK installed.

2. **Add Your URLs**
   Create or edit the `urls.txt` file in the project root, and add one direct download URL per line.

3. **Run the Application**
   In the project directory, open a terminal and run:

   ```bash
   dotnet run
   ```

4. **Save the Files**
   Once the downloads are complete, the program will prompt you to enter a name for the destination folder. The downloaded files will be saved in a folder with that name inside the project directory.

## Example

```
https://example.com/file1.jpg
https://example.com/file2.pdf
```

Running `dotnet run` will download these files and save them in a folder you specify when prompted.

## License

This project is licensed under the MIT License.
