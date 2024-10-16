using MainApp.Shared.Enums;

namespace MainApp.Shared.Services
{
    public interface IFileService
    {
        string GetFromFile();
        StatusCodes SaveToFile(string content);
    }
}