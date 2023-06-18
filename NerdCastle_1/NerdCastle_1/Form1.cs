using System.Text;
using System.Data.SqlClient;


namespace NerdCastle_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-B8UM566\\MSSQLSERVER2022;Initial Catalog=NerdCastle1;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into test(name,email,contact) values ('"+ textBox1.Text + "', '"+ textBox2.Text + "','"+ textBox3.Text + "')", con);

            int i =cmd.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("saved ");

            }
            else
            {
                MessageBox.Show("Error");
            }
            con.Close();

            var output = textBox1.Text;
            var output1 = textBox2.Text;
            var output2 = textBox3.Text;
            MessageBox.Show(output + " " + output1 + " " + output2);

            String file = @"C:\Users\ASUS\Downloads\Nerd Castle\NerdCastle_1\output.csv";
            String separator = ",";
            StringBuilder output11 = new StringBuilder();
            String[] headings = { "First Name", "Email", "Contact" };
            output11.AppendLine(string.Join(separator, headings));


            String[] newLine = { output, output1, output2 };
            output11.AppendLine(string.Join(separator, newLine));

            try
            {
                File.AppendAllTextAsync(file, output11.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
        }
    }
}