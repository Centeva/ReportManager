using System;
using System.Collections.Generic;
using System.Net;
using ReportManager.ReportService;

namespace ReportManager
{
	public class RSCommunicator : IRSCommunicator
	{
		private ReportingService2005 _reportService;

		public RSCommunicator(string url, string userName = null, string password = null)
		{
			_reportService = new ReportingService2005();
			_reportService.Url = url;
			_reportService.PreAuthenticate = true;
			if (String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
			{
				_reportService.UseDefaultCredentials = true;
			}
			else
			{
				_reportService.Credentials = new NetworkCredential(userName, password);
			}
		}

		public CatalogItem[] GetExistingReports(string folder)
		{
			string reportPath = "/" + folder;
			var results = _reportService.ListChildren(reportPath, true);
			return results;
		}

		public void DeleteAllReports(string folder)
		{
			string path = "/" + folder;
			_reportService.DeleteItem(path);
		}

		public void CreateFolder(string folderName, string parentPath)
		{
			try
			{
				_reportService.CreateFolder(folderName, parentPath, null);
			}
			catch(Exception ){}
		}

		public void CreateReport(string reportName, string parentPath, byte[] definition, IEnumerable<string> dataSourcePaths)
		{
			Property[] reportProperties = new Property[1];
			Property hidden = new Property();
			hidden.Name = "Hidden";
			hidden.Value = reportName.StartsWith("_") ? "true" : "false";
			reportProperties[0] = hidden;
			_reportService.CreateReport(reportName, parentPath, true, definition, reportProperties);

			var sources = new List<DataSource>();
			foreach (var dataSourcePath in dataSourcePaths)
			{
				DataSourceReference dsRef = new DataSourceReference();
				dsRef.Reference = dataSourcePath;
				var ds = new DataSource();
				ds.Item = dsRef;
				sources.Add(ds);
			}
			//DataSourceReference dsRef = new DataSourceReference();
			//	dsRef.Reference = dataSourcePaths;
			//	DataSource[] Sources = _reportService.GetItemDataSources(parentPath + "/" + reportName);
				if (sources != null && sources.Count > 0)
				{
					try
					{
						_reportService.SetItemDataSources(parentPath + "/" + reportName, sources.ToArray());
					}
					catch (Exception)
					{

					}
				}
		}

		public byte[] GetReportDefinition(string reportPath)
		{
			return _reportService.GetReportDefinition(reportPath);
		}

		public DataSource[] GetDataSources(string reportPath)
		{
			return _reportService.GetItemDataSources(reportPath);
		}
	}
}