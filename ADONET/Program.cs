using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;

namespace ADONET
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isDone = false;
            LoginUser loginUser = new LoginUser();

            // получаем строку соединения

            //const string dp = "System.Data.SqlClient";
            const string cnStr = "Data Source=NotePC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Users";
            //string dp = ConfigurationManager.AppSettings["provider"]; // dataprovider
            //string cnStr = ConfigurationManager.AppSettings["cnStr"]; // строка для соединения с базой данных 

            // соединяемся с базой
            UsersDAL uDal = new UsersDAL();
            uDal.OpenConnection(cnStr);

            // работа через команды
            do
            {
                Console.WriteLine("Введите команду: ");
                string cmd = Console.ReadLine();
                switch (cmd.ToLower())
                {
                    case "login":
                        Console.Write("login: ");
                        string login = Console.ReadLine();
                        Console.Write("password: ");
                        string password = Console.ReadLine();
                        loginUser.Login(uDal, login, password);
                        if (!loginUser.IsLogin) ConsoleLog.WriteOnConsole(true, "Неверный логин или пароль");
                        break;
                        
                    case "exit":
                        loginUser.Logout();
                        isDone = true;
                        break;

                    case "logout":
                        if (!loginUser.IsLogin) ConsoleLog.WriteOnConsole(true, "Логина не было");
                        else loginUser.Logout();
                        break;

                    case "Input Text":
                        if (!loginUser.IsLogin) ConsoleLog.WriteOnConsole(true, "Логина не было");
                        else
                        {
                            Console.WriteLine("Введите текст: ");
                            uDal.UpdateTextMessage(loginUser.Id, Console.ReadLine());
                        }
                        break;

                    case "OutputTop":
                        if (!loginUser.IsLogin) ConsoleLog.WriteOnConsole(true, "Логина не было");
                        else
                        {
                            List<User> users = uDal.TopTen(false);
                            foreach (var user in users)
                            {
                                Console.WriteLine(user);
                            }
                        }
                        break;
                        
                    case "OutputTopDesc":
                        if (!loginUser.IsLogin) ConsoleLog.WriteOnConsole(true, "Логина не было");
                        else uDal.TopTen(true);
                        List<User> usersDesc = uDal.TopTen(false);
                        foreach (var user in usersDesc)
                        {
                            Console.WriteLine(user);
                        }
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
            } while (!isDone);
            uDal.CloseConnection();
        }
    }
}