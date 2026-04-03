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
				json["downloadDirectory"] = config.DownloadDirectory;
				json["temporaryDirectory"] = config.TemporaryDirectory;
				json["chunkMergerDirectory"] = config.ChunkMergerDirectory;
				json["outputFileNameFormatWithDate"] = config.OutputFileNameFormatWithDate;
				json["outputFileNameFormatWithoutDate"] = config.OutputFileNameFormatWithoutDate;
				json["webBrowserExeFilePath"] = config.WebBrowserExeFilePath;
				json["ffmpegExeFilePath"] = config.FfmpegExeFilePath;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("downloadDirectory");
					if (jt != null)
					{
						config.DownloadDirectory = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("temporaryDirectory");
					if (jt != null)
					{
						config.TemporaryDirectory = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("chunkMergerDirectory");
					if (jt != null)
					{
						config.ChunkMergerDirectory = jt.Value<string>();
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
					JToken jt = json.Value<JToken>("webBrowserExeFilePath");
					if (jt != null)
					{
						config.WebBrowserExeFilePath = jt.Value<string>();
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
				textBoxDownloadDirectory.Text = config.DownloadDirectory;
				textBoxTempDirectory.Text = config.TemporaryDirectory;
				textBoxFileMergerDirectory.Text = config.ChunkMergerDirectory;
				textBoxOutputFileNameFormatWithDate.Text = config.OutputFileNameFormatWithDate;
				textBoxOutputFileNameFormatWithoutDate.Text = config.OutputFileNameFormatWithoutDate;
				textBoxWebBrowserFilePath.Text = config.WebBrowserExeFilePath;
				textBoxFfmpegFilePath.Text = config.FfmpegExeFilePath;
			};
		}

		private void btnBrowseDownloadDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для скачивания";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.DownloadDirectory) && Directory.Exists(config.DownloadDirectory)) ?
				config.DownloadDirectory : config.SelfDirectory;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.DownloadDirectory =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxDownloadDirectory.Text = config.DownloadDirectory;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnBrowseTempDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для временных файлов";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.TemporaryDirectory) && Directory.Exists(config.TemporaryDirectory)) ?
				config.TemporaryDirectory : config.SelfDirectory;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.TemporaryDirectory =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxTempDirectory.Text = config.TemporaryDirectory;
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
				(!string.IsNullOrEmpty(config.ChunkMergerDirectory) && Directory.Exists(config.ChunkMergerDirectory)) ?
				config.ChunkMergerDirectory : config.SelfDirectory;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.ChunkMergerDirectory =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxFileMergerDirectory.Text = config.ChunkMergerDirectory;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnResetFileNameFormatWithDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = Configurator.FILENAME_FORMAT_DEFAULT_WITH_DATE;
			textBoxOutputFileNameFormatWithDate.Text = Configurator.FILENAME_FORMAT_DEFAULT_WITH_DATE;
		}

		private void btnResetFileNameFormatWithoutDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = Configurator.FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			textBoxOutputFileNameFormatWithoutDate.Text = Configurator.FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
		}

		private void btnSelectWebBrowserFilePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл браузера";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.WebBrowserExeFilePath = ofd.FileName;
				textBoxWebBrowserFilePath.Text = config.WebBrowserExeFilePath;
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
			config.DownloadDirectory = textBoxDownloadDirectory.Text;
		}

		private void textBoxTempDirectory_Leave(object sender, EventArgs e)
		{
			config.TemporaryDirectory = textBoxTempDirectory.Text;
		}

		private void textBoxFileMergerDirectory_Leave(object sender, EventArgs e)
		{
			config.ChunkMergerDirectory = textBoxFileMergerDirectory.Text;
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
