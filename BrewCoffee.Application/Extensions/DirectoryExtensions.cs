namespace BrewCoffee.Application.Extensions
{
    public static class DirectoryExtensions
    {
        public static string GetSolutionDirectory()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("BrewCoffee.sln").Any())
            {
                directory = directory.Parent;
            }

            if (directory == null)
            {
                throw new Exception("BrewCoffee.sln does not exist in any of the parent directories of the current working directory.");
            }

            return directory.FullName!;
        }
    }
}
