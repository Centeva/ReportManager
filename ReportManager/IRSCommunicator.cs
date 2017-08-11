using System.Collections.Generic;
using ReportManager.ReportService;

namespace ReportManager
{
	public interface IRSCommunicator
	{
		CatalogItem[] GetExistingReports(string folder);
		void DeleteAllReports(string folder);
		void CreateFolder(string folderName, string parentPath);
		void CreateReport(string reportName, string parentPath, byte[] definition, IEnumerable<string> dataSourcePath);
		byte[] GetReportDefinition(string reportPath);
		DataSource[] GetDataSources(string reportPath);
	}
}