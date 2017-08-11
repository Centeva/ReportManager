using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using ReportManager.ReportService;

namespace ReportManager
{
	public class ViewModel : INotifyPropertyChanged
	{
		private readonly Window _owner;

		public ViewModel(Window owner)
		{
			_owner = owner;
			_url = "http://localhost/reportServer/reportService2005.asmx";
			FetchCommand = new RelayCommand(LoadData);
			UploadCommand = new RelayCommand(UploadReports);
		}
		private string _url;
		private Folder _rootFolder;
		private Folder _selectedFolder;
		private RSCommunicator _communicator;
		private List<SelectableDataSource> _dataSources;
		private string _status;
		public RelayCommand FetchCommand { get; set; }
		public RelayCommand UploadCommand { get; set; }

		public string Url
		{
			get { return _url; }
			set
			{
				_url = value;
				OnPropertyChanged();
			}
		}

		public Folder RootFolder
		{
			get { return _rootFolder; }
			set
			{
				_rootFolder = value;
				OnPropertyChanged();
			}
		}

		public Folder SelectedFolder
		{
			get { return _selectedFolder; }
			set
			{
				_selectedFolder = value; 
				OnPropertyChanged();
			}
		}

		public List<SelectableDataSource> DataSources
		{
			get { return _dataSources; }
			set
			{
				_dataSources = value; 
				OnPropertyChanged();
			}
		}

		public string Status
		{
			get { return _status; }
			set
			{
				_status = value;
				OnPropertyChanged();
			}
		}


		public void LoadData(object obj)
		{
			Status = "Fetching Data...";
			_communicator = new RSCommunicator(_url);
			RSRepository repo = new RSRepository(_communicator, null);
			RootFolder = repo.GetExistingItems("");
			DataSources = repo.GetDataSources();
			SelectedFolder = RootFolder;
			Status = "Data Fetched.";
		}


		public void UploadReports(object obj)
		{

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Multiselect = true;

			bool? result = dialog.ShowDialog(_owner);

			var dsModal = new SelectDataSourceModal(this);
			var dsResult = dsModal.ShowDialog();
			Status = "Uploading Reports...";
			foreach (var fileName in dialog.FileNames)
			{
				_communicator.CreateReport(Path.GetFileNameWithoutExtension(fileName), SelectedFolder.Path, File.ReadAllBytes(fileName), DataSources.Where(x=>x.Selected).Select(x=>x.Path));
			}

			Refresh();
			Status = "Upload complete and Data Fetched.";

		}

		private void Refresh()
		{
			var folderPath = SelectedFolder.Path;
			LoadData(null);
			SelectedFolder = RootFolder.SubFolders.FirstOrDefault(x => x.Path == folderPath);
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}
