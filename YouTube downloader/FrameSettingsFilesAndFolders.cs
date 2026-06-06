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
				json["ytdlExeFilePath"] = config.YtdlExeFilePath;
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
				{
					JToken jt = json.Value<JToken>("ytdlExeFilePath");
					if (jt != null)
					{
						config.YtdlExeFilePath = jt.Value<string>();
					}
				}
			};

			config.Loaded += s =>
			{
				textBoxDownloadDirectory.Text = config.DownloadDirectory;
				textBoxTempDirectory.Text = config.TemporaryDirectory;
				textBoxFileMergerDirectory.Text = config.ChunkMergerDirectory;
				textBoxOutputFileNameFormatWithDate.Text = config.OutputFileNameFormatWithDate;
				textBoxOutputFileNameFormatWithoutDate.Text = config.OutputFileNameFormatWithoutDate;
				textBoxWebBrowserFilePath.Text = config.WebBrowserExeFilePath;
				textBoxFfmpegExeFilePath.Text = config.FfmpegExeFilePath;
				textBoxYtdlExeFilePath.Text = config.YtdlExeFilePath;
			};
		}

		private void btnBrowseDownloadDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
				{
					Description = "Выберите папку для скачивания",
					SelectedPath = (!string.IsNullOrEmpty(config.DownloadDirectory) && Directory.Exists(config.DownloadDirectory)) ?
						config.DownloadDirectory : config.SelfDirectory
				})
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxDownloadDirectory.Text =
						config.DownloadDirectory = folderBrowserDialog.SelectedPath;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnBrowseTempDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
				{
					Description = "Выберите папку для временных файлов",
					SelectedPath = (!string.IsNullOrEmpty(config.TemporaryDirectory) && Directory.Exists(config.TemporaryDirectory)) ?
						config.TemporaryDirectory : config.SelfDirectory
				})
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxTempDirectory.Text =
						config.TemporaryDirectory = folderBrowserDialog.SelectedPath;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnWtfFileMergerDirectory_Click(object sender, EventArgs e)
		{
			string msg = "Для достижения максимальной производительности и уменьшения нагрузки на накопители, " +
				"\"Папка для временных файлов\" и \"Папка для объединения чанков\" должны находиться " +
				$"на разных физических дисках. А файл назначения не должен находиться на одном физическом диске с \"Папкой для объединения чанков\".{Environment.NewLine}" +
				"Если оставить это поле пустым, то \"Папка для объединения чанков\" будет равна \"Папке для временных файлов\".";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnBrowseFileMergerDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
				{
					Description = "Выберите папку для объединения чанков",
					SelectedPath = !string.IsNullOrEmpty(config.ChunkMergerDirectory) && Directory.Exists(config.ChunkMergerDirectory) ?
						config.ChunkMergerDirectory : config.SelfDirectory
				})
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxFileMergerDirectory.Text =
						config.ChunkMergerDirectory = folderBrowserDialog.SelectedPath;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Title = "Выберите EXE-файл браузера",
					Filter = "EXE-файлы|*.exe"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxWebBrowserFilePath.Text =
						config.WebBrowserExeFilePath = openFileDialog.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnBrowseFfmpegFilePath_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Title = "Выберите EXE-файл FFMPEG",
					Filter = "EXE-файлы|*.exe"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxFfmpegExeFilePath.Text =
						config.FfmpegExeFilePath = openFileDialog.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnBrowseYtdlExeFilePath_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Title = "Выберите EXE-файл youtube-dl или ytdlp",
					Filter = "EXE-файлы|*.exe"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxYtdlExeFilePath.Text =
						config.YtdlExeFilePath = openFileDialog.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
			config.FfmpegExeFilePath = textBoxFfmpegExeFilePath.Text;
		}
	}
}
