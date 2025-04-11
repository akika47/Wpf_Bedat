using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Bedat.Models;

namespace Wpf_Bedat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Esemeny> events = new();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
        events.Clear();
        var filePath = string.Empty;
        OpenFileDialog ofd = new();

        ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

        if (ofd.ShowDialog() == true)
        {
            filePath = ofd.FileName;
            File.ReadAllLines(filePath).ToList().ForEach(x => events.Add(new Esemeny(x)));
        }
        dtgEvents.ItemsSource = events;

        FillUpCombobox();
    }


    private void FillUpCombobox()
    {
        cbxFilter.Items.Add("Összes");
        events.Select(x => x.EventType).Distinct().ToList().ForEach(x => cbxFilter.Items.Add(x));
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Biztos, hogy ki akarod űríteni a datagridet?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            events.Clear();
            cbxFilter.Items.Clear();
        }
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        string wr = "";
        var filePath = string.Empty;

        SaveFileDialog sfd = new();
        sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        foreach (var item in events)
        {
            wr += $"{item.Id} {item.EventTime} {item.EventType} {item.IsItAfterNoon}\n";
        }
        if (sfd.ShowDialog() == true)
        {
            filePath = sfd.FileName;
            File.WriteAllText(filePath, wr);
        }

    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (dtgEvents.SelectedIndex == -1)
        {
            MessageBox.Show("Válassz ki előbb egy recordot!");
        }
        else
        {

            MessageBoxResult result = MessageBox.Show("Biztos, hogy ki akarod törölni ezt a recordot?",
                                     "Confirmation",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                events.Remove((Esemeny)dtgEvents.SelectedItem);
            }

        }
    }

    private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbxFilter.SelectedItem.ToString() == "Összes")
        {
            dtgEvents.ItemsSource = events;
        }
        else
        {
            dtgEvents.ItemsSource = events.Where(x => x.EventType == (EventType)cbxFilter.SelectedItem).ToList();
        }
    }
}