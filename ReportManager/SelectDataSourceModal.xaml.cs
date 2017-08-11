using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReportManager
{
	/// <summary>
	/// Interaction logic for SelectDataSourceModal.xaml
	/// </summary>
	public partial class SelectDataSourceModal : Window
	{
		public SelectDataSourceModal(ViewModel vm)
		{
			InitializeComponent();
			this.DataContext = vm;
		}

		private void OK_Button_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Cancel_Button_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
