using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ReportManager.ReportService;

namespace ReportManager
{
	public class Report : IRSItem, INotifyPropertyChanged
	{
		private ObservableCollection<IRSItem> _children; 
		public Report()
		{
			_children = new ObservableCollection<IRSItem>();
			Selected = true;
		}
		public Report(CatalogItem item, DataSource[] dataSources) : this()
		{
			Name = item.Name;
			Path = item.Path;
			ModifiedBy = item.ModifiedBy;
			ModifiedDate = item.ModifiedDate;
			Hidden = item.Hidden;
			DataSources = dataSources;

		}
		public string Name { get; set; }
		public string Path { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public bool Hidden { get; set; }
		public DataSource[] DataSources { get; set; }

		public Folder ParentFolder { get; set; }

		private bool _selected;
		public bool Selected
		{
			get { return _selected; }
			set
			{
				_selected = value; 
				RaisePropertyChanged("Selected");
			}
		}

		public string DataSource
		{
			get
			{
				return string.Join(",", DataSources.Select(x=> x.Item is DataSourceReference ? (x.Item as DataSourceReference).Reference : x.Name));
			}
		}


		public ObservableCollection<IRSItem> Children
		{
			get { return _children; }
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}

			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}