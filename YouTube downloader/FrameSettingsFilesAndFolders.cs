using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public partial class FrameSettingsFilesAndFolders : UserControl
	{
		public FrameSettingsFilesAndFolders()
		{
			InitializeComponent();

			config.Saving += (s, json) =>
			{
				json["downloadingDirPath"] = config.DownloadingDirPath;
				json["tempDirPath"] = config.TempDirPath;
				json["chunksMergingDirPath"] = config.ChunksMergingDirPath;
				json["outputFileNameFormatWithDate"] = config.OutputFileNameFormatWithDate;
				json["outputFileNameFormatWithoutDate"] = config.OutputFileNameFormatWithoutDate;
				json["browserExeFilePath"] = config.BrowserExeFilePath;
				json["ffmpegExeFilePath"] = config.FfmpegExeFilePath;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("downloadingDirPath");
					if (jt != null)
					{
						config.DownloadingDirPath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("tempDirPath");
					if (jt != null)
					{
						config.TempDirPath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("chunksMergingDirPath");
					if (jt != null)
					{
						config.ChunksMergingDirPath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("outputFileNameFormatWithDate");
					if (jt != null)
					{
						config.OutputFileNameFormatWithDate = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("outputFileNameFormatWithoutDate");
					if (jt != null)
					{
						config.OutputFileNameFormatWithoutDate = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("browserExeFilePath");
					if (jt != null)
					{
						config.BrowserExeFilePath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("ffmpegExeFilePath");
					if (jt != null)
					{
						config.FfmpegExeFilePath = jt.Value<string>();
					}
				}
			};

			config.Loaded += (s) =>
			{
				textBoxDownloadDirectory.Text = config.DownloadingDirPath;
				textBoxTempDirectory.Text = config.TempDirPath;
				textBoxFileMergerDirectory.Text = config.ChunksMergingDirPath;
				textBoxOutputFileNameFormatWithDate.Text = config.OutputFileNameFormatWithDate;
				textBoxOutputFileNameFormatWithoutDate.Text = config.OutputFileNameFormatWithoutDate;
				textBoxWebBrowserFilePath.Text = config.BrowserExeFilePath;
				textBoxFfmpegFilePath.Text = config.FfmpegExeFilePath;
			};
		}

		private void btnBrowseDownloadDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для скачивания";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.DownloadingDirPath) && Directory.Exists(config.DownloadingDirPath)) ?
				config.DownloadingDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.DownloadingDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxDownloadDirectory.Text = config.DownloadingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnBrowseTempDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для временных файлов";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.TempDirPath) && Directory.Exists(config.TempDirPath)) ?
				config.TempDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.TempDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxTempDirectory.Text = config.TempDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnWtfFileMergerDirectory_Click(object sender, EventArgs e)
		{
			string msg = "Для достижения максимальной производительности и уменьшения нагрузки на накопители, " +
				"\"Папка для временных файлов\" и \"Папка для объединения чанков\" должны находиться " +
				"на разных физических дисках. А файл назначения не должен находиться на одном физическом диске с \"Папкой для объединения чанков\".\n" +
				"Если оставить это поле пустым, то \"Папка для объединения чанков\" будет равна \"Папке для временных файлов\".";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnBrowseFileMergerDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для объединения чанков";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.ChunksMergingDirPath) && Directory.Exists(config.ChunksMergingDirPath)) ?
				config.ChunksMergingDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.ChunksMergingDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxFileMergerDirectory.Text = config.ChunksMergingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnResetFileNameFormatWithDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = FILENAME_FORMAT_DEFAULT_WITH_DATE;
			textBoxOutputFileNameFormatWithDate.Text = FILENAME_FORMAT_DEFAULT_WITH_DATE;
		}

		private void btnResetFileNameFormatWithoutDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			textBoxOutputFileNameFormatWithoutDate.Text = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
		}

		private void btnSelectWebBrowserFilePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл браузера";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.BrowserExeFilePath = ofd.FileName;
				textBoxWebBrowserFilePath.Text = config.BrowserExeFilePath;
			}
			ofd.Dispose();
		}

		private void btnBrowseFfmpegFilePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл FFMPEG";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.FfmpegExeFilePath = ofd.FileName;
				textBoxFfmpegFilePath.Text = config.FfmpegExeFilePath;
			}
			ofd.Dispose();
		}

		private void textBoxDownloadDirectory_Leave(object sender, EventArgs e)
		{
			config.DownloadingDirPath = textBoxDownloadDirectory.Text;
		}

		private void textBoxTempDirectory_Leave(object sender, EventArgs e)
		{
			config.TempDirPath = textBoxTempDirectory.Text;
		}

		private void textBoxFileMergerDirectory_Leave(object sender, EventArgs e)
		{
			config.ChunksMergingDirPath = textBoxFileMergerDirectory.Text;
		}

		private void textBoxOutputFileNameFormatWithDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = textBoxOutputFileNameFormatWithDate.Text;
		}

		private void textBoxOutputFileNameFormatWithoutDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = textBoxOutputFileNameFormatWithoutDate.Text;
		}

		private void textBoxFfmpegFilePath_Leave(object sender, EventArgs e)
		{
			config.FfmpegExeFilePath = textBoxFfmpegFilePath.Text;
		}
	}
}
