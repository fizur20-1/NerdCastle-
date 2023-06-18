using System.Text;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;

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
            
            var output = textBox1.Text;
            var output1 = textBox2.Text;
            var output2 = textBox3.Text;

            if (!IsValidEmail(output1))
            {
                MessageBox.Show("Please enter a valid email address.");
               
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-B8UM566\\MSSQLSERVER2022;Initial Catalog=NerdCastle1;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into test(name,email,contact) values ('" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "')", con);

            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("saved ");

            }
            else
            {
                MessageBox.Show("Error");
            }
            con.Close();

            

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

           
            string searchEmail = textBox4.Text;

            // Establish connection to the database
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-B8UM566\\MSSQLSERVER2022;Initial Catalog=NerdCastle1;Integrated Security=True"))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Construct the SQL query
                    string query = "SELECT COUNT(*) FROM test WHERE email = @email";

                    // Create the command object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set the email parameter
                        command.Parameters.AddWithValue("@email", searchEmail);

                        // Execute the query and get the result
                        int count = (int)command.ExecuteScalar();

                        // Check if the email exists
                        if (count > 0)
                        {
                            MessageBox.Show("Email found in the database.");
                        }
                        else
                        {
                            MessageBox.Show("Email not found in the database.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any potential errors
                    MessageBox.Show("An error occurred while searching for the email: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }


        }
        private bool IsValidEmail(string email)
        {
            // Use regex pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        }
    }
}