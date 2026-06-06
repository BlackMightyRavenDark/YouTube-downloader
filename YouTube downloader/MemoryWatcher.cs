
namespace YouTube_downloader
{
	public static class MemoryWatcher
	{
		public static ulong RamTotal { get; private set; } = 0U;
		public static ulong RamUsed { get; private set; } = 0U;
		public static ulong RamFree { get; private set; } = 0U;

		private static Microsoft.VisualBasic.Devices.ComputerInfo _computerInfo = null;

		public static bool Update()
		{
			try
			{
				if (_computerInfo == null)
				{
					_computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
				}

				RamTotal = _computerInfo.TotalPhysicalMemory;
				RamFree = _computerInfo.AvailablePhysicalMemory;
				RamUsed = RamTotal - RamFree;

				return true;
			}
#if DEBUG
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			RamFree = RamUsed = RamTotal = 0L;
			_computerInfo = null;
			return false;
		}
	}
}
