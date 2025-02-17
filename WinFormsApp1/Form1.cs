namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Person p = new Person { Name = "sss" };
            propertyGrid1.SelectedObject = p;


        }
    }



    public class Person
    {
        public string Name { get; set; }
        public int Age { get;  }

    }
}
