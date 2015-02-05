namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CheckSources : IEnumerable<CheckSource>
    {
        private readonly string basePath;

        private readonly string projectfilePattern;

        public CheckSources(string basePath, string projectfilePattern)
        {
            this.basePath = basePath;
            this.projectfilePattern = projectfilePattern;
        }

        public IEnumerator<CheckSource> GetEnumerator()
        {
            foreach (var file in this.GetFiles())
            {
                yield return new CheckSource(file);
            }
        }

        private IEnumerable<FileInfo> GetFiles()
        {
            var hasFiles = false;
            foreach (var file in this.GetFiles(this.basePath))
            {
                hasFiles = true;
                yield return file;
            }

            if (!hasFiles)
            {
                throw new MatchException(string.Format("No project found that match '{0}'", this.projectfilePattern));
            }
        }

        private IEnumerable<FileInfo> GetFiles(string path)
        {

            foreach (var file in Directory.GetFiles(path, this.projectfilePattern))
            {
                yield return new FileInfo(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var fileInfo in this.GetFiles(directory))
                {
                    yield return fileInfo;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.GetClassess(classPattern));
        }

        private IEnumerable<CheckClass> GetClassess(string classPattern)
        {
            return this.SelectMany(s => s.Class(classPattern));
        }

        public CheckClasses Class()
        {
            return this.Class(string.Empty);
        }

        public CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(this.Select(s => s.Assembly()).Where(a => FileUtil.FilenameMatchesPattern(a.FileName, matchAssemblies)), matchAssemblies);
        }

        public CheckFile File(string checkCs)
        {
            throw new System.NotImplementedException();
        }
    }
}