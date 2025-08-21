using System.Buffers;
using System.IO.MemoryMappedFiles;

namespace Mail.dat.Io
{
	public class MemoryMappedRecordFile : DisposableObject
	{
		protected MemoryMappedFile Mmf { get; private set; }
		protected long Capacity { get; private set; }
		protected int RecordLength { get; private set; }

		public MemoryMappedRecordFile(string path, int recordLength, long recordCount)
		{
			this.RecordLength = recordLength;
			this.Capacity = (long)recordLength * recordCount;

			//
			// Make sure the folder exists.
			//
			string dir = Path.GetDirectoryName(Path.GetFullPath(path));

			if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			//
			// Create a brand-new file with the full capacity and ReadWrite access.
			// If you want to overwrite if it already exists, use FileMode.Create.
			// If you want to fail if it exists, use FileMode.CreateNew.
			//
			this.Mmf = MemoryMappedFile.CreateFromFile(path: path, mode: FileMode.Create, mapName: null, capacity: this.Capacity, access: MemoryMappedFileAccess.ReadWrite);
		}

		public void WriteRecord(long index, ReadOnlySpan<byte> data)
		{
			if (data.Length != this.RecordLength)
			{
				throw new ArgumentException("Record length mismatch.", nameof(data));
			}

			long offset = index * (long)this.RecordLength;

			if (offset < 0 || offset + this.RecordLength > this.Capacity)
			{
				throw new ArgumentOutOfRangeException(nameof(index), "Record is outside the mapped capacity.");
			}

			using MemoryMappedViewAccessor view = this.Mmf.CreateViewAccessor(offset, this.RecordLength, MemoryMappedFileAccess.ReadWrite);

			//
			// WriteArray takes an array; rent a tiny buffer to bridge from ReadOnlySpan<byte>.
			//
			byte[] temp = ArrayPool<byte>.Shared.Rent(this.RecordLength);

			try
			{
				data.CopyTo(temp);
				view.WriteArray(0, temp, 0, this.RecordLength);
				view.Flush();
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(temp);
			}
		}

		protected override void OnDisposeManagedObjects()
		{
			this.Mmf?.Dispose();
		}
	}
}