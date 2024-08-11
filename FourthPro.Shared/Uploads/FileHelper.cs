using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;

namespace FourthPro.Uploads;

public class FileHelper
{
    public static string UploadFile(IFormFile file, FileType type)
    {
        List<string> validExtentions = new List<string>() { ".jpg", ".jpeg", ".pdf", ".png", ".gif", ".bmp" };
        var extention = Path.GetExtension(file.FileName);

        if (!validExtentions.Contains(extention))
            throw new InValidExtensionException();

        long size = file.Length;
        if (size >= (5 * 1024 * 1024)) // 5 mg
            throw new InValidSizeException();

        string fileName = Guid.NewGuid().ToString() + extention;

        string folderName = "";
        //if (isLecture)
        //    folderName = Path.Combine("Uploads", "LectureFiles");
        //else
        //    folderName = Path.Combine("Uploads", "LastQuestionsFile");

        switch (type)
        {
            case FileType.LastQuestionsFile:
                folderName = Path.Combine("Uploads", "LastQuestionsFile");
                break;

            case FileType.Lecture:
                folderName = Path.Combine("Uploads", "LectureFiles");
                break;

            case FileType.Project:
                folderName = Path.Combine("Uploads", "ProjectFiles");
                break;
        }
        string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        var fullPath = Path.Combine(path, fileName);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        using FileStream stream = new FileStream(fullPath, FileMode.Create);
        file.CopyTo(stream);

        return fileName;
    }
    public static async Task<MemoryStream> DownloadFile(string fileName, FileType type)
    {

        string folderName = "";
        switch (type)
        {
            case FileType.LastQuestionsFile:
                folderName = Path.Combine("Uploads", "LastQuestionsFile");
                break;

            case FileType.Lecture:
                folderName = Path.Combine("Uploads", "LectureFiles");
                break;

            case FileType.Project:
                folderName = Path.Combine("Uploads", "ProjectFiles");
                break;
        }
        // string path = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

        //var fullPath = Path.Combine(path, fileName);

        //if (!System.IO.File.Exists(path))
        // throw new NotFoundException("File does not exist");

        //var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);


        //var f = new FileStream(path, FileMode.Open, FileAccess.Read);
        //return f;

        var path = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

        // Debugging: Print the constructed path
        Console.WriteLine($"Constructed path: {path}");

        if (!System.IO.File.Exists(path))
            throw new NotFoundException("File does not exist");

        try
        {
            var f = new FileStream(path, FileMode.Open, FileAccess.Read);

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                f.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var resultStream = new MemoryStream(fileBytes);
            return resultStream;

            //return f;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Could not find file '{path}': {ex.Message}");
            // Handle the exception (e.g., return null or throw a custom exception)
            return null;
        }
    }
}
