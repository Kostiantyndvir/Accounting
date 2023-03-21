

using Syncfusion.Maui.Charts;


namespace Accounting;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Display();
    }

    private void Add(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AddPage());
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void DisplayEvent(object sender, System.EventArgs e)
    {
        Display();
    }
    public void Display()
    {
        stack.Clear();

        using (Context cont = new Context())
        {
            int  profitSum=0, costSum = 0;
            Label name, sum;
            Button button;
            HorizontalStackLayout innerStack;
            
            var catsP = cont.ProfitCats.ToList();
            foreach (var item in catsP)
            {
                
                innerStack = new();
                name = new() { Text = item.Name };
                int temp = cont.Prfts.
                    Where(u => u.category == item).
                    Where(u => u.Data >= DateOnly.FromDateTime(dateStart.Date)).
                    Where(u => u.Data <= DateOnly.FromDateTime(dateEnd.Date)).
                    Sum(u => u.Sum);
                profitSum += temp;
                sum = new()
                {
                    Text = temp.ToString()
                };
                button = new() { Text = "-->" };

                name.VerticalOptions = LayoutOptions.Center;
                sum.VerticalOptions = LayoutOptions.Center;
                sum.Padding = new Thickness(10, 0, 10, 0);
                button.FontAttributes = FontAttributes.Bold;
                button.FontSize = 25;
                button.Padding = new Thickness(0, 0, 0, 0);
                button.Clicked += DisplayDiagramEvent;
                button.StyleId = item.Id.ToString();
                innerStack = new() { name, sum};
                stack.Add(innerStack);
            }
            BoxView boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);
            
            name = new() { Text = "ДОХОДИ" };
            sum = new() { Text = profitSum.ToString() };
            button = new() { Text = "-->" };

            name.VerticalOptions = LayoutOptions.Center;
            sum.VerticalOptions = LayoutOptions.Center;
            sum.Padding = new Thickness(10, 0, 10, 0);
            button.FontAttributes = FontAttributes.Bold;
            button.FontSize = 25;
            button.Padding = new Thickness(0, 0, 0, 0);
            button.Clicked += DisplayDiagramEvent;

            button.StyleId = "profit";
            innerStack = new() { name, sum,  button };
            innerStack.VerticalOptions = LayoutOptions.Center;
            stack.Add(innerStack);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);

            var catsC = cont.CostCats.ToList();
            foreach (var item in catsC)
            {
                innerStack = new();
                name = new() { Text = item.Name };
                int temp = cont.Csts.
                    Where(u => u.category == item).
                    Where(u => u.Data >= DateOnly.FromDateTime(dateStart.Date)).
                    Where(u => u.Data <= DateOnly.FromDateTime(dateEnd.Date)).
                    Sum(u => u.Sum);
                costSum += temp;
                sum = new()
                {
                    Text = temp.ToString()
                };
                button = new() { Text = "-->" };

                name.VerticalOptions = LayoutOptions.Center;
                sum.VerticalOptions = LayoutOptions.Center;
                sum.Padding = new Thickness(10, 0, 10, 0);
                button.FontAttributes = FontAttributes.Bold;
                button.FontSize = 25;
                button.Padding = new Thickness(0, 0, 0, 0);
                button.Clicked += DisplayDiagramEvent;

                button.StyleId = item.Id.ToString();
                innerStack = new() { name, sum};
                stack.Add(innerStack);
            }
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView); 

            name = new() { Text = "ВИТРАТИ" };
            sum = new() { Text = costSum.ToString()};
            button = new() { Text = "-->" };

            name.VerticalOptions = LayoutOptions.Center;
            sum.VerticalOptions = LayoutOptions.Center;
            sum.Padding = new Thickness(10,0,10,0);
            button.FontAttributes = FontAttributes.Bold;
            button.FontSize = 25;
            button.Padding = new Thickness(0,0,0,0);
            button.Clicked += DisplayDiagramEvent;

            button.StyleId = "cost";
            innerStack = new() { name, sum, button };
            stack.Add(innerStack);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);

            name = new() { Text = "ВСЬОГО" };
            name.FontAttributes = FontAttributes.Bold;
            name.Margin = new Thickness(0, 20, 0, 0);
            name.Padding = new Thickness(0, 20, 0, 0);
            sum = new() { Text = (-costSum + profitSum).ToString() };
            sum.FontAttributes = FontAttributes.Bold;
            sum.Margin = new Thickness(0, 20, 0, 0);
            sum.Padding = new Thickness(10, 20, 10, 0);
            innerStack = new() { name, sum };
            stack.Add(innerStack);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);
            boxView = new BoxView { Color = Colors.Gray, HeightRequest = 2, Margin = new Thickness(1), HorizontalOptions = LayoutOptions.Fill };
            stack.Add(boxView);
        }
    }
    private void DisplayDiagramEvent(object sender, System.EventArgs e)
    {
        DisplayDiagram(((Button)sender).StyleId.ToString());
    }
    public void DisplayDiagram(string id)
    {
        stack.Clear();
        using (Context cont = new Context())
        {

            SfCartesianChart chart = new SfCartesianChart();
            Button buttonBack = new() { Text = "<--" };
            buttonBack.Clicked += Back;
            buttonBack.FontSize = 25;
            buttonBack.FontAttributes = FontAttributes.Bold;
            buttonBack.WidthRequest = 100;

            chart.Title = buttonBack;
            string name ="";
            
            ViewModel View = new ViewModel();
            if (id == "cost")
            {

                var catsC = cont.CostCats.ToList();
                foreach (var item in catsC)
                {
                    int temp = cont.Csts.
                        Where(u => u.category == item).
                        Where(u => u.Data >= DateOnly.FromDateTime(dateStart.Date)).
                        Where(u => u.Data <= DateOnly.FromDateTime(dateEnd.Date)).
                        Sum(u => u.Sum);
                    Column col = new() { Name = item.Name, Value = temp };
                    View.Data.Add(col);
                    name = "ВИТРАТИ";
                }
            }
            else if (id == "profit")
            {

                var catsP = cont.ProfitCats.ToList();
                foreach (var item in catsP)
                {
                    int temp = cont.Prfts.
                        Where(u => u.category == item).
                        Where(u => u.Data >= DateOnly.FromDateTime(dateStart.Date)).
                        Where(u => u.Data <= DateOnly.FromDateTime(dateEnd.Date)).
                        Sum(u => u.Sum);
                    Column col = new() { Name = item.Name, Value = temp };
                    View.Data.Add(col);
                    name = "ДОХОДИ";
                }
            }

            // Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title = new ChartAxisTitle { Text = name };
            chart.XAxes.Add(primaryAxis);

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title = new ChartAxisTitle { Text = "Кошти (грн)" };
            chart.YAxes.Add(secondaryAxis);

            //Initialize the two series for SfChart
            ColumnSeries series = new ColumnSeries()
            {
                Label = "Value",
                ShowDataLabels = true,
                ItemsSource = View.Data,
                XBindingPath = "Name",
                YBindingPath = "Value",
                DataLabelSettings = new CartesianDataLabelSettings
                {
                    LabelPlacement = DataLabelPlacement.Inner
                }
            };

            //Adding Series to the Chart Series Collection
            chart.Series.Add(series);
            Content = chart;
        }
    }
}

