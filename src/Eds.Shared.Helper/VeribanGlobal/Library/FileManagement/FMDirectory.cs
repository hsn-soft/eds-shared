namespace Eds.Shared.Helper.VeribanGlobal.Library.FileManagement
{
    public class FMDirectory
    {
        public KeyValuePair<bool, string> CreateDirectory(string directoryPath, bool checkIfExists = false)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    if (checkIfExists)
                    {
                        return new KeyValuePair<bool, string>(false, "Aynı isimde bir klasör mevcut");
                    }
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                }
                return new KeyValuePair<bool, string>(true, directoryPath);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public KeyValuePair<bool, string> DeleteDirectory(string directoryPath, bool checkIfExistsMoveToArchive = true)
        {
            try
            {
                if (Directory.GetFiles(directoryPath).Length == 0 && Directory.GetDirectories(directoryPath).Length == 0)
                {
                    Directory.Delete(directoryPath);
                }

                if (checkIfExistsMoveToArchive)
                {
                    return MoveDirectory(directoryPath);
                }
                return new KeyValuePair<bool, string>(true, directoryPath);

            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }

        }

        public KeyValuePair<bool, string> MoveDirectory(string sourcePath)
        {


            return new KeyValuePair<bool, string>(true, sourcePath);
        }
    }
}
