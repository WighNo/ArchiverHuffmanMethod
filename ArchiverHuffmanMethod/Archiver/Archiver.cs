using ArchiverHuffmanMethod.Compress;

namespace ArchiverHuffmanMethod
{
    class Archiver
    {
        private Compression _compress = new Compression();
        private Decompression _decompression = new Decompression();

        public void Archive(string savePath, string archiveFileName)
        {
            _compress.Execute(savePath, archiveFileName);
        }

        public void Unpack(string filePath, string savePath)
        {
            _decompression.Execute(filePath, savePath);
        }

    }
}
