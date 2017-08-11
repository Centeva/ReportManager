using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReportManager.ReportService;

namespace ReportManager
{
	public class SelectableDataSource : INotifyPropertyChanged
	{
		private string _path;
		private bool _selected;

		public string Path
		{
			get { return _path; }
			set
			{
				_path = value;
				OnPropertyChanged();
			}
		}

		public bool Selected
		{
			get { return _selected; }
			set
			{
				_selected = value; 
				OnPropertyChanged();
			}
		}

		public SelectableDataSource(CatalogItem ci)
		{
			Path = ci.Path;
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