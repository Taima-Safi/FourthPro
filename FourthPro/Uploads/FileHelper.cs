//using FourthPro.Shared.Exception;

//namespace FourthPro.Uploads;

//public class FileHelper
//{
//    public static string UploadFile(IFormFile file, bool isLecture)
//    {
//        List<string> validExtentions = new List<string>() { ".jpg", ".jpeg", ".pdf", ".png", ".gif", ".bmp" };
//        var extention = Path.GetExtension(file.FileName);

//        if (!validExtentions.Contains(extention))
//            throw new InValidExtensionException();

//        long size = file.Length;
//        if (size >= (5 * 1024 * 1024)) // 5 mg
//            throw new InValidSizeException();

//        string fileName = Guid.NewGuid().ToString() + extention;

//        string folderName = "";
//        if (isLecture)
//            folderName = Path.Combine("Uploads", "LectureFiles");
//        else
//            folderName = Path.Combine("Uploads", "LastQuestionsFile");

//        string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

//        var fullPath = Path.Combine(folderName, fileName);

//        if (!Directory.Exists(path))
//            Directory.CreateDirectory(path);

//        using FileStream stream = new FileStream(fullPath, FileMode.Create);
//        file.CopyTo(stream);

//        return fileName;
//    }
//}
