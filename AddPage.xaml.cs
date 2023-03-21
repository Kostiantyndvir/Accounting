

namespace Accounting;

public partial class AddPage : ContentPage
{
    Entry entry;
    Button button;
    Entry entryP1;
    Entry entryP2;
    Label labelP1;
    Picker pickerP, catPicker;
    Label labelP2;
    DatePicker dateP;
    Entry entryC1;
    Entry entryC2;
    Label labelC1;
    Picker pickerC;
    Label labelC2;
    DatePicker dateC;
    public AddPage()
	{
		InitializeComponent();
	}

    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage() { });
    }
    private void PickerSelectedIndexChanged(object sender, System.EventArgs e)
    {
        stack.Clear();
        if (ItemPicker.SelectedItem.ToString() == "��������")
        {
            entry = new Entry() { Placeholder = "��'�" };
            catPicker = new();
            catPicker.Items.Add("������");
            catPicker.Items.Add("������");
            button = new Button() { Text = "������ ��������" };
            button.Clicked += AddCategory;
            stack.Add(entry);
            stack.Add(catPicker);
            stack.Add(button);
        }
        else if (ItemPicker.SelectedItem.ToString() == "�������")
        {
            entryC1 = new Entry() { Placeholder = "��'�" };
            entryC2 = new Entry() { Placeholder = "����" };
            labelC1 = new Label() { Text = "��������" };
            pickerC = new Picker();
            labelC2 = new Label() { Text = "����" };
            dateC = new DatePicker();
            using (Context cont = new Context())
            {
                var cats = cont.CostCats.ToList();
                foreach (var c in cats)
                    pickerC.Items.Add(c.Name);
            }
            Button buttonC = new Button() { Text = "������ �������" };
            buttonC.Clicked += AddCost;
            stack.Add(entryC1);
            stack.Add(entryC2);
            stack.Add(labelC1);
            stack.Add(pickerC);
            stack.Add(labelC2);
            stack.Add(dateC);
            stack.Add(buttonC);
        }
        else if (ItemPicker.SelectedItem.ToString() == "�����")
        {
            entryP1 = new Entry() { Placeholder = "��'�" };
            entryP2 = new Entry() { Placeholder = "����" };
            labelP1 = new Label() { Text = "��������" };
            pickerP = new Picker();
            labelP2 = new Label() { Text = "����" };
            dateP = new DatePicker();
            using (Context context = new Context())
            {
                var cats = context.ProfitCats.ToList();
                foreach (var c in cats)
                    pickerP.Items.Add(c.Name);
            }
            Button buttonP = new Button() { Text = "������ �����" };
            buttonP.Clicked += AddProfit;
            stack.Add(entryP1);
            stack.Add(entryP2);
            stack.Add(labelP1);
            stack.Add(pickerP);
            stack.Add(labelP2);
            stack.Add(dateP);
            stack.Add(buttonP);
        }
    }
    private void AddCategory(object sender, System.EventArgs e)
    {
        if (entry.Text != null && catPicker.SelectedItem != null)
        {
            using (Context cont = new Context())
            {
                if (catPicker.SelectedItem.ToString() == "������")
                {
                    CostCategory cat = new CostCategory()
                    {
                        Name = entry.Text
                    };
                    cont.CostCats.Add(cat);
                    cont.SaveChanges();
                    DisplayAlert("�����������", "�������� ������ ������", "Ok");
                    entry.Text = "";

                }
                else if (catPicker.SelectedItem.ToString() == "������")
                {
                    ProfitCategory cat = new ProfitCategory()
                    {
                        Name = entry.Text
                    };
                    cont.ProfitCats.Add(cat);
                    cont.SaveChanges();
                    DisplayAlert("�����������", "�������� ������ ������", "Ok");
                    entry.Text = "";

                }
                  
            }
        }
        else
            DisplayAlert("������������", "������ �������� �� ������� �� ���", "��");
    }
    private void AddCost(object sender, System.EventArgs e)
    {
        Cost cost = new Cost();

        if (entryC1.Text != null && entryC2.Text != null &&
            pickerC.SelectedItem != null)
        {
            using (Context cont = new Context())
            {

                cost.Name = entryC1.Text;
                Int32.TryParse(entryC2.Text, out int result);
                cost.Sum = result;
                cost.category = cont.CostCats.FirstOrDefault(
                        p => p.Name == pickerC.SelectedItem.ToString());
                cost.Data = DateOnly.FromDateTime(dateC.Date);
                cont.Csts.Add(cost);
                cont.SaveChanges();
                DisplayAlert("�����������", "������� ������", "Ok");
                entryC1.Text = "";
                entryC2.Text = "";
            }
        }
        else DisplayAlert("������������", "��� �� ������", "��");

    }

    private void AddProfit(object sender, System.EventArgs e)
    {
        Profit profit = new Profit();

        if (entryP1.Text != null && entryP2.Text != null &&
            pickerP.SelectedItem != null)
        {
            using (Context cont = new Context())
            {

                profit.Name = entryP1.Text;
                Int32.TryParse(entryP2.Text, out int result);
                profit.Sum = result;
                profit.category = cont.ProfitCats.FirstOrDefault(
                        p => p.Name == pickerP.SelectedItem.ToString());
                profit.Data = DateOnly.FromDateTime(dateP.Date);
                cont.Prfts.Add(profit);
                cont.SaveChanges();
                DisplayAlert("�����������", "����� �������", "Ok");
                entryP1.Text = "";
                entryP2.Text = "";
            }
        }
        else DisplayAlert("������������", "��� �� ������", "��");

    }
}