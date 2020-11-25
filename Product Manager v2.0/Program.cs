using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using static System.Console;

namespace Product_Manager_v2._0
{
    class Program
    {
        static string connectionString = "Server=localhost;Database=ProductManager2;Integrated Security=True";

        public static int id { get; set; }

        static void Main(string[] args)
        {
            bool mainMenu = true;

            do
            {
                CursorVisible = false;

                WriteLine("1. Categories");

                WriteLine("2. Articles (Ignore)");

                WriteLine("3. Exit");

                ConsoleKeyInfo mainMenuKeys = ReadKey();

                switch (mainMenuKeys.Key)
                {
                    case ConsoleKey.D1:
                        Clear();

                        bool subCategorieChoices = true;

                        do
                        {
                            WriteLine("1. Add Category");

                            WriteLine("2. List Categories");

                            WriteLine("3. Add Product to Category");

                            WriteLine("4. Add Category to Category");

                            WriteLine(" ");

                            WriteLine("Press (X) To Return To Main Menu.");

                            ConsoleKeyInfo subMenuCategoryKeyInput = ReadKey();

                            if (subMenuCategoryKeyInput.Key == ConsoleKey.D1)
                            {
                                bool addingCategory = true;

                                AddProduct(addingCategory);

                            }
                            else if (subMenuCategoryKeyInput.Key == ConsoleKey.D2)
                            {
                                Clear();

                                WriteLine("Categories");

                                WriteLine("------------------------------------------------------------------");

                                // SQL Code Here to View Categories added to Database

                                ListTasks();

                                WriteLine("");

                                WriteLine("Press Any Key To Return");

                                ReadKey(true);

                                Clear();
                            }
                            else if (subMenuCategoryKeyInput.Key == ConsoleKey.D3)
                            {
                                Clear();

                                WriteLine("ID  Category                                       Total products");

                                WriteLine("------------------------------------------------------------------");

                                ListTasks();

                                ListTasksBySearch();

                                ReadKey(true);

                                Clear();

                                // Input Code
                            }
                            else if (subMenuCategoryKeyInput.Key == ConsoleKey.D4)
                            {
                                // Input Code
                            }
                            else if (subMenuCategoryKeyInput.Key == ConsoleKey.X)
                            {
                                subCategorieChoices = false;
                            }
                        } while (subCategorieChoices);

                        //bool invalidArticleCredentials = true;

                        //AddProduct(invalidArticleCredentials);

                        Clear();

                        break;
                    case ConsoleKey.D2:
                        Clear();

                        // No function yet

                        break;
                    case ConsoleKey.D3:
                        Clear();

                        Environment.Exit(0);

                        break;
                }
            } while (mainMenu);
        }

        private static void AddProduct(bool addingCategory)
        {
            do
            {
                Clear();

                Write("Name: ");

                var categoryName = ReadLine();

                WriteLine(" ");

                Write("Total Products: ");

                var totalProducts = ReadLine();

                Console.CursorVisible = false;

                WriteLine(" ");
                WriteLine("Is this correct? (Y)es (N)o");

                ConsoleKeyInfo yesNo = ReadKey(true);

                // I want to run a code here that checks if the registration number already exists
                if (yesNo.Key == ConsoleKey.Y)
                {
                    Categories myCategory;

                    myCategory = new Categories(id, categoryName, totalProducts);

                    InsertMyTask(myCategory);

                    Clear();

                    WriteLine("Category saved.");

                    Thread.Sleep(2000);

                    Clear();

                    break;
                }
                else if (yesNo.Key == ConsoleKey.N)
                {
                    Clear();
                }
            } while (addingCategory);
        }

        private static void InsertMyTask(Categories myCategory)
        {
            var sql = $@"INSERT INTO CategoriesNext (Category, TotalProducts) VALUES (@categoryName, @totalProducts)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@categoryName", myCategory.categoryName);
                    command.Parameters.AddWithValue("@totalProducts", myCategory.totalProducts);

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        private static List<Categories> FetchMyTasks()
        {
            string sql = $@"SELECT * FROM CategoriesNext";

            List<Categories> myCategory = new List<Categories>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var category = (string)reader["Category"];
                    var totalProducts = (string)reader["TotalProducts"];

                    myCategory.Add(new Categories(id, category, totalProducts));
                }

                connection.Close();
            }

            return myCategory;
        }

        private static void ListTasks()
        {
            var myCategoryList = FetchMyTasks();

            foreach (var myCategory in myCategoryList)
            {
                WriteLine(" ");

                WriteLine($"ID: {myCategory.id}");

                WriteLine(" ");

                WriteLine($"Category Name: {myCategory.categoryName}");

                WriteLine(" ");

                WriteLine($"Total Products: {myCategory.totalProducts}");

                WriteLine(" ");

                WriteLine("------------------------------------------------------------------");
            }
        }

        private static void ListTasksBySearch()
        {
            var myCategoryList = SearchForTask();

            WriteLine("ID  Category                                       Total products");

            WriteLine("------------------------------------------------------------------");

            foreach (var myCategories in myCategoryList)
            {
                WriteLine(" ");

                WriteLine($"ID: {myCategories.id}");

                WriteLine(" ");

                WriteLine($"Category Name: {myCategories.categoryName}");

                WriteLine(" ");

                WriteLine($"Total Products: {myCategories.totalProducts}");

                WriteLine(" ");

                WriteLine("------------------------------------------------------------------");
            }
        }


        private static List<Categories> SearchForTask()
        {
            WriteLine("");

            CursorVisible = true;

            Write("Selected ID> ");

            string searchByID = ReadLine();

            //int searchByID = Convert.ToInt32(Console.ReadLine());

            CursorVisible = false;

            Clear();

            string sql = $@"SELECT * FROM CategoriesNext WHERE ID = {searchByID}";

            List<Categories> myCategories = new List<Categories>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var category = (string)reader["Category"];
                    var totalProducts = (string)reader["TotalProducts"];

                    myCategories.Add(new Categories(id, category, totalProducts));
                }

                connection.Close();
            }

            return myCategories;
        }
    }
}
